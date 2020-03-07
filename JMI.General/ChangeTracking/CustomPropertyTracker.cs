using System;

namespace JMI.General.ChangeTracking
{
    class CustomPropertyTracker<T1, T2> : IDisposable, IPropertyTracker
        where T1 : ObservableObject
        where T2 : ObservableObject
    {
        #region constructors
        public CustomPropertyTracker(
            T1 originalObjectToCompare,
            string propertyNameToFollow,
            T2 objectToTrack,
            string propertyNameToTrack)
        {
            originalPropertyName = propertyNameToFollow;
            originalObject = originalObjectToCompare;
            originalObject.PropertyChanged += OnOriginalPropertyChanged;

            trackedPropertyName = propertyNameToTrack;
            trackedObject = objectToTrack;
            trackedObject.PropertyChanged += OnTrackedPropertyChanged;

            CompareValues();
        }
        #endregion

        #region properties
        private readonly T1 originalObject;
        private readonly string originalPropertyName;
        private readonly T2 trackedObject;
        private readonly string trackedPropertyName;
        public bool ComparisonResult { get; private set; }
        #endregion

        #region methods
        public void Dispose()
        {
            originalObject.PropertyChanged -= OnOriginalPropertyChanged;
            trackedObject.PropertyChanged -= OnTrackedPropertyChanged;
        }

        private void CompareValues()
        {
            var originalValue = originalObject.GetType().GetProperty(originalPropertyName).GetValue(originalObject);
            var trackValue = trackedObject.GetType().GetProperty(trackedPropertyName).GetValue(trackedObject);
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
        private void OnOriginalPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(originalPropertyName))
            {
                CompareValues();
            }
        }

        private void OnTrackedPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(trackedPropertyName))
            {
                CompareValues();
            }
        }
        #endregion

    }
}
