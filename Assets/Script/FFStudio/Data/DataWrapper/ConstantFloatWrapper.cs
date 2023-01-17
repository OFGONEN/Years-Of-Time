/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	public class RunTimeFloatWrapper : FloatWrapper
	{
#region Fields
        [ SerializeField ] RunTimeFloat runTimeFloat;
#endregion

#region Properties
		public override float Data => runTimeFloat.runTimeData;
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