using System;
using System.Threading;
using System.Threading.Tasks;

namespace LastSeenWeb.Core.Extensions
{
	public static class SemaphoreSlimExtensions
	{
		public static int SafeRelease(this SemaphoreSlim semaphoreSlim)
		{
			try
			{
				return semaphoreSlim.Release();
			}
			catch
			{
				// Ignore
			}

			return -1;
		}

		// IMPORTANT
		// Needs to be awaited in using() or Task is returned
		// instead of Disposable -> no Dispose = stuck
		public static async Task<IDisposable> DisposableWaitAsync(this SemaphoreSlim semaphoreSlim, TimeSpan timeout)
		{
			var miliseconds = (int)timeout.TotalMilliseconds;
			if (timeout == TimeSpan.MaxValue)
			{
				miliseconds = Int32.MaxValue; // Max for WaitAsync
			}
			await semaphoreSlim.WaitAsync(miliseconds).ConfigureAwait(false);

			return new Disposable(semaphoreSlim);
		}

		private class Disposable : IDisposable
		{
			private readonly SemaphoreSlim _semaphoreSlim;
			private bool _isDisposed;

			public Disposable(SemaphoreSlim semaphoreSlim)
			{
				_semaphoreSlim = semaphoreSlim;
			}

			public void Dispose()
			{
				if (_isDisposed)
				{
					return;
				}
				_isDisposed = true;

				_semaphoreSlim.SafeRelease();
			}
		}
	}
}
