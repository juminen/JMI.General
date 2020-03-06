using System;
using System.Collections.Generic;
using System.Linq;

namespace JMI.General.ChangeTracking
{
    /// <summary>
    /// Class provides mechanism to track/compare property changes 
    /// between two objects.
    /// </summary>
    public class StateTracker<T1, T2> : ObservableObject, IDisposable
        where T1 : ObservableObject
        where T2 : ObservableObject
    {
        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="propertyNamesToTrack">List of names of properties whose values are tracked.
        /// Both objects has to have same property names.</param>
        /// <param name="originalObjectToCompare">Object where property values are compared.</param>
        /// <param name="objectToTrack">Object whose property value changes are tracked.</param>
        public StateTracker(
            IEnumerable<string> propertyNamesToTrack,
            T1 originalObjectToCompare,
            T2 objectToTrack)
        {
            if (originalObjectToCompare == null)
            {
                throw new ArgumentNullException(nameof(originalObjectToCompare));
            }
            if (objectToTrack == null)
            {
                throw new ArgumentNullException(nameof(objectToTrack));
            }

            trackers = new List<IPropertyTracker>();
            foreach (string item in propertyNamesToTrack)
            {
                PropertyTracker<T1, T2> tracker = 
                    new PropertyTracker<T1, T2>(originalObjectToCompare, objectToTrack, item);
                tracker.ValuesChecked += OnTrackerValueChecked;
                trackers.Add(tracker);
            }
            CheckState();
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
            private set { SetProperty(ref sameState, value); }
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
        #endregion

        #region events
        #endregion

        #region event handlers
        private void OnTrackerValueChecked(object sender, EventArgs e)
        {
            CheckState();
        }
        #endregion
    }
}