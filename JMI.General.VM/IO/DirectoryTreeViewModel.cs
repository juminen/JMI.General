using JMI.General.IO;
using JMI.General.Logging;
using JMI.General.Trees;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JMI.General.VM.IO
{
    public class DirectoryTreeViewModel : Tree
    {
        #region constructors
        public DirectoryTreeViewModel()
        {
            IsLoadingNodes = false;
        }
        #endregion

        #region properties
        private Logger logger = Logger.Instance;
        private Dictionary<string, ITreeItem> nodeDict = new Dictionary<string, ITreeItem>();

        private bool isLoadingNodes;
        public bool IsLoadingNodes
        {
            get { return isLoadingNodes; }
            private set { SetProperty(ref isLoadingNodes, value); }
        }
        #endregion

        #region commands
        #endregion

        #region methods
        public override void LoadRootNodes()
        {
            if (IsLoadingNodes)
            {
                return;
            }

            IsLoadingNodes = true;
            LoadRoots();
            IsLoadingNodes = false;
        }

        public async override Task LoadRootNodesAsync()
        {
            if (IsLoadingNodes)
            {
                return;
            }

            IsLoadingNodes = true;
            await LoadRootsAsync();
            IsLoadingNodes = false;
        }

        private async Task LoadRootsAsync()
        {
            //Get all expanded nodes
            List<ITreeItem> oldExpanded = new List<ITreeItem>();
            if (ExpandedNodes.Count > 0)
            {
                foreach (ITreeItem item in ExpandedNodes)
                {
                    oldExpanded.Add(item);
                }
                oldExpanded = new List<ITreeItem>(oldExpanded.OrderBy(x => x.Level));
            }

            //Clean tree
            foreach (ITreeItem item in nodeDict.Values)
            {
                UnSubscribeEvents(item);
            }
            nodeDict.Clear();
            ClearTree();

            //Get drives
            IEnumerable<DriveInfo> diResult = await LocalDriveCrawler.GetReadyDrivesAsync();

            foreach (DriveInfo item in diResult)
            {
                DriveTreeItemViewModel vm = new DriveTreeItemViewModel(item);
                AddToTree(vm);
                nodeDict.Add(vm.Path.ToLower(), vm);
                vm.Expanded += OnVmExpanded;
                vm.Collapsed += OnVmCollapsed;
                //create children for root nodes
                CreateSubFolders(vm);
            }


            if (oldExpanded.Count > 0)
            {
                foreach (ITreeItem item in oldExpanded)
                {
                    if (nodeDict.ContainsKey(item.Path.ToLower()))
                    {
                        nodeDict[item.Path.ToLower()].IsExpanded = true;
                    }
                }
            }
        }

        private void LoadRoots()
        {
            //Get all expanded nodes
            List<ITreeItem> oldExpanded = new List<ITreeItem>();
            if (ExpandedNodes.Count > 0)
            {
                foreach (ITreeItem item in ExpandedNodes)
                {
                    oldExpanded.Add(item);
                }
                oldExpanded = new List<ITreeItem>(oldExpanded.OrderBy(x => x.Level));
            }

            //Clean tree
            foreach (ITreeItem item in nodeDict.Values)
            {
                UnSubscribeEvents(item);
            }
            nodeDict.Clear();
            ClearTree();

            //Get drives
            ActionResult<IEnumerable<DriveInfo>> diResult = DirectoryCrawler.GetDrivesReady();
            if (diResult.Successful)
            {
                foreach (DriveInfo item in diResult.Result)
                {
                    DriveTreeItemViewModel vm = new DriveTreeItemViewModel(item);
                    AddToTree(vm);
                    nodeDict.Add(vm.Path.ToLower(), vm);
                    vm.Expanded += OnVmExpanded;
                    vm.Collapsed += OnVmCollapsed;
                    //create children for root nodes
                    CreateSubFolders(vm);
                }
            }
            //else
            //{
            //    logger.Log(LogFactory.CreateWarningMessage(diResult.GetComments()));
            //    return;
            //}

            if (oldExpanded.Count > 0)
            {
                foreach (ITreeItem item in oldExpanded)
                {
                    if (nodeDict.ContainsKey(item.Path.ToLower()))
                    {
                        nodeDict[item.Path.ToLower()].IsExpanded = true;
                    }
                }
            }
        }

        private void CreateSubFolders(ITreeItem parent)
        {
            ActionResult<IEnumerable<DirectoryInfo>> actionResult =
                DirectoryCrawler.GetSubFolders(parent.Path, false);
            if (actionResult.Successful)
            {
                foreach (DirectoryInfo di in actionResult.Result)
                {
                    FolderTreeItemViewModel sub = new FolderTreeItemViewModel(di);
                    AddToTree(sub);
                    nodeDict.Add(sub.Path.ToLower(), sub);
                    sub.Expanded += OnVmExpanded;
                    sub.Collapsed += OnVmCollapsed;
                    parent.AddChild(sub);
                }
            }
            //else
            //{
            //    logger.Log(LogMessageStatus.Warning, actionResult.GetComments());
            //}
        }

        private void RemoveNodes(List<ITreeItem> itemsToRemove)
        {
            foreach (ITreeItem node in itemsToRemove)
            {
                if (nodeDict.ContainsKey(node.Path.ToLower()))
                {
                    UnSubscribeEvents(nodeDict[node.Path.ToLower()]);
                    nodeDict.Remove(node.Path.ToLower());
                }
            }
            RemoveFromTree(itemsToRemove);
        }

        private void UnSubscribeEvents(ITreeItem item)
        {
            item.Expanded -= OnVmExpanded;
            item.Collapsed -= OnVmCollapsed;
        }

        private void SubscribeEvents(ITreeItem item)
        {
            item.Expanded += OnVmExpanded;
            item.Collapsed += OnVmCollapsed;
        }

        public void SelectTreeItem(string path)
        {
            if (!Directory.Exists(path))
            {
                logger.Log(LogFactory.CreateWarningMessage($"Path '{path}' does not exist."));
                return;
            }

            if (CurrentNode != null
                && CurrentNode.Path.Equals(path, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            List<string> list = CreatePaths(path);

            foreach (string item in list)
            {
                if (nodeDict.ContainsKey(item))
                {
                    ITreeItem node = nodeDict[item];
                    if (!node.IsExpanded)
                    {
                        node.IsExpanded = true;
                    }
                }
            }

            if (nodeDict.ContainsKey(path.ToLower()))
            {
                nodeDict[path.ToLower()].IsSelected = true;
            }
        }

        private List<string> CreatePaths(string path)
        {
            List<string> list = new List<string>();

            char[] charSeparators = new char[] { Path.DirectorySeparatorChar };
            List<string> paths = path.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries).ToList();
            string current = paths[0] + Path.DirectorySeparatorChar;
            list.Add(current);
            paths.RemoveAt(0);

            foreach (string item in paths)
            {
                current = Path.Combine(current, item);
                list.Add(current.ToLower());
            }

            return list;
        }
        #endregion

        #region events
        #endregion

        #region event handlers
        private void OnVmExpanded(object sender, EventArgs e)
        {
            ITreeItem parent = (ITreeItem)sender;
            Console.WriteLine($"OnVmExpanded: {parent.Path} expanded");
            List<ITreeItem> children = GetDescendants(parent);
            if (children.Count > 0)
            {
                RemoveNodes(children);
                parent.RemoveAllChildren();
            }
            CreateSubFolders(parent);
            //Create children for parent's children
            foreach (ITreeItem item in parent.Children)
            {
                CreateSubFolders(item);
            }
        }

        private void OnVmCollapsed(object sender, EventArgs e)
        {
            //Collapse all children that are expanded.
            //All descendants will be collapsed too.
            ITreeItem parent = (ITreeItem)sender;
            foreach (ITreeItem item in parent.Children)
            {
                if (item.IsExpanded)
                {
                    item.IsExpanded = false;
                }
            }
        }
        #endregion
    }
}
//F:\Omat\CAD\Blokit\Vanhat