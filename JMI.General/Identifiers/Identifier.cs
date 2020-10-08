namespace JMI.General.Identifiers
{
    /// <summary>
    /// Provides mechanism for identifying objects
    /// </summary>
    public abstract class Identifier<T> : ObservableObject, IIdentifier
    {
        #region constructors
        /// <summary>
        /// Constructor that prevents using this class outside assembly.
        /// </summary>
        internal Identifier()
        {
        }
        #endregion constructors

        #region properties
        /// <summary>
        /// Unique id
        /// </summary>
        protected T id;
        /// <summary>
        /// Returns the unique id of the item as a string.
        /// </summary>
        public virtual string Id { get { return id.ToString(); } }
        #endregion properties

        #region methods
        /// <summary>
        /// Returns the id object.
        /// </summary>
        /// <returns><see cref="T"/></returns>
        public T GetIdObject()
        {
            return id;
        }
        #endregion
    }
}