using System.Collections;
using System;
using UniRx;

namespace Lockables {

	public class SimpleLockable : AbstractLockable {
		Action onLocked;
		Action onUnlocked;

		public SimpleLockable (Action onLocked, Action onUnlocked) : base() {
			this.onLocked = onLocked;
			this.onUnlocked = onUnlocked;
		}

		public override void Lock () {
			if(locked)
				throw new System.Exception("already locked");

			DoLock ();
		}
		protected override void OnLocked () {
			onLocked ();
		}

		protected override void OnUnlocked () {
			onUnlocked ();
		}

		public override void Unlock () {
			if(! locked)
				throw new System.Exception("already unlocked");

			DoUnlock ();
		}

		public override void ForceUnlock () {
			if(! locked)
				return;
			Unlock();
		}
	}

}