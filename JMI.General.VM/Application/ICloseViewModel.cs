using System;

namespace JMI.General.VM.Application
{
    /// <summary>
    /// Provides mechanism for user interface items that are closable (eg. tab, editing form etc).
    /// </summary>
    public interface ICloseViewModel: IDisposable
    {
        /// <summary>
        /// Use this property to enable <see cref="CloseCommand"/>.
        /// </summary>
        bool AllowClose { get; }
        /// <summary>
        /// Set message if <see cref="AllowClose"/> is false.
        /// </summary>
        string CloseFailReason { get; }
        /// <summary>
        /// Enable command by <see cref="AllowClose"/> and create method for 
        /// executing closing request in implementing class.
        /// </summary>
        RelayCommand CloseCommand { get; }
        /// <summary>
        /// Execute event in implementing class when close is requested.
        /// </summary>
        event EventHandler CloseRequested;
    }
}