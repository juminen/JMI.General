namespace JMI.General.Identifiers
{
    /// <summary>
    /// Provides mechanism for identifying objects using string.
    /// </summary>
    public sealed class StringIdentifier : Identifier<string>
    {
        #region constructors
        /// <summary>
        /// Constructor for existing item
        /// </summary>
        /// <param name="uniqueIdentifier">Unique identifier for object</param>
        public StringIdentifier(string uniqueIdentifier)
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