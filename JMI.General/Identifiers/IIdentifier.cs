using System;

namespace JMI.General.Identifiers
{
    /// <summary>
    /// Provides mechanism for identifying objects
    /// </summary>
    public interface IIdentifier
    {
        /// <summary>
        /// Returns the unique id of the item as a string.
        /// </summary>
        string Id { get; }
    }
}