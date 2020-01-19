using System;

namespace JMI.General.Logging
{
    public sealed class SingletonLogger : Logger
    {
        #region constructors
        #endregion

        #region properties
        private static readonly Lazy<SingletonLogger> lazy = new Lazy<SingletonLogger>(() => new SingletonLogger());
        public static SingletonLogger Instance { get { return lazy.Value; } }
        #endregion

        #region methods
        #endregion

        #region events
        #endregion

        #region event handlers
        #endregion
    }
}
