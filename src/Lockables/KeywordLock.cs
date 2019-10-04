using System.Collections.Generic;

namespace Lockables
{
	public class KeywordLock
	{
		private HashSet<string> lockingKeyword = new HashSet<string>();
		private ILockable lockable;

		public KeywordLock(ILockable lockable)
		{
			this.lockable = lockable;
		}

		public bool IsLocked
		{
			get { return lockable.IsLocked(); }
		}

		public void Lock(string keyword)
		{
			if (lockingKeyword.Count == 0) {
				lockingKeyword.Add(keyword);
				lockable.Lock();
			}
			else {
				lockingKeyword.Add(keyword);
			}
		}

		public void Unlock(string keyword)
		{
			if(lockingKeyword.Count == 0)
				return;
			;
			lockingKeyword.Remove(keyword);
			if(lockingKeyword.Count == 0)
				lockable.Unlock();
		}

		public void UnlockAll()
		{
			if(lockingKeyword.Count == 0)
				return;
			
			lockingKeyword.Clear();
			lockable.Unlock();
		}
	}
}
