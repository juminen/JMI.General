using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JMI.General.Tree
{
    public abstract class Tree : ObservableObject, ITree
    {
        #region constructors
        public Tree()
        {
            allNodes = new ObservableCollection<ITreeItem>();

            RootNodes = new ListCollectionView(allNodes)
            {
                Filter = new Predicate<object>(ParentFilter),
                IsLiveFiltering = true
            };
            RootNodes.LiveFilteringProperties.Add("Parent");

            ExpandedNodes = new ListCollectionView(allNodes)
            {
                Filter = new Predicate<object>(IsExpandedFilter),
                IsLiveFiltering = true
            };
            ExpandedNodes.LiveFilteringProperties.Add("IsExpanded");

            SelectedNodes = new ListCollectionView(allNodes)
            {
                Filter = new Predicate<object>(IsSelectedFilter),
                IsLiveFiltering = true
            };
            SelectedNodes.LiveFilteringProperties.Add("IsSelected");
        }
        #endregion

        #region properties
        private ObservableCollection<ITreeItem> allNodes;
        public ListCollectionView RootNodes { get; protected set; }
        public ListCollectionView ExpandedNodes { get; protected set; }
        public ListCollectionView SelectedNodes { get; protected set; }

        private ITreeItem currentNode;
        public ITreeItem CurrentNode
        {
            get { return currentNode; }
            private set
            {
                if (SetProperty(ref currentNode, value))
                {
                    CurrentNodeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion

        #region methods
        private bool ParentFilter(object obj)
        {
            return ((ITreeItem)obj).Parent == null;
        }

        private bool IsExpandedFilter(object obj)
        {
            return ((ITreeItem)obj).IsExpanded;
        }

        private bool IsSelectedFilter(object obj)
        {
            return ((ITreeItem)obj).IsSelected;
        }

        public abstract void LoadRootNodes();

        protected void AddToTree(ITreeItem node)
        {
            if (!allNodes.Contains(node))
            {
                allNodes.Add(node);
                node.Selected += OnTreeItemSelected;
            }
        }

        protected void AddToTree(IEnumerable<ITreeItem> nodes)
        {
            foreach (ITreeItem item in nodes)
            {
                AddToTree(item);
            }
        }

        protected List<ITreeItem> GetDescendants(ITreeItem parent)
        {
            List<ITreeItem> list = new List<ITreeItem>();
            foreach (ITreeItem child in parent.Children)
            {
                list.Add(child);
                list.AddRange(GetDescendants(child));
            }
            return list;
        }

        /// <summary>
        /// Removes node and all descendants from tree
        /// </summary>
        /// <param name="node">Node to remove</param>
        protected void RemoveFromTree(ITreeItem node)
        {
            if (!allNodes.Contains(node)) return;

            foreach (ITreeItem descendant in GetDescendants(node))
            {
                allNodes.Remove(descendant);
            }
            node.Selected -= OnTreeItemSelected;
            allNodes.Remove(node);
        }

        /// <summary>
        /// Removes nodes and all descendants from tree
        /// </summary>
        /// <param name="nodes">Nodes to remove</param>
        protected void RemoveFromTree(IEnumerable<ITreeItem> nodes)
        {
            foreach (ITreeItem item in nodes)
            {
                RemoveFromTree(item);
            }
        }

        public void MoveNodeToRoot(ITreeItem node)
        {
            if (node.Parent != null)
            {
                node.Parent.RemoveChild(node);
            }
        }

        public void MoveNodeToUnderParent(ITreeItem newParent, ITreeItem nodeToMove)
        {
            if (newParent == null) throw new ArgumentNullException(nameof(newParent));
            if (newParent.IsDescendantOf(nodeToMove)) return;
            nodeToMove.Parent.RemoveChild(nodeToMove);
            newParent.AddChild(nodeToMove);
        }
        #endregion

        #region events
        public event EventHandler CurrentNodeChanged;
        #endregion

        #region event handlers

        private void OnTreeItemSelected(object sender, EventArgs e)
        {
            CurrentNode = (ITreeItem)sender;
        }
        #endregion
    }
}
