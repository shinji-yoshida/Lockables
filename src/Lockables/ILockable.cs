using System.Collections;

namespace Lockables {

	public interface ILockable : LockStatus {
		void Lock();
		void Unlock();
		void ForceUnlock();
	}

}