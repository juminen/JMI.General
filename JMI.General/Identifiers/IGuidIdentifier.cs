using System;

namespace JMI.General.Identifiers
{
    /// <summary>
    /// Provides mechanism for identifying objects using Guid.
    /// </summary>
    public interface IGuidIdentifier : IIdentifier
    {
        Guid GetIdObject();
    }
}