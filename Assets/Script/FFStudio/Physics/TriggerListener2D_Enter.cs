/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class TriggerListener2D_Enter : TriggerListener2D
	{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
        void OnTriggerEnter2D( Collider2D other )
        {
			InvokeEvent( other );
		}
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