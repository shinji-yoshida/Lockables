using System.Collections;
using System;


namespace Lockables {

	public static class Lockable {
		public static ILockable Create(Action onLocked, Action onUnlocked) {
			return new SimpleLockable(onLocked, onUnlocked);
		}
	}

}
