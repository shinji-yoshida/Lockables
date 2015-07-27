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
			if(locked)
				throw new System.Exception("already locked");

			locked = true;
			onLocked();
		}

		public void Unlock () {
			if(! locked)
				throw new System.Exception("already unlocked");

			locked = false;
			onUnlocked();
		}

		public void ForceUnlock () {
			if(! locked)
				return;
			Unlock();
		}

		public bool IsLocked () {
			return locked;
		}
	}

}