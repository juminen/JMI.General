using System;
using System.Windows.Data;

namespace JMI.General.Tree
{
    public interface ITree
    {
        ITreeItem CurrentNode { get; }
        ListCollectionView ExpandedNodes { get; }
        ListCollectionView RootNodes { get; }
        ListCollectionView SelectedNodes { get; }

        event EventHandler CurrentNodeChanged;

        void LoadRootNodes();
        void MoveNodeToRoot(ITreeItem node);
        void MoveNodeToUnderParent(ITreeItem newParent, ITreeItem nodeToMove);
    }
}