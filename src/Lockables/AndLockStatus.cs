using System;
using UniRx;
using System.Collections.Generic;
using System.Linq;

namespace Lockables {
	public class AndLockStatus : LockStatus {
		List<LockStatus> statuses = new List<LockStatus>();
		bool locked;
		Subject<bool> lockUpdatedSubject = new Subject<bool>();

		public void Add(LockStatus status) {
			statuses.Add (status);
			UpdateLockStatus ();
			status.OnLockUpdatedAsObservable ().Subscribe (_ => UpdateLockStatus ());
		}

		void UpdateLockStatus () {
			var current = statuses.All (s => s.IsLocked ());
			if (this.locked != current) {
				this.locked = current;
				lockUpdatedSubject.OnNext (current);
			}
		}

		public bool IsLocked () {
			return locked;
		}

		public IObservable<bool> OnLockUpdatedAsObservable () {
			return lockUpdatedSubject;
		}
	
	}
}
