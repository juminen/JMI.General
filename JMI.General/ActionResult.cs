using System;
using System.Collections.ObjectModel;
using System.Text;

namespace JMI.General
{
    public class ActionResult<T> : ObservableObject
    {
        public ActionResult()
        {
            successful = true;
            comments = new ObservableCollection<string>();
            Comments = new ReadOnlyObservableCollection<string>(comments);
        }

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

    }
}
