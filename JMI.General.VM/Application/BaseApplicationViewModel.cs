using JMI.General.VM.Logging;
using System.Windows;

namespace JMI.General.VM.Application
{
    public abstract class BaseApplicationViewModel : ObservableObject
    {
        #region constructors
        public BaseApplicationViewModel()
        {
            Log = new LogViewModel();
        }
        #endregion

        #region properties
        public abstract string WindowTitle { get; }

        #region window settings
        private double windowHeight;
        public double WindowHeight
        {
            get { return windowHeight; }
            set { SetProperty(ref windowHeight, value); }
        }

        private double windowWidht;
        public double WindowWidht
        {
            get { return windowWidht; }
            set { SetProperty(ref windowWidht, value); }
        }

        private double windowTop;
        public double WindowTop
        {
            get { return windowTop; }
            set { SetProperty(ref windowTop, value); }
        }

        private double windowLeft;
        public double WindowLeft
        {
            get { return windowLeft; }
            set { SetProperty(ref windowLeft, value); }
        }

        private WindowState windowState;
        public WindowState WindowState
        {
            get { return windowState; }
            set { SetProperty(ref windowState, value); }
        }

        private GridLength rowHeightTop;
        public GridLength RowHeightTop
        {
            get { return rowHeightTop; }
            set { SetProperty(ref rowHeightTop, value); }
        }

        private GridLength rowHeightBottom;
        public GridLength RowHeightBottom
        {
            get { return rowHeightBottom; }
            set { SetProperty(ref rowHeightBottom, value); }
        }
        #endregion window settings

        private LogViewModel log;
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
