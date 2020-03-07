namespace JMI.General.ChangeTracking
{
    public interface ICustomStateTracker : IStateTracker
    {
        void AddTracking<T1, T2>(
            T1 originalObjectToCompare,
            string originalPropertyName,
            T2 objectToTrack,
            string trackedPropertyName)
            where T1 : ObservableObject
            where T2 : ObservableObject;
    }
}