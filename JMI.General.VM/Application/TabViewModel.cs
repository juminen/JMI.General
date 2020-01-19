namespace JMI.General.VM.Application
{
    /// <summary>
    /// Class for tabs used in user interface
    /// </summary>
    public class TabViewModel<T> : CloseViewModel where T : class
    {
        #region constructors
        #endregion

        #region properties
        private string displayText;
        /// <summary>
        /// Text displayed in tab header
        /// </summary>
        public string DisplayText
        {
            get { return displayText; }
            set { SetProperty(ref displayText, value); }
        }

        private T tabContent;
        /// <summary>
        /// Content for the tab (usually a viewmodel class)
        /// </summary>
        public T TabContent
        {
            get { return tabContent; }
            set { SetProperty(ref tabContent, value); }
        }

        #endregion

        #region commands
        #endregion

        #region methods
        #endregion

        #region events

        #endregion

        #region event handlers
        #endregion
    }
}
