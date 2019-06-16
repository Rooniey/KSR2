
using System.Threading.Tasks;

namespace GUI.Base
{
     public static class TaskUtility
    {
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (System.Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }
}
