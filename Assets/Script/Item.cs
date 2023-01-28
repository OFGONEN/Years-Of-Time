/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Item : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] Vector2Int[] item_coordinate_array;
    [ SerializeField ] int item_index;

  [ Title( "Shared" ) ]
    [ SerializeField ] ListItem list_item;

    UnityMessage onUpdate;
#endregion

#region Properties
#endregion

#region Unity API
    private void OnEnable()
    {
        for( var i = 0; i < item_coordinate_array.Length; i++ )
        {
			list_item.AddDictionary( item_coordinate_array[ i ].GetCustomHashCode(), this );
		}
    }

    private void Awake()
    {
		EmptyDelegates();
		//todo Invisible, Unlock, ReadyToUnlock, ReadyToProduce    
	}

    private void Update()
    {
		onUpdate();
	}
#endregion

#region API
#endregion

#region Implementation
    void EmptyDelegates()
    {
		onUpdate = ExtensionMethods.EmptyMethod;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
