using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMI.General.Identifiers
{
    public interface IIdentityTreeManager<T> where T : IIdentityTreeItem<T>
    {
        #region properties
        /// <summary>
        /// Collection containing all root items.
        /// </summary>
        IReadOnlyIdentityCollection<T> RootItems { get; }
        /// <summary>
        /// Collection containing all tree items.
        /// </summary>
        IReadOnlyIdentityCollection<T> AllItems { get; }
        #endregion

        #region methods
        void RefreshChildren(T parentOfChildren);

        void RefreshRootItems();

        Task<bool> RefreshItemsAsync(IEnumerable<T> items);

        /// <summary>
        /// Creates a copy from the given tree item and puts it to children of 
        /// given parent.
        /// </summary>
        /// <param name="itemToCopy">Tree item to be copied</param>
        /// <param name="copyDescendats">Copy all descendants if set to true</param>
        /// <param name="parentOfNewCopy">Parent of the copied new tree item</param>
        /// <returns>If copying was succesfull, copy of the 
        /// tree item is set to <see cref="ActionResult{T}.Result"/></returns>
        Task<bool> CreateCopyAsync(
            T itemToCopy,
            bool copyDescendats,
            T parentOfNewCopy);

        /// <summary>
        /// Creates a copy from the given tree item and makes it root item.
        /// </summary>
        /// <param name="itemToCopy">Tree item to be copied</param>
        /// <param name="copyDescendats">Copy all descendants if set to true</param>
        /// <returns>If copying was succesfull, copy of the 
        /// tree item is set to <see cref="ActionResult{T}.Result"/></returns>
        Task<bool> CreateCopyAsync(
            T itemToCopy,
            bool copyDescendats);

        /// <summary>
        /// Moves tree item to under new parent.
        /// </summary>
        /// <param name="itemToMove">Tree item to be moved</param>
        /// <param name="newParent">New parent of the tree item</param>
        /// <returns>True if moving was succesfull</returns>
        Task<bool> MoveToAsync(T itemToMove, T newParent);

        /// <summary>
        /// Moves tree item to root.
        /// </summary>
        /// <param name="itemToMove">Tree item to be moved</param>
        /// <returns>True if moving was succesfull</returns>
        Task<bool> MoveToAsync(T itemToMove);

        /// <summary>
        /// Remove tree item from the tree.
        /// </summary>
        /// <param name="itemToDelete">Tree item to be removed</param>
        /// <returns>True, if remove was succesfull</returns>
        Task<bool> RemoveAsync(T itemToDelete);

        /// <summary>
        /// Removes tree items from the tree.
        /// </summary>
        /// <param name="itemsToDelete">Tree items to be removed</param>
        /// <returns>True, if remove was succesfull</returns>
        Task<bool> RemoveAsync(IEnumerable<T> itemsToDelete);
        #endregion

        #region events
        #endregion
    }
}
