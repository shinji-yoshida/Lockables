﻿using System.Collections;
using System.Collections.Generic;

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

		public bool IsLocked () {
			return original.IsLocked();
		}

		class Composite : ILockable {
			List<ILockable> lockables = new List<ILockable>();
			bool locked = false;

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
				locked = true;
				foreach(var each in lockables)
					each.Lock();
			}

			public void Unlock () {
				locked = false;
				foreach(var each in lockables)
					each.Unlock();
			}

			public bool IsLocked () {
				return locked;
			}
		}
	}

}