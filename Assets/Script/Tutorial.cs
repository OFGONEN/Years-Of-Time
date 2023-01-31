/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using TMPro;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] RectTransform transform_hand;
    [ SerializeField ] RectTransform transform_mask;
    [ SerializeField ] GameEvent event_clock_selection_enable;
    [ SerializeField ] GameEvent event_clock_selection_disable;
	[ SerializeField ] SharedReferenceNotifier notif_camera_reference;
    [ SerializeField ] ListSpawnSlot list_spawn_slot;
    [ SerializeField ] ListClockSlot list_clock_slot_column;
    [ SerializeField ] ListClockSlot list_clock_slot_row;


  [ Title( "Settings" ) ]
    [ SerializeField ] float hand_click_scale;
    [ SerializeField ] float hand_click_cooldown;
    
    [ FoldoutGroup( "Phase 1" ), SerializeField ] RectTransform phase_one_target_hand;
    [ FoldoutGroup( "Phase 1" ), SerializeField ] RectTransform phase_one_information;

    [ FoldoutGroup( "Phase 2" ), SerializeField ] float phase_two_cooldown_start;
    [ FoldoutGroup( "Phase 2" ), SerializeField ] RectTransform phase_two_information;
    [ FoldoutGroup( "Phase 2" ), SerializeField ] float phase_two_hand_movement_duration;
    [ FoldoutGroup( "Phase 2" ), SerializeField ] Ease phase_two_hand_movement_ease;
    [ FoldoutGroup( "Phase 2" ), SerializeField ] float phase_two_hand_movement_cooldown;

    [ FoldoutGroup( "Phase 3" ), SerializeField ] RectTransform phase_three_information;
    [ FoldoutGroup( "Phase 3" ), SerializeField ] float phase_three_cooldown_start;

    [ FoldoutGroup( "Phase 4" ), SerializeField ] RectTransform phase_four_information;

	Clock clock_current;
	ClockSlot clock_slot_current;

	Camera _camera;
	RecycledSequence recycledSequence = new RecycledSequence();
    UnityMessage onClockSpawn;
    UnityMessage onUpdate;
    Cooldown cooldown = new Cooldown();
#endregion

#region Properties
#endregion

#region Unity API
	private void Awake()
	{
		onUpdate     = ExtensionMethods.EmptyMethod;
		onClockSpawn = ExtensionMethods.EmptyMethod;
	}

	private void Update()
	{
		onUpdate();
	}
#endregion

#region API
    public void StartPhaseOne()
    {
		_camera = ( notif_camera_reference.sharedValue as Transform ).GetComponent< Camera >();

		transform_hand.position = phase_one_target_hand.position;
		transform_hand.rotation = phase_one_target_hand.rotation;

		transform_hand.gameObject.SetActive( true );
		transform_mask.gameObject.SetActive( true );
		phase_one_information.gameObject.SetActive( true );

		event_clock_selection_disable.Raise();
		onClockSpawn = StopPhaseOne;

		DoHandClickAnimation();
	}

    public void StopPhaseOne()
    {
		onClockSpawn = ExtensionMethods.EmptyMethod;
		recycledSequence.Kill();

		transform_hand.gameObject.SetActive( false );
		transform_mask.gameObject.SetActive( false );
		phase_one_information.gameObject.SetActive( false );

		cooldown.Start( phase_two_cooldown_start, StartPhaseTwo );
	}

    public void StartPhaseTwo()
    {
        transform_hand.gameObject.SetActive( true );
		phase_two_information.gameObject.SetActive( true );

		foreach( var spawnSlot in list_spawn_slot.itemDictionary.Values )
		{
			if( spawnSlot.IsClockPresent() )
			{
				clock_current = spawnSlot.GetCurrentClock();
				break;
			}
		}

		list_clock_slot_column.itemDictionary.TryGetValue( 0, out clock_slot_current );

		transform_hand.localScale = Vector3.one;
		transform_hand.rotation   = Quaternion.identity;

		DoHandMoveAnimation();
		onUpdate = PhaseTwoCheckIfComplete;
	}

	public void StartPhaseThree()
	{
		event_clock_selection_disable.Raise();

		transform_hand.position = phase_one_target_hand.position;
		transform_hand.rotation = phase_one_target_hand.rotation;

		transform_hand.gameObject.SetActive( true );
		transform_mask.gameObject.SetActive( true );
		phase_three_information.gameObject.SetActive( true );

		event_clock_selection_disable.Raise();
		onClockSpawn = StopPhaseThree;

		DoHandClickAnimation();
	}

	public void StopPhaseThree()
	{
		onClockSpawn = ExtensionMethods.EmptyMethod;

		recycledSequence.Kill();

		transform_hand.gameObject.SetActive( false );
		transform_mask.gameObject.SetActive( false );
		phase_three_information.gameObject.SetActive( false );

		cooldown.Start( phase_three_cooldown_start, StartPhaseFour );
	}

	public void StartPhaseFour()
	{
		transform_hand.gameObject.SetActive( true );
		phase_four_information.gameObject.SetActive( true );

		foreach( var spawnSlot in list_spawn_slot.itemDictionary.Values )
		{
			if( spawnSlot.IsClockPresent() )
			{
				clock_current = spawnSlot.GetCurrentClock();
				break;
			}
		}

		list_clock_slot_row.itemDictionary.TryGetValue( 0, out clock_slot_current );

		transform_hand.localScale = Vector3.one;
		transform_hand.rotation = Quaternion.identity;

		DoHandMoveAnimation();
		onUpdate = PhaseFourCheckIfComplete;
	}

    public void OnClockSpawn()
    {
		onClockSpawn();
	}
#endregion

#region Implementation
    void DoHandClickAnimation()
    {
		var sequence = recycledSequence.Recycle( DoHandClickAnimation );

		sequence.AppendCallback( SetHandClickScale );
		sequence.AppendInterval( hand_click_cooldown );
		sequence.AppendCallback( SetHandDefaultScale );
		sequence.AppendInterval( hand_click_cooldown );
	}

	void DoHandMoveAnimation()
	{
		transform_hand.position = _camera.WorldToScreenPoint( clock_current.transform.position );
		var targetPosition = _camera.WorldToScreenPoint( clock_slot_current.transform.position );

		var sequence = recycledSequence.Recycle( DoHandMoveAnimation );

		sequence.Append( transform_hand.DOMove( targetPosition, phase_two_hand_movement_duration )
			.SetEase( phase_two_hand_movement_ease ) );
		sequence.AppendInterval( phase_two_hand_movement_cooldown );
	}

	void PhaseTwoCheckIfComplete()
	{
		if( clock_slot_current.IsClockPresent() )
		{
			onUpdate = ExtensionMethods.EmptyMethod;
			phase_two_information.gameObject.SetActive( false );

			recycledSequence.Kill();
			transform_hand.gameObject.SetActive( false );

			cooldown.Start( GameSettings.Instance.clock_spawn_shake_scale.duration, StartPhaseThree );
		}
	}

	void PhaseFourCheckIfComplete()
	{
		if( clock_slot_current.IsClockPresent() && clock_slot_current.CurrentClockLevel() == 2 )
		{
			onUpdate = ExtensionMethods.EmptyMethod;
			phase_four_information.gameObject.SetActive( false );

			recycledSequence.Kill();
			transform_hand.gameObject.SetActive( false );

			cooldown.Start( GameSettings.Instance.clock_spawn_shake_scale.duration, event_clock_selection_disable.Raise ); //todo start phase five
		}
	}

    void SetHandDefaultScale()
    {
		transform_hand.localScale = Vector3.one;
	}

    void SetHandClickScale()
    {
		transform_hand.localScale = Vector3.one * hand_click_scale;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
