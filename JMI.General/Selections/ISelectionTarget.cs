using JMI.General.Identifiers;

namespace JMI.General.Selections
{
    /// <summary>
    ///Item that is target of the selection.
    /// </summary>
    public interface ISelectionTarget
    {
        /// <summary>
        /// Identifier for the target object (<see cref="IIdentifier"/>).
        /// </summary>
        IIdentifier Identifier { get; }
    }
}