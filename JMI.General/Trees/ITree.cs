using System;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JMI.General.Trees
{
    public interface ITree
    {
        ITreeItem CurrentNode { get; }
        ListCollectionView ExpandedNodes { get; }
        ListCollectionView RootNodes { get; }
        ListCollectionView SelectedNodes { get; }

        event EventHandler CurrentNodeChanged;

        void LoadRootNodes();
        Task LoadRootNodesAsync();
        void MoveNodeToRoot(ITreeItem node);
        void MoveNodeToUnderParent(ITreeItem newParent, ITreeItem nodeToMove);
    }
}