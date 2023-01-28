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
    [ SerializeField ] Vector2Int item_coordinate_array;
    [ SerializeField ] int item_index;

  [ Title( "Shared" ) ]
    [ SerializeField ] ListItem list_item;

	List< ClockSlot > clock_slot_list = new List< ClockSlot >( 2 );
    UnityMessage onUpdate;
#endregion

#region Properties
#endregion

#region Unity API
    private void OnEnable()
    {
		list_item.AddDictionary( item_coordinate_array.GetCustomHashCode(), this );

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
	public void AssignClockSlot( ClockSlot clockSlot )
	{
		clock_slot_list.Add( clockSlot );

		if( clock_slot_list.Count == 2 )
			StartProduction();
	}

	public void RemoveClockSlot( ClockSlot clockSlot )
	{
#if UNITY_EDITOR
		var removed = clock_slot_list.Remove( clockSlot );
		if( !removed )
			FFLogger.Log( name + " - Clock slot could not removed", clockSlot );
#else
		clock_slot_list.Remove( clockSlot );
#endif
		StopProduction();
	}
#endregion

#region Implementation
	void StartProduction()
	{
		FFLogger.Log( "Start Production", this );
	}

	void StopProduction()
	{
		FFLogger.Log( "Stop Production", this );
	}

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