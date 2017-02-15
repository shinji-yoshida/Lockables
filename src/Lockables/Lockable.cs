using System.Collections;
using System;


namespace Lockables {

	public static class Lockable {
		public static ILockable Create(Action onLocked, Action onUnlocked) {
			return new SimpleLockable(onLocked, onUnlocked);
		}

		public static CountingLockable CreateCounting(Action onLocked, Action onUnlocked) {
			return new CountingLockable(Create(onLocked, onUnlocked));
		}

		public static CompositeLockable Composite(params ILockable[] lockables) {
			var result = new CompositeLockable();
			foreach(var each in lockables)
				result.Add(each);
			return result;
		}

		public static IDisposable WithLock(this ILockable lockable, Action<IDisposable> action) {
			var singleLock = new Lock(lockable);
			action(singleLock);
			return singleLock;
		}

		public static IDisposable DisposableLock(this ILockable lockable) {
			return new Lock(lockable);
		}
	}

}
