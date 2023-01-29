/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Shapes;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class Item : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] Vector2Int item_coordinate;
    [ SerializeField ] int item_index;
    [ SerializeField ] ItemData item_data;

  [ Title( "Shared" ) ]
    [ SerializeField ] ListItem list_item_coordinate;
    [ SerializeField ] ListItem list_item_index;
    [ SerializeField ] Currency notif_currency;
    [ SerializeField ] IncomeCofactor notif_income_cofactor;
    [ SerializeField ] SaveSystem system_save;

  [ Title( "Components" ) ]
    [ SerializeField ] Rectangle item_background;
    [ SerializeField ] Image item_image_background;
    [ SerializeField ] Image item_image_foreground;
    [ SerializeField ] Transform item_image_parent;

	ItemState item_state;
	float item_duration;
	Color item_background_color;

	RecycledTween recycledTween_Color = new RecycledTween();
	RecycledTween recycledTween_Scale = new RecycledTween();
	List< ClockSlot > clock_slot_list = new List< ClockSlot >( 2 );
    UnityMessage onUpdate;
	ClockMessage onClockAssign;
	ClockMessage onClockRemove;
#endregion

#region Properties
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
		item_background_color = item_background.Color;

		// Load item state
		var state = (ItemState)system_save.SaveData.item_array[ item_index ];
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

		transform.localScale = Vector3.one;
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

	void EmptyMethod( Clock clock )
	{
		// Left emtpy
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}

public delegate void ClockMessage( Clock clock );

public enum ItemState
{
	Invisible = -2,
	Locked = -1,
	Unlocked = 0
}