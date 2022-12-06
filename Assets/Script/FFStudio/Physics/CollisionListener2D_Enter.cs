/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public class CollisionListener2D_Enter : CollisionListener2D
	{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
        void OnCollisionEnter2D( Collision2D collision )
        {
			InvokeEvent( collision );
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