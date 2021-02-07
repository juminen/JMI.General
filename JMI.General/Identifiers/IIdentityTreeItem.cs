using System;
using System.ComponentModel;

namespace JMI.General.Identifiers
{
    public interface IIdentityTreeItem<T> : IIdentityCollectionItem, INotifyPropertyChanged where T: IIdentityTreeItem<T>
    {
        /// <summary>
        /// Name of the tree item.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Description of the tree item.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Returns the item full path 
        /// (ie. <see cref="Parent"/> full path combined to <see cref="Name"/>.
        /// </summary>
        string FullPath { get; }
        /// <summary>
        /// Parent of the tree item. 
        /// When parent is changed, event <see cref="ParentChanged"/> is sent.
        /// </summary>
        T Parent { get; }
        /// <summary>
        /// Collection of child items.
        /// </summary>
        IReadOnlyIdentityCollection<T> Children { get; }
        /// <summary>
        /// Add child to <see cref="Children"/>.
        /// </summary>
        /// <param name="child">child to add</param>
        /// <returns>True if child was added to <see cref="Children"/></returns>
        bool AddChild(T child);
        /// <summary>
        /// Remove child from <see cref="Children"/>.
        /// </summary>
        /// <param name="child">child to remove</param>
        /// <returns>True if child was removed from <see cref="Children"/></returns>
        bool RemoveChild(T child);
        /// <summary>
        /// Checks if given item in parameter is the ancestor of this tag. 
        /// Uses <see cref="Identity"/> property <see cref="IIdentifier.Id"/> for checking.
        /// </summary>
        /// <param name="item">Possible ancestor item</param>
        /// <returns>True if this item is descendant of item given in paremeter.</returns>
        bool IsDescendantOf(T item);
        /// <summary>
        /// Fills collection given in parameter <paramref name="collectionToFill"/> with 
        /// the descendants of tree item.
        /// </summary>
        /// <param name="collectionToFill">Collection to fill with descendants</param>
        void FillCollectionWithDescendants(IIdentityCollection<T> collectionToFill);
        /// <summary>
        /// When <see cref="Parent"/> is changed, event <see cref="ParentChanged"/> is sent.
        /// </summary>
        event EventHandler<IdentityTreeItemParentChangedEventArgs<T>> ParentChanged;
    }
}