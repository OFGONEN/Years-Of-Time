/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	public class RunTimeIntWrapper : IntWrapper
	{
#region Fields
        [ SerializeField ] RunTimeInt runTimeInt;
#endregion

#region Properties
		public override int Data => runTimeInt.runTimeData;
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}