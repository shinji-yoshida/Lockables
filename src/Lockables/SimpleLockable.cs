using System.Collections;
using System;

namespace Lockables {

	public class SimpleLockable : ILockable {
		bool locked = false;
		Action onLocked;
		Action onUnlocked;

		public SimpleLockable (Action onLocked, Action onUnlocked) {
			this.onLocked = onLocked;
			this.onUnlocked = onUnlocked;
		}

		public void Lock () {
			locked = true;
			onLocked();
		}

		public void Unlock () {
			locked = false;
			onUnlocked();
		}

		public bool IsLocked () {
			return locked;
		}
	}

}