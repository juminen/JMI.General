using System;

namespace JMI.General
{
    public class CreatedChanged : ObservableObject, ICreatedChanged
    {
        #region constructors
        /// <summary>
        /// Default constructor. Sets <see cref="Created"/> and 
        /// <see cref="Changed"/> to local now.
        /// </summary>
        public CreatedChanged()
        {
            Created = DateTime.Now;
            changed = created;
        }

        /// <summary>
        /// Constructor for setting <see cref="Created"/> and <see cref="Changed"/>.
        /// </summary>
        /// <param name="created">creation time</param>
        /// <param name="changed">change time</param>
        public CreatedChanged(DateTime created, DateTime changed)
        {
            Created = created;
            Changed = changed;
        }
        #endregion

        #region properties
        /// <summary>
        /// Date and time when object was created.
        /// </summary>
        private DateTime created;
        public DateTime Created
        {
            get { return created; }
            private set { SetProperty(ref created, value); }
        }

        /// <summary>
        /// Date and time when object was changed.
        /// </summary>
        private DateTime changed;
        public DateTime Changed
        {
            get { return changed; }
            set { SetProperty(ref changed, value); }
        }
        #endregion

        #region methods
        /// <summary>
        /// Sets <see cref="Changed"/> to local now.
        /// </summary>
        public void SetChangedToNow()
        {
            Changed = DateTime.Now;
        }
        #endregion

        #region events
        #endregion

        #region event handlers
        #endregion
    }
}
