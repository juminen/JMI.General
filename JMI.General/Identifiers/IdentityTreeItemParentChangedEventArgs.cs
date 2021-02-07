using System;

namespace JMI.General.Identifiers
{
    public class IdentityTreeItemParentChangedEventArgs<T> : EventArgs where T : IIdentityTreeItem<T>
    {
        public IIdentityTreeItem<T> OldParent { get; set; }
        public IIdentityTreeItem<T> NewParent { get; set; }
    }
}