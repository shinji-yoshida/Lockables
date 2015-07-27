using UnityEngine;
using System.Collections;
using System;

namespace Lockables {
	public class Lock : IDisposable {
		ILockable lockable;

		public Lock (ILockable lockable) {
			this.lockable = lockable;
			lockable.Lock();
		}
		
		public void Dispose () {
			if(lockable == null)
				return;

			lockable.Unlock();
			lockable = null;
		}
	}
}
