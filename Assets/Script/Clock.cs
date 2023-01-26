/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

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
    public void SetIdlePosition( Vector3 position )
    {
		transform.position = position.SetY( GameSettings.Instance.clock_height_idle );
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

	public void DOPunchScale()
	{
		recycledTween.Recycle( GameSettings.Instance.clock_spawn_punchScale.CreateTween( transform_gfx ) ) ;
	}

	public void OnSelected()
	{
		onSelected();
	}

	public void OnDeSelected()
	{
		onDeSelected();
	}
#endregion

#region Implementation
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