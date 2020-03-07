using System;

namespace JMI.General.ChangeTracking
{
    class PropertyTracker<T1, T2> : IDisposable, IPropertyTracker where T1 : ObservableObject
        where T2 : ObservableObject
    {
        #region constructors
        public PropertyTracker(T1 originalObjectToCompare,
            T2 objectToTrack, string propertyNameToTrack)
        {
            originalObject = originalObjectToCompare;
            originalObject.PropertyChanged += OnPropertyChanged;
            trackedObject = objectToTrack;
            TrackedPropertyName = propertyNameToTrack;
            trackedObject.PropertyChanged += OnPropertyChanged;
            CompareValues();
        }
        #endregion

        #region properties
        private readonly T1 originalObject;
        private readonly T2 trackedObject;
        public string TrackedPropertyName { get; private set; }
        public bool ComparisonResult { get; private set; }
        #endregion

        #region methods
        public void Dispose()
        {
            originalObject.PropertyChanged -= OnPropertyChanged;
            trackedObject.PropertyChanged -= OnPropertyChanged;
        }

        private void CompareValues()
        {
            var originalValue = originalObject.GetType().GetProperty(TrackedPropertyName).GetValue(originalObject);
            var trackValue = trackedObject.GetType().GetProperty(TrackedPropertyName).GetValue(trackedObject);

            if (originalValue == null && trackValue == null)
            {
                ComparisonResult = true;
            }
            else if ((originalValue == null && trackValue != null) ||
            (originalValue != null && trackValue == null))
            {
                ComparisonResult = false;
            }
            else
            {
                ComparisonResult = originalValue.Equals(trackValue);
            }
            ValuesChecked?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region events
        public event EventHandler ValuesChecked;
        #endregion

        #region event handlers
        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(TrackedPropertyName))
            {
                CompareValues();
            }
        }
        #endregion

    }
}
