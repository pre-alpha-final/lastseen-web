using LastSeenWeb.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LastSeenWeb.Tests
{
	public class SemaphoreSlimExtensionsTests
	{
		[Fact]
		public async Task DisposableWaitAsync_ShouldHandleMultipleTasks()
		{
			const int taskCount = 1000;

			var semaphoreSlim = new SemaphoreSlim(1);
			var tasks = new List<Task<bool>>();
			for (var count = 0; count < taskCount; count++)
			{
				tasks.Add(Task.Run(() => DelayAsync(semaphoreSlim)));
			}
			var taskResults = await Task.WhenAll(tasks.ToArray());

			Assert.All(taskResults, Assert.True);
		}

		private static async Task<bool> DelayAsync(SemaphoreSlim semaphoreSlim)
		{
			await Task.Delay(1000);
			var random = new Random();

			using (await semaphoreSlim.DisposableWaitAsync(TimeSpan.MaxValue))
			{
				await Task.Delay(random.Next(1, 10));

				return true;
			}
		}
	}
}
