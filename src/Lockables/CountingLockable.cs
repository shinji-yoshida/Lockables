using System.Collections;

namespace Lockables {

	public class CountingLockable : ILockable {
		int counter = 0;
		ILockable lockable;

		public CountingLockable (ILockable lockable) {
			this.lockable = lockable;
		}
		

		public void Lock () {
			if(counter == 0)
				lockable.Lock();
			counter++;
		}

		public void Unlock () {
			counter--;
			if(counter == 0)
				lockable.Unlock();
		}

		public void ForceUnlock () {
			if(counter == 0)
				return;
			counter = 0;
			lockable.Unlock();
		}

		public bool IsLocked () {
			return counter > 0;
		}

		public override string ToString () {
			return string.Format ("[CountingLockable: counter={0}, lockable={1}]", counter, lockable);
		}
	}

}