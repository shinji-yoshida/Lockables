using System.Collections;

namespace Lockables {

	public interface ILockable {
		void Lock();
		void Unlock();
		bool IsLocked();
	}

}