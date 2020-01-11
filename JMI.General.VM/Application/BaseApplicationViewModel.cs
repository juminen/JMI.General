using JMI.General.VM.Logging;
using System.Windows;

namespace JMI.General.VM.Application
{
    /// <summary>
    /// Base class for application view
    /// </summary>
    public class BaseApplicationViewModel : ObservableObject
    {
        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseApplicationViewModel()
        {
            Log = new LogViewModel();
        }
        #endregion

        #region properties
        private string windowTitle;
        /// <summary>
        /// Text displayed in window title.
        /// </summary>
        public string WindowTitle
        {
            get { return windowTitle; }
            protected set { SetProperty(ref windowTitle, value); }
        }

        #region window settings
        private double windowHeight;
        /// <summary>
        /// For storing/restoring window height
        /// </summary>
        public double WindowHeight
        {
            get { return windowHeight; }
            set { SetProperty(ref windowHeight, value); }
        }

        private double windowWidht;
        /// <summary>
        /// For storing/restoring window widht
        /// </summary>
        public double WindowWidht
        {
            get { return windowWidht; }
            set { SetProperty(ref windowWidht, value); }
        }

        private double windowTop;
        /// <summary>
        /// For storing/restoring window top location
        /// </summary>
        public double WindowTop
        {
            get { return windowTop; }
            set { SetProperty(ref windowTop, value); }
        }

        private double windowLeft;
        /// <summary>
        /// For storing/restoring window left location
        /// </summary>
        public double WindowLeft
        {
            get { return windowLeft; }
            set { SetProperty(ref windowLeft, value); }
        }

        private WindowState windowState;
        /// <summary>
        /// For storing/restoring window state
        /// </summary>
        public WindowState WindowState
        {
            get { return windowState; }
            set { SetProperty(ref windowState, value); }
        }

        private GridLength rowHeightTop;
        /// <summary>
        /// For storing/restoring top row height (main window part)
        /// </summary>
        public GridLength RowHeightTop
        {
            get { return rowHeightTop; }
            set { SetProperty(ref rowHeightTop, value); }
        }

        private GridLength rowHeightBottom;
        /// <summary>
        /// For storing/restoring bottom row height (log window part)
        /// </summary>
        public GridLength RowHeightBottom
        {
            get { return rowHeightBottom; }
            set { SetProperty(ref rowHeightBottom, value); }
        }
        #endregion window settings

        private LogViewModel log;
        /// <summary>
        /// For logging part of the window
        /// </summary>
        public LogViewModel Log
        {
            get { return log; }
            protected set { SetProperty(ref log, value); }
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
