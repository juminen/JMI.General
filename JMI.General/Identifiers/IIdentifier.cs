using System;

namespace JMI.General.Identifiers
{
    /// <summary>
    /// Provides mechanism for identifying objects
    /// </summary>
    public interface IIdentifier
    {
        /// <summary>
        /// Returns the unique (<see cref="Guid"/>) id of the item as a string.
        /// </summary>
        string Id { get; }
        /// <summary>
        /// Returns the unique id of the item.
        /// </summary>
        /// <returns><see cref="Guid"/></returns>
        Guid GetGuid();
    }
}