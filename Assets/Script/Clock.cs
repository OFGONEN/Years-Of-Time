/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using DG.Tweening;

public class Clock : MonoBehaviour
{
#region Fields
  [ Title( "Shared Data" ) ]
    [ SerializeField ] ClockData clock_data;
    [ SerializeField ] InputFingerPosition shared_input_finger_position;
    [ SerializeField ] SharedReferenceNotifier notif_camera_reference;

  [ Title( "Components" ) ]
	[ SerializeField ] Transform transform_gfx;
	[ SerializeField ] Collider collider_selection;

  [ Title( "Visual Components" ) ]
    [ SerializeField ] MeshFilter clock_circle_meshFilter;
    [ SerializeField ] Renderer clock_circle_renderer;
    [ SerializeField ] MeshFilter clock_hand_second_meshFilter;
    [ SerializeField ] Renderer clock_hand_second_renderer;

	Transform camera_transform;
	Camera camera_main;
	int selection_layer_mask;
	float animation_wave_cofactor;
	Vector3 position;

	RecycledTween recycledTween = new RecycledTween();

	UnityMessage onSelected;
	UnityMessage onDeSelected;
	UnityMessage onUpdate;
#endregion

#region Properties
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
	public void SpawnIntoSpawnSlot()
	{
		gameObject.SetActive( true );
		position = transform.position;
		DOPunchScale( DoWaveAnimation );
		//todo wave animation
	}

    public void SetIdlePosition( Vector3 position )
    {
		transform.position = position.SetY( GameSettings.Instance.clock_height_idle );
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
		FFLogger.Log( "Clock Selected", this );
		Selected();
		// onSelected(); //todo un-comment this line
	}

	public void OnDeSelected()
	{
		FFLogger.Log( "Clock DeSelected", this );
		SetIdlePosition( Vector3.zero );
		onUpdate = ExtensionMethods.EmptyMethod;
		collider_selection.enabled = true;
		// onDeSelected(); //todo un-comment this line 
	}
#endregion

#region Implementation
	void Selected()
	{
		CacheCamera(); //todo remove this
		//todo start scale tween
		selection_layer_mask = 1 << GameSettings.Instance.game_selection_layer; //todo remove this

		collider_selection.enabled = false;
		onUpdate                   = Movement;
	}

	void Movement()
	{
		var position       = transform.position;
		var movementTarget = FindMovementPosition().SetY( GameSettings.Instance.clock_movement_height );
		var movementDelta  = movementTarget - position;

		//todo Use movementDelta and calculate rotation

		position.x = position.x.Lerp( movementTarget.x, Time.deltaTime * GameSettings.Instance.clock_movement_speed_horizontal );
		position.y = position.y.Lerp( movementTarget.y, Time.deltaTime * GameSettings.Instance.clock_movement_speed_vertical );
		position.z = position.z.Lerp( movementTarget.z, Time.deltaTime * GameSettings.Instance.clock_movement_speed_horizontal );

		transform.position = position;
	}

	void DoWaveAnimation()
	{
		var targetPosition = position + Random.insideUnitCircle.ConvertV3_Z() * GameSettings.Instance.clock_animation_wave_radius + Vector3.right * GameSettings.Instance.clock_animation_wave_radius * animation_wave_cofactor;

		recycledTween.Recycle( transform.DOMove(
			targetPosition,
			GameSettings.Instance.clock_animation_wave_speed )
			.SetSpeedBased(), DoWaveAnimation );

		animation_wave_cofactor *= -1f;
	}

	void CacheCamera()
	{
		camera_transform = notif_camera_reference.sharedValue as Transform;
		camera_main      = camera_transform.GetComponent< Camera >();
	}

	Vector3 FindMovementPosition()
	{
		var fingerPosition = shared_input_finger_position.sharedValue;

		var worldPosition_Start = camera_main.ScreenToWorldPoint_NearClipPlane( fingerPosition );
		var worldPosition_End   = camera_main.ScreenToWorldPoint_FarClipPlane( fingerPosition );

		RaycastHit hitInfo;

		var hit = Physics.Raycast( 
			worldPosition_Start, 
			( worldPosition_End - worldPosition_Start ).normalized, 
			out hitInfo, 
			GameSettings.Instance.game_selection_distance, selection_layer_mask );

#if UNITY_EDITOR
		Debug.DrawRay( worldPosition_Start, hitInfo.point - worldPosition_Start, Color.red, 1 );
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
#endif
#endregion
}