using JMI.General.Identifiers;
using JMI.General.Sorting;
using System.Collections;

namespace JMI.General.VM.Trees
{
    public class TreeItemNameComparer<T> : IComparer where T: IIdentityTreeItem<T>
    {
        public int Compare(object x, object y)
        {
            IIdentityTreeItemViewModel<T> a = x as IIdentityTreeItemViewModel<T>;
            IIdentityTreeItemViewModel<T> b = y as IIdentityTreeItemViewModel<T>;
            if (a != null && b != null)
            {
                AlphanumStringComparatorFast comp = new AlphanumStringComparatorFast();
                return comp.Compare(a.Name, b.Name);
            }
            return -1;
        }
    }
}
