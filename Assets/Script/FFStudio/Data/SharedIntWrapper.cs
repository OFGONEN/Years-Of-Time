/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	[ System.Serializable ]
	public class SharedIntWrapper : IntDataWrapper
	{
#region Fields
        [ SerializeField ] SharedIntNotifier sharedIntNotifier;
#endregion

#region Properties
		public override int Data => sharedIntNotifier.sharedValue;
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