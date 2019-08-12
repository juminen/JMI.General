using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JMI.General
{
    //http://danrigby.com/2012/04/01/inotifypropertychanged-the-net-4-5-way-revisited/
    /// <example>
    /// public class ObservableCat : ObservableObject
    /// {
    ///     private bool _isSleeping = true;
    ///     public bool IsSleeping
    ///     {
    ///         get { return _isSleeping; }
    ///         set { SetProperty(ref _isSleeping, value); }
    ///     }
    /// }
    /// </example>
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propName">
        /// Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically 
        /// when invoked from compilers that support <see cref="CallerMemberNameAttribute" />.
        /// </param>
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        /// Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.
        /// </param>
        /// <returns>
        /// True if the value was changed, false if the existing value matched the desired value.
        /// </returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}