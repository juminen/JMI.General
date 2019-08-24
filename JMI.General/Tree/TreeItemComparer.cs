using JMI.General.Sorting;
using System.Collections;

namespace JMI.General.Tree
{
    /// <summary>
    /// Compares tree items with their displaynames.
    /// Uses <see cref="AlphanumComparatorFast"/> for comparison.
    /// </summary>
    public class TreeItemComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (!(x is ITreeItem item1))
            {
                return 0;
            }
            if (!(y is ITreeItem item2))
            {
                return 0;
            }

            AlphanumComparatorFast comp = new AlphanumComparatorFast();
            return comp.Compare(item1.DisplayName, item2.DisplayName);
        }
    }
}
