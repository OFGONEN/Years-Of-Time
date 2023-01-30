/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Shapes;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] Vector2Int item_coordinate;
    [ SerializeField ] int item_index;
    [ SerializeField ] ItemData item_data;
	[ SerializeField ] UnityEvent item_event_onUnlock;
	[ SerializeField ] Currency currency;

  [ Title( "Shared" ) ]
    [ SerializeField ] ListItem list_item_coordinate;
    [ SerializeField ] ListItem list_item_index;
    [ SerializeField ] Currency notif_currency;
    [ SerializeField ] IncomeCofactor notif_income_cofactor;
    [ SerializeField ] SaveSystem system_save;
    [ SerializeField ] ParticleSpawnEvent event_particle_spawn;

  [ Title( "Components" ) ]
    [ SerializeField ] Rectangle item_background;
    [ SerializeField ] Image item_image_background;
    [ SerializeField ] Image item_image_foreground;
    [ SerializeField ] Transform item_image_parent;

	ItemState item_state;
	float item_duration;
	Color item_background_color;
	[ ShowInInspector, ReadOnly ] Vector3 item_scale;

	RecycledTween recycledTween_Color = new RecycledTween();
	RecycledTween recycledTween_Scale = new RecycledTween();
	List< ClockSlot > clock_slot_list = new List< ClockSlot >( 2 );
    UnityMessage onUpdate;
	ClockMessage onClockAssign;
	ClockMessage onClockRemove;
#endregion

#region Properties
	public ItemData ItemData   => item_data;
	public int ItemIndex       => item_index;
	public ItemState ItemState => item_state;
#endregion

#region Unity API
	private void OnEnable()
	{
		list_item_coordinate.AddDictionary( item_coordinate.GetCustomHashCode(), this );
		list_item_index.AddDictionary( item_index, this );
	}
	private void OnDisable()
	{
		list_item_coordinate.RemoveDictionary( item_coordinate.GetCustomHashCode() );
		list_item_index.RemoveDictionary( item_index );
	}

    private void Awake()
    {
		EmptyDelegates();
		onClockAssign = AssignClockSlotLocked;
		onClockRemove = RemoveClockSlotLocked;

		item_scale = item_image_parent.localScale;

		item_background_color = item_background.Color;

		// Load item state

		item_state = ItemState.Invisible;
		var data = (ItemState)system_save.SaveData.item_array[ item_index ];

		if( data == ItemState.Locked )
			StartAsLocked();
		else if( data == ItemState.Unlocked )
			StartAsUnlocked();
	}

    private void Update()
    {
		onUpdate();
	}
#endregion

#region API
	public void OnPlayAreaSizeChange( int size )
	{
		if( ( size == item_coordinate.x && item_coordinate.y <= size ) || 
			( item_coordinate.x <= size && size == item_coordinate.y ) )
			StartAsLocked();
	}

	public void OnAssignClockSlot( ClockSlot clockSlot )
	{
		onClockAssign( clockSlot );
	}

	public void OnRemoveClockSlot( ClockSlot clockSlot )
	{
		onClockRemove( clockSlot );
	}

	[ Button() ]
	public void Unlock()
	{
		event_particle_spawn.Raise( "item_unlock", transform.position );
		StartAsUnlocked();

		notif_currency.SharedValue -= item_data.ItemCost;
		item_event_onUnlock.Invoke();

		if( clock_slot_list.Count == 2 )
			StartProduction();
	}

	public void StartAsLocked()
	{
		item_state = ItemState.Locked;

		item_image_background.enabled = true;
		item_image_background.sprite  = GameSettings.Instance.item_locked_sprite;
	}
#endregion

#region Implementation
	void StartAsUnlocked()
	{
		item_background.enabled       = true;
		item_image_background.enabled = true;
		item_image_foreground.enabled = true;

		item_state = ItemState.Unlocked;

		UpdateVisual();

		onClockAssign = AssignClockSlotUnlocked;
		onClockRemove = RemoveClockSlotUnlocked;
	}
	
	void AssignClockSlotUnlocked( ClockSlot clockSlot )
	{
		clock_slot_list.Add( clockSlot );

		if( clock_slot_list.Count == 2 )
			StartProduction();
	}

	void RemoveClockSlotUnlocked( ClockSlot clockSlot )
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

	void AssignClockSlotLocked( ClockSlot clockSlot )
	{
		clock_slot_list.Add( clockSlot );
	}

	void RemoveClockSlotLocked( ClockSlot clockSlot )
	{
#if UNITY_EDITOR
		var removed = clock_slot_list.Remove( clockSlot );
		if( !removed )
			FFLogger.Log( name + " - Clock slot could not removed", clockSlot );
#else
		clock_slot_list.Remove( clockSlot );
#endif
	}

	void OnProduction()
	{
		item_duration += Time.deltaTime * GetCurrentClockSpeed();
		item_image_foreground.fillAmount = Mathf.Lerp( item_data.ItemSpriteFillBottom, item_data.ItemSpriteFillTop, item_duration / item_data.ItemDuration );

		if( item_duration > item_data.ItemDuration )
			OnItemProduced();
	}

	void OnItemProduced()
	{
		item_duration = 0;
		var moneyGain = item_data.ItemCurrency * notif_income_cofactor.sharedValue;

		notif_currency.SharedValue += moneyGain;

		item_image_parent.localScale = item_scale;
		recycledTween_Scale.Recycle( GameSettings.Instance.item_produce_tween_punchScale.CreateTween( item_image_parent ) );
	}

	void UpdateVisual()
	{
		item_image_background.sprite = item_data.ItemSpriteBackground;
		item_image_foreground.sprite = item_data.ItemSpriteForeground;
	}

	void StartProduction()
	{
		item_background.Color = GameSettings.Instance.item_produce_start_color;
		TweenBackgroundColorToDefault();

		onUpdate = OnProduction;
	}

	void StopProduction()
	{
		item_background.Color = GameSettings.Instance.item_produce_stop_color;
		TweenBackgroundColorToDefault();

		onUpdate = ExtensionMethods.EmptyMethod;
	}

	void TweenBackgroundColorToDefault()
	{
		recycledTween_Color.Recycle( DOTween.To(
			GetBackgroundColor, SetBackgroundColor, item_background_color, GameSettings.Instance.item_produce_duration )
			.SetEase( GameSettings.Instance.item_produce_ease ) );
	}

	void EmptyDelegates()
    {
		onUpdate      = ExtensionMethods.EmptyMethod;
		onClockAssign = EmptyMethod;
		onClockRemove = EmptyMethod;
	}

	float GetCurrentClockSpeed()
	{
		float speed = 0;

		for( var i = 0; i < clock_slot_list.Count; i++ )
		{
			speed += clock_slot_list[ i ].CurrentClockSpeed();
		}

		return speed;
	}

	Color GetBackgroundColor()
	{
		return item_background.Color;
	}

	void SetBackgroundColor( Color color )
	{
		item_background.Color = color;
	}

	void EmptyMethod( ClockSlot clockSlot )
	{
		// Left emtpy
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
	public void SetItemIndex( int index )
	{
		UnityEditor.EditorUtility.SetDirty( gameObject );
		item_index = index;
	}

	public void SetItemCoordinate( Vector2Int coordinate )
	{
		UnityEditor.EditorUtility.SetDirty( gameObject );
		item_coordinate = coordinate;
	}

	public void SetItemData( ItemData itemData )
	{
		UnityEditor.EditorUtility.SetDirty( gameObject );
		item_data = itemData;
	}
#endif
#endregion
}

public delegate void ClockMessage( ClockSlot clockSlot );

public enum ItemState
{
	Invisible = -2,
	Locked = -1,
	Unlocked = 0
}