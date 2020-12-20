using System;

namespace JMI.General
{
    public interface ICreatedChanged
    {
        /// <summary>
        /// Date and time when object was created.
        /// </summary>
        DateTime Created { get; }
        /// <summary>
        /// Date and time when object was changed.
        /// </summary>
        DateTime Changed { get; set; }
        /// <summary>
        /// Sets <see cref="Changed"/> to local now.
        /// </summary>
        void SetChangedToNow();
    }
}
