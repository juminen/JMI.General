using System;

namespace JMI.General.Identifiers
{
    public abstract class IdentityTreeItem<T> : ObservableObject, IIdentityTreeItem<T> 
        where T : IIdentityTreeItem<T>
    {
        #region constructors
        /// <summary>
        /// Default constructor, uses <see cref="GuidIdentifier"/> as identifier.
        /// </summary>
        public IdentityTreeItem()
        {
            Identifier = new GuidIdentifier();
            children = new IdentityCollection<T>();
            Children = children;
        }
        #endregion

        #region properties
        /// <summary>
        /// Identifier of the tree item.
        /// </summary>
        public IIdentifier Identifier { get; protected set; }

        private string name;
        /// <summary>
        /// Name of the tree item.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string description;
        /// <summary>
        /// Description of the tree item.
        /// </summary>
        public virtual string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        protected IdentityCollection<T> children;

        /// <summary>
        /// Collection of child items.
        /// </summary>
        public IReadOnlyIdentityCollection<T> Children { get; }

        /// <summary>
        /// Returns the item full path 
        /// (ie. <see cref="Parent"/> full path combined to <see cref="Name"/>.
        /// </summary>
        public virtual string FullPath
        {
            get
            {
                if (Parent != null)
                {
                    return $"{Parent.FullPath}{Name}";
                }
                return Name;
            }
        }

        private T parent;
        /// <summary>
        /// Parent of the tree item. 
        /// When parent is changed, event <see cref="ParentChanged"/> is sent.
        /// </summary>
        public T Parent
        {
            get { return parent; }
            set
            {
                T oldParent = parent;
                T newParent = value;
                if (SetProperty(ref parent, value))
                {
                    IdentityTreeItemParentChangedEventArgs<T> args =
                        new IdentityTreeItemParentChangedEventArgs<T>()
                        {
                            OldParent = oldParent,
                            NewParent = newParent
                        };
                    ParentChanged?.Invoke(this, args);
                }
            }
        }
        #endregion

        #region methods
        public override string ToString()
        {
            return $"Name: {Name}, Description: {Description}, FullPath: {FullPath}";
        }

        /// <summary>
        /// Checks if given item in parameter is the ancestor of this tree item. 
        /// Uses <see cref="Identity"/> property <see cref="IIdentifier.Id"/> for checking.
        /// </summary>
        /// <param name="possibleAncestor">Possible ancestor item</param>
        /// <returns>True if this item is descendant of item given in paremeter.</returns>
        public bool IsDescendantOf(T possibleAncestor)
        {
            if (Parent == null)
            {
                return false;
            }

            if (Parent.Identifier.Id.Equals(possibleAncestor.Identifier.Id))
            {
                return true;
            }

            return Parent.IsDescendantOf(possibleAncestor);
        }

        /// <summary>
        /// Fills collection given in parameter <paramref name="collectionToFill"/> with 
        /// the descendants of tree item.
        /// </summary>
        /// <param name="collectionToFill">Collection to fill with descendants</param>
        public void FillCollectionWithDescendants(IIdentityCollection<T> collectionToFill)
        {
            foreach (var item in Children)
            {
                collectionToFill.AddItem(item);
                item.FillCollectionWithDescendants(collectionToFill);
            }
        }

        /// <summary>
        /// Add child to <see cref="Children"/>.
        /// </summary>
        /// <param name="child">child to add</param>
        /// <returns>True if child was added to <see cref="Children"/></returns>
        public abstract bool AddChild(T child);

        /// <summary>
        /// Remove child from <see cref="Children"/>.
        /// </summary>
        /// <param name="child">child to remove</param>
        /// <returns>True if child was removed from <see cref="Children"/></returns>
        public abstract bool RemoveChild(T child);
        #endregion

        #region events
        /// <summary>
        /// When <see cref="Parent"/> is changed, event <see cref="ParentChanged"/> is sent.
        /// </summary>
        public event EventHandler<IdentityTreeItemParentChangedEventArgs<T>> ParentChanged;
        #endregion

        #region event handlers
        #endregion
    }
}
