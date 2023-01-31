/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FFStudio;

public class MultiplySharedNotifierFloat : MonoBehaviour
{
#region Fields
    [ SerializeField ] UnityEvent< float > onMultiply;

    List< SharedFloatNotifier > notif_float_list = new List< SharedFloatNotifier >( 4 );
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Clear()
    {
		notif_float_list.Clear();
	}

    public void Add( SharedFloatNotifier sharedFloatNotifier )
    {
		notif_float_list.Add( sharedFloatNotifier );
	}

    public void Multiply()
    {
		float total = 1;
        for( var i = 0; i < notif_float_list.Count; i++ )
			total *= notif_float_list[ i ].sharedValue;

		onMultiply.Invoke( total );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
