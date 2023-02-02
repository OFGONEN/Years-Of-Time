/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using FFStudio;
using UnityEngine;

public class TestDictionary : MonoBehaviour
{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
    private void Start()
    {
		Dictionary<int, string> dic = new Dictionary<int, string>();


        for( var x = 0; x < 100; x++ )
        {
            for( var y = 0; y < 100; y++ )
            {
				dic.Add( new Vector2Int( x, y ).GetCustomHashCode(), $"foo:{x},{y}" );
			}
        }

        foreach( var element in dic.Values )
        {
            FFLogger.Log( element );
        }
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