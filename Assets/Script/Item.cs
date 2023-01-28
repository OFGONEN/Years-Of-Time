/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using Shapes;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] Vector2Int item_coordinate;
    [ SerializeField ] int item_index;
    [ SerializeField ] ItemData item_data;

  [ Title( "Shared" ) ]
    [ SerializeField ] ListItem list_item;

  [ Title( "Components" ) ]
    [ SerializeField ] Rectangle item_background;
    [ SerializeField ] Image item_image_background;
    [ SerializeField ] Image item_image_foreground;

	List< ClockSlot > clock_slot_list = new List< ClockSlot >( 2 );
    UnityMessage onUpdate;
#endregion

#region Properties
#endregion

#region Unity API
    private void OnEnable()
    {
		list_item.AddDictionary( item_coordinate.GetCustomHashCode(), this );
    }

	private void OnDisable()
	{
		list_item.RemoveDictionary( item_coordinate.GetCustomHashCode() );
	}

    private void Awake()
    {
		EmptyDelegates();
		UpdateVisual(); //todo remove this line
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
	void UpdateVisual()
	{
	}

	void StartProduction()
	{

	}

	void StopProduction()
	{
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