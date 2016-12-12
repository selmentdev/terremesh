using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terremesh.IDE
{
    /// <summary>
    /// Progress listener interface.
    /// </summary>
    interface IProgressListener
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stage">The name of the stage.</param>
        void OnStart(string stage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="current">The current progress value.</param>
        /// <param name="total">The total progress value.</param>
        void OnProgress(long current, long total);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stage">The name of the stage.</param>
        void OnComplete(string stage);
        #endregion
    }
}
