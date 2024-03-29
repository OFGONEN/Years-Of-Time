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
	[ SerializeField ] ParticleSpawnEvent event_particle_spawn;
	[ SerializeField ] IntGameEvent event_vibrate;
	[ SerializeField ] TweenableFloatNotifier notif_item_speed;

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

	RecycledTween    recycledTween    = new RecycledTween();
	RecycledSequence recycledSequence = new RecycledSequence();

	UnityMessage onSelected;
	UnityMessage onDeSelected;
	UnityMessage onDeSelectedCache;
	UnityMessage onUpdate;
#endregion

#region Properties
	public  ClockData ClockData => clock_data;
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

		gameObject.SetActive( true );

		transform.SetParent( slot_current.GetTransform() );
		transform.localPosition = Vector3.up * GameSettings.Instance.clock_height_idle;
		transform.localScale    = Vector3.one;

		DOPunchScale( () => {
			collider_selection.enabled = true;
			DoWaveAnimation();
		} );
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

		transform.SetParent( slot_current.GetTransform() );
		transform.localPosition  = Vector3.up * GameSettings.Instance.clock_height_idle;
		transform.localScale     = Vector3.one;
		transform_gfx.localScale = Vector3.one;

		onUpdate = DoProductionAnimation;
	}

	public void UpgradeInSpawnSlot()
	{
		UpdateClockData( clock_data.ClockNextData );
		UpdateVisuals();

		transform.localPosition = Vector3.up * GameSettings.Instance.clock_height_idle;

		collider_selection.enabled = false;
		DOPunchScale( () => {
			collider_selection.enabled = true;
			DoWaveAnimation();
		} );

		event_vibrate.Raise( 0 );
		event_particle_spawn.Raise( "clock_upgrade", transform.position );
	}

	[ Button() ]
	public void UpgradeInClockSlot()
	{
		UpdateClockData( clock_data.ClockNextData );
		UpdateVisuals();

		transform.localPosition = Vector3.up * GameSettings.Instance.clock_height_idle;

		DOPunchScale( () => collider_selection.enabled = true );

		event_vibrate.Raise( 0 );
		event_particle_spawn.Raise( "clock_upgrade", transform.position );
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
		transform_gfx.localScale = Vector3.one;
		recycledTween.Recycle( GameSettings.Instance.clock_spawn_shake_scale.CreateTween( transform_gfx ), onComplete ) ;
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

		recycledTween.Kill();
		collider_selection.enabled = false;
		transform.localScale       = Vector3.one;
		transform_gfx.localScale   = Vector3.one;

		pool_clock.ReturnEntity( this );
	}
#endregion

#region Implementation
	void SelectedOnSpawnSlot()
	{
		onSelected        = ExtensionMethods.EmptyMethod;
		onDeSelected      = DeSelectedOnSpawnSlotReturnToCurrentSlot;
		onDeSelectedCache = DeSelectedOnSpawnSlotReturnToCurrentSlot;

		transform.SetParent( null );

		recycledTween.Kill();
		recycledTween.Recycle( transform.DOScale( Vector3.one * GameSettings.Instance.clock_movement_scale,
			GameSettings.Instance.clock_movement_scale_duration )
			.SetEase( GameSettings.Instance.clock_movement_scale_ease ) );

		slot_current.OnCurrentClockDeparted();

		collider_selection.enabled = false;
		onUpdate                   = OnMovement;
	}

	void SelectedOnClockSlot()
	{
		onSelected        = ExtensionMethods.EmptyMethod;
		onDeSelected      = DeSelectedOnClockSlotReturnToCurrentSlot;
		onDeSelectedCache = DeSelectedOnClockSlotReturnToCurrentSlot;

		transform.SetParent( null );

		recycledTween.Kill();
		recycledTween.Recycle( transform.DOScale( Vector3.one * GameSettings.Instance.clock_movement_scale,
			GameSettings.Instance.clock_movement_scale_duration )
			.SetEase( GameSettings.Instance.clock_movement_scale_ease ) );

		slot_current.OnCurrentClockDeparted();

		collider_selection.enabled = false;
		onUpdate                   = OnMovement;
	}

	void DeSelectedGoToTargetSlot()
	{
		if( slot_target.IsClockPresent() && slot_target.CurrentClockLevel() != ClockData.ClockLevel )
		{
			slot_target.HighlightDefault();
			DeSelectedOnSpawnSlotReturnToCurrentSlot();
		}
		else
		{
			transform.SetParent( slot_target.GetTransform() );

			var sequence = recycledSequence.Recycle( OnGoToTargetSlotComplete );

			sequence.Append( transform.DOLocalMove(
				Vector3.up * GameSettings.Instance.clock_height_idle,
				GameSettings.Instance.clock_slot_go_duration )
				.SetEase( GameSettings.Instance.clock_slot_go_ease ) );

			sequence.Join( transform.DOScale(
				Vector3.one,
				GameSettings.Instance.clock_movement_scale_duration )
				.SetEase( GameSettings.Instance.clock_movement_scale_ease ) );

			sequence.Join( transform.DORotate( Vector3.zero,
				GameSettings.Instance.clock_slot_go_duration )
				.SetEase( GameSettings.Instance.clock_slot_go_ease ) );

			EmptyDelegates();
		}
	}

	void DeSelectedOnClockSlotReturnToCurrentSlot()
	{
		transform.SetParent( slot_current.GetTransform() );
		slot_target = null;

		var sequence = recycledSequence.Recycle( OnReturnToCurrentSlotComplete );

		sequence.Append( transform.DOLocalMove(
			Vector3.up * GameSettings.Instance.clock_height_idle,
			GameSettings.Instance.clock_slot_go_duration )
			.SetEase( GameSettings.Instance.clock_slot_go_ease ) );

		sequence.Join( transform.DOScale(
			Vector3.one,
			GameSettings.Instance.clock_movement_scale_duration )
			.SetEase( GameSettings.Instance.clock_movement_scale_ease ) );
		
		sequence.Join( transform.DORotate( Vector3.zero,
			GameSettings.Instance.clock_slot_go_duration )
			.SetEase( GameSettings.Instance.clock_slot_go_ease ) );

		EmptyDelegates();
	}

	void DeSelectedOnSpawnSlotReturnToCurrentSlot()
	{
		transform.SetParent( slot_current.GetTransform() );
		slot_target = null;

		var sequence = recycledSequence.Recycle( OnReturnToCurrentSlotComplete );

		sequence.Append( transform.DOLocalMove(
			Vector3.up * GameSettings.Instance.clock_height_idle,
			GameSettings.Instance.clock_slot_go_duration )
			.SetEase( GameSettings.Instance.clock_slot_go_ease ) );

		sequence.Join( transform.DOScale(
			Vector3.one,
			GameSettings.Instance.clock_movement_scale_duration )
			.SetEase( GameSettings.Instance.clock_movement_scale_ease ) );
		
		sequence.Join( transform.DORotate( Vector3.zero,
			GameSettings.Instance.clock_slot_go_duration )
			.SetEase( GameSettings.Instance.clock_slot_go_ease ) );

		EmptyDelegates();
	}

	void OnGoToTargetSlotComplete()
	{
		slot_current = slot_target;
		slot_target  = null;

		slot_current.HandleIncomingClock( this );
	}

	void OnReturnToCurrentSlotComplete()
	{
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

		slot_target?.HighlightDefault();

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
		var pitch = ( movementDelta.z * GameSettings.Instance.clock_rotation_cofactor ).Clamp( GameSettings.Instance.clock_rotation_clamp );

		var roll = ( movementDelta.x * GameSettings.Instance.clock_rotation_cofactor ).Clamp( GameSettings.Instance.clock_rotation_clamp ) * -1f;

		position.x = position.x.Lerp( movementTarget.x, Time.deltaTime * GameSettings.Instance.clock_movement_speed_horizontal );
		position.y = position.y.Lerp( movementTarget.y, Time.deltaTime * GameSettings.Instance.clock_movement_speed_vertical );
		position.z = position.z.Lerp( movementTarget.z, Time.deltaTime * GameSettings.Instance.clock_movement_speed_horizontal );

		transform.position = position;
		transform.eulerAngles = Vector3.zero.SetX( pitch ).SetZ( roll );
		return position;
	}

	void DoWaveAnimation()
	{
		var targetPosition = Vector3.up * GameSettings.Instance.clock_height_idle + Random.insideUnitCircle.ConvertV3_Z() * GameSettings.Instance.clock_animation_wave_radius + Vector3.right * GameSettings.Instance.clock_animation_wave_radius * animation_wave_cofactor;

		recycledTween.Recycle( transform.DOLocalMove(
			targetPosition,
			GameSettings.Instance.clock_animation_wave_speed )
			.SetSpeedBased(), DoWaveAnimation );

		animation_wave_cofactor *= -1f;
	}

	void DoProductionAnimation()
	{
		clock_hand_second.Rotate( Vector3.forward * -1f * clock_data.ClockHandSecondSpeed * ( notif_item_speed.sharedValue * GameSettings.Instance.clock_produce_cofactor ) * Time.deltaTime, Space.Self );
		clock_hand_minute.Rotate( Vector3.forward * -1f * clock_data.ClockHandMinuteSpeed * ( notif_item_speed.sharedValue * GameSettings.Instance.clock_produce_cofactor ) * Time.deltaTime, Space.Self );
		clock_hand_hour.Rotate( Vector3.forward * -1f * clock_data.ClockHandHourSpeed * ( notif_item_speed.sharedValue * GameSettings.Instance.clock_produce_cofactor ) * Time.deltaTime, Space.Self );
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