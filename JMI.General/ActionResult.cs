using System;
using System.Collections.ObjectModel;
using System.Text;

namespace JMI.General
{
    /// <summary>
    /// This class is used to communicate about method result.<br/>
    /// Result is put in property <see cref="Result"/>.<br/>
    /// If result was ok property <see cref="Successful"/> can be set true (default value is true).<br/>
    /// If something fails, reason (string) can can be added by using method <see cref="AddFailReason(string)"/>.
    /// This sets the property <see cref="Successful"/> to false.<br/>
    /// Reasons can be added also using method <see cref="AddReason(string)"/>.<br/><br/>
    /// Summary (string) from the reasons can get by using methods <see cref="GetComments(string)"/> 
    /// and <see cref="GetComments"/>.
    /// </summary>
    /// <typeparam name="T">Any kind of object</typeparam>
    public class ActionResult<T> : ObservableObject
    {
        #region constructors
       /// <summary>
       /// Default constructor.
       /// </summary>
       /// <remarks>Property <see cref="Successful"/> is set to true.</remarks>
        public ActionResult()
        {
            successful = true;
            comments = new ObservableCollection<string>();
            Comments = new ReadOnlyObservableCollection<string>(comments);
        }

        public ActionResult(T result) : this()
        {
            Result = result;
        }
        #endregion

        #region properties
        private T result;
        public T Result
        {
            get { return result; }
            set { SetProperty(ref result, value); }
        }

        private bool successful;
        public bool Successful
        {
            get { return successful; }
            set { SetProperty(ref successful, value); }
        }

        private ObservableCollection<string> comments;
        public ReadOnlyObservableCollection<string> Comments;
        #endregion

        #region methods
        /// <summary>
        /// Adds string to Comments and marks successful to false
        /// </summary>
        /// <param name="reason"></param>
        public virtual void AddFailReason(string reason)
        {
            if (!string.IsNullOrWhiteSpace(reason))
            {
                successful = false;
                comments.Add(reason);
            }
        }

        public void AddReason(string reason)
        {
            if (!string.IsNullOrWhiteSpace(reason))
            {
                comments.Add(reason);
            }
        }

        /// <summary>
        /// Creates string from comments separated by separator
        /// </summary>
        /// <param name="separator">Separator that separates reasons</param>
        /// <returns>string</returns>
        public string GetComments(string separator)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < comments.Count; i++)
            {
                sb.Append(comments[i]);
                if (i < comments.Count - 1)
                {
                    sb.Append(separator);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Creates string from comments separated by line feed
        /// </summary>
        /// <returns>string</returns>
        public string GetComments()
        {
            return GetComments(Environment.NewLine);
        }
        #endregion

        #region events
        #endregion

        #region event handlers
        #endregion







    }
}
