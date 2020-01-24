using System;
using System.Collections;
using UniRx;

namespace Lockables {

	public class CountingLockable : ILockable {
		int counter = 0;
		ILockable lockable;
		Subject<bool> lockUpdatedSubject;

		public CountingLockable (ILockable lockable) {
			this.lockable = lockable;
			lockUpdatedSubject = new Subject<bool> ();
		}

		public int Count {
			get { return counter; }
		}

		public void Lock () {
			if (counter == 0) {
				lockable.Lock ();
				counter++;
				lockUpdatedSubject.OnNext (true);
			} else {
				counter++;
			}
		}

		public void Unlock () {
			counter--;
			if (counter == 0) {
				lockable.Unlock ();
				lockUpdatedSubject.OnNext (false);
			}
		}

		public void ForceUnlock () {
			if(counter == 0)
				return;
			
			counter = 0;
			lockable.Unlock();
			lockUpdatedSubject.OnNext (false);
		}

		public bool IsLocked () {
			return counter > 0;
		}

		public IObservable<bool> OnLockUpdatedAsObservable () {
			return lockUpdatedSubject;
		}

		public override string ToString () {
			return string.Format ("[CountingLockable: counter={0}, lockable={1}]", counter, lockable);
		}
	}

}