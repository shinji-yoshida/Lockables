
namespace Lockables {
	public class EmptyLockable : AbstractLockable {
		public override void Lock () {
			if(locked)
				throw new System.Exception("already locked");

			DoLock ();
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
