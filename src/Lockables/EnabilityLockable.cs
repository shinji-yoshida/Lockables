using UniRx;

namespace Lockables {
	public class EnabilityLockable : AbstractLockable {
		bool enabled;
		Subject<bool> enabilityUpdatedSubject;

		public EnabilityLockable (bool enabled) {
			this.enabled = enabled;
			enabilityUpdatedSubject = new Subject<bool> ();
		}

		public bool UnlockedEnabled {
			get {
				return enabled && ! IsLocked();
			}
		}

		public IObservable<bool> OnEnabilityUpdatedAsObservable() {
			return enabilityUpdatedSubject;
		}

		public void Enable() {
			var prev = UnlockedEnabled;
			enabled = true;
			if (prev != UnlockedEnabled)
				enabilityUpdatedSubject.OnNext (UnlockedEnabled);
		}

		public void Disable() {
			var prev = UnlockedEnabled;
			enabled = false;
			if (prev != UnlockedEnabled)
				enabilityUpdatedSubject.OnNext (UnlockedEnabled);
		}

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

		protected override void OnLocked () {
			if (enabled)
				enabilityUpdatedSubject.OnNext (UnlockedEnabled);
		}

		protected override void OnUnlocked () {
			if (enabled)
				enabilityUpdatedSubject.OnNext (UnlockedEnabled);
		}

		public override void ForceUnlock () {
			if(! locked)
				return;
			Unlock();
		}
	}
}
