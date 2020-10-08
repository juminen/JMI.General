using System;

namespace JMI.General.Identifiers
{
    /// <summary>
    /// Provides mechanism for identifying objects using Guid.
    /// </summary>
    public sealed class GuidIdentifier : Identifier<Guid>, IGuidIdentifier
    {
        #region constructors
        /// <summary>
        /// Constructor that creates new identifier (<see cref="Id"/>)
        /// </summary>
        public GuidIdentifier()
        {
            id = Guid.NewGuid();
            OnPropertyChanged(nameof(Id));
        }

        /// <summary>
        /// Constructor for existing item
        /// </summary>
        /// <param name="uniqueIdentifier">Unique identifier for object</param>
        public GuidIdentifier(Guid uniqueIdentifier)
        {
            id = uniqueIdentifier;
            OnPropertyChanged(nameof(Id));
        }
        #endregion constructors

        #region properties
        #endregion properties

        #region methods
        #endregion
    }
}