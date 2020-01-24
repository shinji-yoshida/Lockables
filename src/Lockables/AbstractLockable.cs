using System;
using UniRx;


namespace Lockables {
	public abstract class AbstractLockable : ILockable {
		protected bool locked;
		Subject<bool> lockUpdatedSubject;

		public AbstractLockable () {
			locked = false;
			lockUpdatedSubject = new Subject<bool> ();
		}

		public abstract void Lock ();

		protected void DoLock() {
			locked = true;
			OnLocked ();
			lockUpdatedSubject.OnNext (true);
		}

		protected virtual void OnLocked () {
		}

		public abstract void Unlock ();

		protected void DoUnlock() {
			locked = false;
			OnUnlocked();
			lockUpdatedSubject.OnNext (false);
		}

		protected virtual void OnUnlocked () {
		}

		public abstract void ForceUnlock ();

		public bool IsLocked () {
			return locked;
		}

		public IObservable<bool> OnLockUpdatedAsObservable () {
			return lockUpdatedSubject;
		}
	}
}
