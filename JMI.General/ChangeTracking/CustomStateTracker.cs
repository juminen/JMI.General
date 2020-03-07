using System;
using System.Collections.Generic;
using System.Linq;

namespace JMI.General.ChangeTracking
{
    /// <summary>
    /// Class provides mechanism to track/compare property changes 
    /// between different objects. 
    /// </summary>
    public class CustomStateTracker : ObservableObject, IDisposable, ICustomStateTracker
    {
        #region constructors
        public CustomStateTracker()
        {
            trackers = new List<IPropertyTracker>();
        }
        #endregion

        #region properties
        private List<IPropertyTracker> trackers;

        private bool sameState;
        /// <summary>
        /// If both object tracked property values are same, returns true, otherwise false.
        /// </summary>
        public bool SameState
        {
            get { return sameState; }
            private set
            {
                if (SetProperty(ref sameState, value))
                {
                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion

        #region methods
        private void CheckState()
        {
            if (trackers.All(t => t.ComparisonResult == true))
            {
                SameState = true;
            }
            else
            {
                SameState = false;
            }
        }

        public void Dispose()
        {
            foreach (IPropertyTracker item in trackers)
            {
                item.ValuesChecked -= OnTrackerValueChecked;
                item.Dispose();
            }
        }

        /// <summary>
        /// Use this method to add comparable objects/properties.
        /// </summary>
        /// <typeparam name="T1"><see cref="ObservableObject"/></typeparam>
        /// <typeparam name="T2"><see cref="ObservableObject"/></typeparam>
        /// <param name="originalObjectToCompare">Object to which property value is compared.</param>
        /// <param name="originalPropertyName">Name of the compared property</param>
        /// <param name="objectToTrack">Object whose property value changes are tracked.</param>
        /// <param name="trackedPropertyName">Name of the compared property</param>
        public void AddTracking<T1, T2>(
            T1 originalObjectToCompare,
            string originalPropertyName,
            T2 objectToTrack,
            string trackedPropertyName)
            where T1 : ObservableObject
            where T2 : ObservableObject
        {
            CustomPropertyTracker<T1, T2> cpt = new CustomPropertyTracker<T1, T2>(
                originalObjectToCompare, originalPropertyName,
                objectToTrack, trackedPropertyName);
            cpt.ValuesChecked += OnTrackerValueChecked;
            trackers.Add(cpt);
            CheckState();
        }
        #endregion

        #region events
        public event EventHandler StateChanged;
        #endregion

        #region event handlers
        private void OnTrackerValueChecked(object sender, EventArgs e)
        {
            CheckState();
        }
        #endregion
    }
}
