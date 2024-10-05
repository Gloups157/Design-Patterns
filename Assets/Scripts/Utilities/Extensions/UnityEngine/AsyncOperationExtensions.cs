using System.Threading.Tasks;

namespace UnityEngine
{
    /// <summary>
    /// Provides extension methods for <see cref="AsyncOperation"/> to support asynchronous programming with tasks.
    /// </summary>
    public static class AsyncOperationExtensions
    {
        /// <summary>
        /// Converts an <see cref="AsyncOperation"/> to a <see cref="Task"/>.
        /// </summary>
        /// <param name="asyncOperation">The <see cref="AsyncOperation"/> to be converted.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <remarks>
        /// This method allows you to use an <see cref="AsyncOperation"/> with C#'s async/await pattern.
        /// The task will be completed when the <see cref="AsyncOperation"/> is finished.
        /// </remarks>
        public static Task AsTask(this AsyncOperation asyncOperation)
        {
            TaskCompletionSource<bool> taskCompletionSource = new();
            asyncOperation.completed += _ => taskCompletionSource.SetResult(true);
            return taskCompletionSource.Task;
        }
    }
}