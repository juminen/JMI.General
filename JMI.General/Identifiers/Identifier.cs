using System;

namespace JMI.General.Identifiers
{
    /// <summary>
    /// Provides mechanism for identifying objects
    /// </summary>
    public sealed class Identifier : ObservableObject, IIdentifier
    {
        #region constructors
        /// <summary>
        /// Constructor that creates new identifier (<see cref="Id"/>)
        /// </summary>
        public Identifier()
        {
            id = Guid.NewGuid();
            OnPropertyChanged(nameof(Id));
        }

        /// <summary>
        /// Constructor for existing item
        /// </summary>
        /// <param name="uniqueIdentifier">Unique identifier for object</param>
        public Identifier(Guid uniqueIdentifier)
        {
            id = uniqueIdentifier;
        }
        #endregion constructors

        #region properties
        /// <summary>
        /// unique id (<see cref="Guid"/>)
        /// </summary>
        private Guid id;
        /// <summary>
        /// Returns the unique (<see cref="Guid"/>) id of the item as a string.
        /// </summary>
        public string Id { get { return id.ToString(); } }
        #endregion properties

        #region methods
        /// <summary>
        /// Returns the unique id of the item.
        /// </summary>
        /// <returns><see cref="Guid"/></returns>
        public Guid GetGuid()
        {
            return id;
        }
        #endregion
    }
}
