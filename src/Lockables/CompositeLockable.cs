using System.Collections;
using System.Collections.Generic;
using UniRx;

namespace Lockables {

	public class CompositeLockable : ILockable {
		Composite composite;
		CountingLockable original;

		public CompositeLockable() {
			composite = new Composite();
			original = new CountingLockable(composite);
		}

		public void Add(ILockable lockable) {
			composite.Add(lockable);
		}

		public void Remove(ILockable lockable) {
			composite.Remove(lockable);
		}

		public void Lock () {
			original.Lock();
		}

		public void Unlock () {
			original.Unlock();
		}

		public void ForceUnlock () {
			original.ForceUnlock();
		}

		public bool IsLocked () {
			return original.IsLocked();
		}

		public IObservable<bool> OnLockUpdatedAsObservable () {
			return original.OnLockUpdatedAsObservable ();
		}

		class Composite : ILockable {
			List<ILockable> lockables = new List<ILockable>();
			bool locked = false;
			Subject<bool> lockUpdatedSubject = new Subject<bool>();

			public void Add(ILockable lockable) {
				if(locked)
					lockable.Lock();

				lockables.Add(lockable);
			}

			public void Remove(ILockable lockable) {
				if(locked)
					lockable.Unlock();

				lockables.Remove(lockable);
			}

			public void Lock () {
				if(locked)
					throw new System.Exception("already locked");

				locked = true;
				foreach(var each in lockables)
					each.Lock();

				lockUpdatedSubject.OnNext (locked);
			}

			public void Unlock () {
				if(! locked)
					throw new System.Exception("already unlocked");

				locked = false;
				foreach(var each in lockables)
					each.Unlock();

				lockUpdatedSubject.OnNext (locked);
			}

			public void ForceUnlock () {
				if(! locked)
					return;
				Unlock();
			}

			public bool IsLocked () {
				return locked;
			}

			public IObservable<bool> OnLockUpdatedAsObservable () {
				return lockUpdatedSubject;
			}
		}
	}

}