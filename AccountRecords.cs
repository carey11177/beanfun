using System;
using System.Collections.Generic;

namespace Beanfun
{
		[Serializable]
	internal class AccountRecords
	{
				public AccountRecords()
		{
		}

				public List<string> regionList;

				public List<string> accountList;

				public List<string> passwdList;

				public List<string> verifyList;

				public List<int> methodList;

				public List<bool> autoLoginList;
	}
}
