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
		Dictionary<Vector2Int, string> dic = new Dictionary<Vector2Int, string>();

		dic.Add( new Vector2Int( 0, 0 ), "foo" );

		string value;

		var validValue = dic.TryGetValue( new Vector2Int( 0, 0 ), out value );

        if( validValue )
            FFLogger.Log( "Value: " + value );
        else
            FFLogger.Log( "No Valid Value" );
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
