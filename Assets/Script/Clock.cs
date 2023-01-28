/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEditor;

public class Clock : MonoBehaviour
{
#region Fields
  [ Title( "Shared Data" ) ]
    [ SerializeField ] ClockData clock_data;
    [ SerializeField ] InputFingerPosition shared_input_finger_position;
    [ SerializeField ] SharedReferenceNotifier notif_camera_reference;
    [ SerializeField ] ListSlot list_slot;
	[ SerializeField ] PoolClock pool_clock;

  [ Title( "Components" ) ]
	[ SerializeField ] Transform transform_gfx;
	[ SerializeField ] Collider collider_selection;
	[ SerializeField ] Transform clock_hand_second;
	[ SerializeField ] Transform clock_hand_minute;
	[ SerializeField ] Transform clock_hand_hour;

  [ Title( "Visual Components" ) ]
    [ SerializeField ] MeshFilter clock_circle_meshFilter;
    [ SerializeField ] Renderer clock_circle_renderer;
    [ SerializeField ] MeshFilter clock_hand_second_meshFilter;
    [ SerializeField ] Renderer clock_hand_second_renderer;

	Transform camera_transform;
	Camera camera_main;
	ISlotEntity slot_current;
	ISlotEntity slot_target;
	float animation_wave_cofactor;

	RecycledTween recycledTween = new RecycledTween();

	UnityMessage onSelected;
	UnityMessage onDeSelected;
	UnityMessage onDeSelectedCache;
	UnityMessage onUpdate;
#endregion

#region Properties
	public  ClockData ClockData => clock_data;
	Vector3 SlotPositionCurrent => slot_current.GetPosition().SetY( GameSettings.Instance.clock_height_idle );
	Vector3 SlotPositionTarget  => slot_target.GetPosition().SetY( GameSettings.Instance.clock_height_idle );
#endregion

#region Unity API
	void Awake()
	{
		EmptyDelegates();
	}

	void Update()
	{
		onUpdate();
	}
#endregion

#region API
	public void SpawnIntoSpawnSlot( ISlotEntity slotEntity, ClockData data  )
	{
		CacheCamera();
		UpdateClockData( data );
		UpdateVisuals();

		slot_current = slotEntity;
		onSelected   = SelectedOnSpawnSlot;

		collider_selection.enabled = true;

		gameObject.SetActive( true );
		transform.position = SlotPositionCurrent;

		DoWaveAnimation();
	}

	public void LoadIntoClockSlot( ISlotEntity slotEntity, ClockData data )
	{
		CacheCamera();
		UpdateClockData( data );
		UpdateVisuals();

		slot_current = slotEntity;
		onSelected   = SelectedOnClockSlot;

		collider_selection.enabled = true;

		gameObject.SetActive( true );
		transform.position = SlotPositionCurrent;

		onUpdate = DoProductionAnimation;
	}

	public void UpgradeInSpawnSlot()
	{
		UpdateClockData( clock_data.ClockNextData );
		UpdateVisuals();

		transform.position = SlotPositionCurrent;
		DOPunchScale( DoWaveAnimation );
		//todo Play PFX
	}

	public void UpgradeInClockSlot()
	{
		UpdateClockData( clock_data.ClockNextData );
		UpdateVisuals();

		transform.position = SlotPositionCurrent;
		DOPunchScale( null );
		//todo Play PFX
	}

	public void UpdateClockData( ClockData data )
	{
		clock_data = data;
	}

    public void UpdateVisuals()
    {
		var circleSharedMaterials  = clock_circle_renderer.sharedMaterials;
		circleSharedMaterials[ 1 ] = clock_data.ClockMaterial;

		clock_circle_meshFilter.mesh              = clock_data.ClockCircleMesh;
		clock_hand_second_meshFilter.mesh         = clock_data.ClockSecondHandMesh;
		clock_circle_renderer.sharedMaterials     = circleSharedMaterials;
		clock_hand_second_renderer.sharedMaterial = clock_data.ClockMaterial;
	}

	public void DOPunchScale( UnityMessage onComplete )
	{
		recycledTween.Recycle( GameSettings.Instance.clock_spawn_punchScale.CreateTween( transform_gfx ), onComplete ) ;
	}

	public void OnSelected()
	{
		onSelected(); 
	}

	public void OnDeSelected()
	{
		onDeSelected();
	}

	public void OccupySpawnSlot()
	{
		collider_selection.enabled = true;
		onSelected   = SelectedOnSpawnSlot;

		DoWaveAnimation();
	}

	public void OccupyClockSlot()
	{
		collider_selection.enabled = true;
		onSelected                 = SelectedOnClockSlot;

		onUpdate = DoProductionAnimation;
	}

	public void ReturnToPool()
	{
		EmptyDelegates();
		collider_selection.enabled = false;

		pool_clock.ReturnEntity( this );
	}
#endregion

#region Implementation
	void SelectedOnSpawnSlot()
	{
		//todo start scale tween
		onSelected        = ExtensionMethods.EmptyMethod;
		onDeSelected      = DeSelectedOnSpawnSlotReturnToCurrentSlot;
		onDeSelectedCache = DeSelectedOnSpawnSlotReturnToCurrentSlot;

		recycledTween.Kill();

		collider_selection.enabled = false;
		onUpdate                   = OnMovement;
	}

	void SelectedOnClockSlot()
	{
		onSelected        = ExtensionMethods.EmptyMethod;
		onDeSelected      = DeSelectedOnClockSlotReturnToCurrentSlot;
		onDeSelectedCache = DeSelectedOnClockSlotReturnToCurrentSlot;

		recycledTween.Kill();

		collider_selection.enabled = false;
		onUpdate                   = OnMovement;
	}

	void DeSelectedGoToTargetSlot()
	{
		if( slot_target.IsClockPresent() && slot_target.CurrentClockLevel() != ClockData.ClockLevel )
			DeSelectedOnSpawnSlotReturnToCurrentSlot();
		else
		{
			slot_current.OnCurrentClockDeparted();
			
			recycledTween.Recycle( transform.DOMove(
				SlotPositionTarget,
				GameSettings.Instance.clock_slot_go_duration )
				.SetEase( GameSettings.Instance.clock_slot_go_ease ),
				OnGoToTargetSlotComplete
			);

			EmptyDelegates();
		}
	}

	void DeSelectedOnClockSlotReturnToCurrentSlot()
	{
		slot_target = null;

		recycledTween.Recycle( transform.DOMove(
			SlotPositionCurrent,
			GameSettings.Instance.clock_slot_return_duration )
			.SetEase( GameSettings.Instance.clock_slot_return_ease ),
			OccupyClockSlot
		);

		EmptyDelegates();
	}

	void DeSelectedOnSpawnSlotReturnToCurrentSlot()
	{
		slot_target = null;

		recycledTween.Recycle( transform.DOMove(
			SlotPositionCurrent,
			GameSettings.Instance.clock_slot_return_duration )
			.SetEase( GameSettings.Instance.clock_slot_return_ease ),
			OccupySpawnSlot
		);

		EmptyDelegates();
	}

	void OnGoToTargetSlotComplete()
	{
		slot_current = slot_target;
		slot_target  = null;

		slot_current.HandleIncomingClock( this );
	}

	void OnMovement()
	{
		var position = Movement(); // move on this frame
		SearchTargetSlot();
	}

	void SearchTargetSlot()
	{
		ISlotEntity slotTarget      = slot_current;
		var         closestDistance = float.MaxValue;
		var         position        = transform.position;

		foreach( var slot in list_slot.itemList )
		{
			var slotPosition = slot.GetPosition();
			var distance = Vector3.Distance( slotPosition, position );
			if( distance < closestDistance && distance <= GameSettings.Instance.clock_slot_search_distance  )
				slotTarget = slot;
		}

		if( slotTarget == slot_current )
		{
			slot_target?.HighlightDefault();
			onDeSelected = onDeSelectedCache;
		}
		else
		{
			slot_target  = slotTarget;
			onDeSelected = DeSelectedGoToTargetSlot;

			if( !slot_target.IsClockPresent() || slot_target.CurrentClockLevel() == ClockData.ClockLevel )
				slot_target.HighlightPositive();
			else
				slot_target.HighlightNegative();
		}
	}

	Vector3 Movement()
	{
		var position       = transform.position;
		var movementTarget = FindMovementPosition().SetY( GameSettings.Instance.clock_movement_height );
		var movementDelta  = movementTarget - position;

		//todo Use movementDelta and calculate rotation

		position.x = position.x.Lerp( movementTarget.x, Time.deltaTime * GameSettings.Instance.clock_movement_speed_horizontal );
		position.y = position.y.Lerp( movementTarget.y, Time.deltaTime * GameSettings.Instance.clock_movement_speed_vertical );
		position.z = position.z.Lerp( movementTarget.z, Time.deltaTime * GameSettings.Instance.clock_movement_speed_horizontal );

		transform.position = position;
		return position;
	}

	void DoWaveAnimation()
	{
		var targetPosition = SlotPositionCurrent + Random.insideUnitCircle.ConvertV3_Z() * GameSettings.Instance.clock_animation_wave_radius + Vector3.right * GameSettings.Instance.clock_animation_wave_radius * animation_wave_cofactor;

		recycledTween.Recycle( transform.DOMove(
			targetPosition,
			GameSettings.Instance.clock_animation_wave_speed )
			.SetSpeedBased(), DoWaveAnimation );

		animation_wave_cofactor *= -1f;
	}

	void DoProductionAnimation()
	{
		clock_hand_second.Rotate( Vector3.forward * -1f * clock_data.ClockHandSecondSpeed * Time.deltaTime, Space.Self );
		clock_hand_minute.Rotate( Vector3.forward * -1f * clock_data.ClockHandMinuteSpeed * Time.deltaTime, Space.Self );
		clock_hand_hour.Rotate( Vector3.forward * -1f * clock_data.ClockHandHourSpeed * Time.deltaTime, Space.Self );
	}

	void CacheCamera()
	{
		camera_transform = notif_camera_reference.sharedValue as Transform;
		camera_main      = camera_transform.GetComponent< Camera >();
	}

	Vector3 FindMovementPosition()
	{
		var fingerPosition      = shared_input_finger_position.sharedValue;
		var worldPosition_Start = camera_main.ScreenToWorldPoint_NearClipPlane( fingerPosition );
		var worldPosition_End   = camera_main.ScreenToWorldPoint_FarClipPlane( fingerPosition );

		RaycastHit hitInfo;

		var hit = Physics.Raycast( 
			worldPosition_Start, 
			( worldPosition_End - worldPosition_Start ).normalized, 
			out hitInfo, 
			GameSettings.Instance.game_selection_distance, GameSettings.Instance.game_selection_layer_mask );

#if UNITY_EDITOR
		// Debug.DrawRay( worldPosition_Start, hitInfo.point - worldPosition_Start, Color.red, 1 );
#endif

		return hitInfo.point;
	}

	void EmptyDelegates()
	{
		onSelected   = ExtensionMethods.EmptyMethod;
		onDeSelected = ExtensionMethods.EmptyMethod;
		onUpdate     = ExtensionMethods.EmptyMethod;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
	private void OnDrawGizmos() 
	{
		if( slot_target == null ) return;

		Handles.DrawLine( transform.position, slot_target.GetPosition(), 1f );
	}
#endif
#endregion
}