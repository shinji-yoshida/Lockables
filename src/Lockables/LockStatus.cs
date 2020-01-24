using System;
using UniRx;


namespace Lockables {
	public interface LockStatus {
		bool IsLocked();
		IObservable<bool> OnLockUpdatedAsObservable();
	}
}
