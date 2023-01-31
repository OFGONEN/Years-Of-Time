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

  [ Title( "Settings" ) ]
    [ SerializeField ] float hand_click_scale;
    [ SerializeField ] float hand_click_cooldown;
    
    [ FoldoutGroup( "Phase 1" ), SerializeField ] RectTransform phase_one_target_hand;
    [ FoldoutGroup( "Phase 1" ), SerializeField ] RectTransform phase_one_information;
    [ FoldoutGroup( "Phase 2" ), SerializeField ] float phase_two_cooldown_start;
    [ FoldoutGroup( "Phase 2" ), SerializeField ] RectTransform phase_two_information;

    RecycledSequence recycledSequence = new RecycledSequence();
    UnityMessage onClockSpawn;
    Cooldown cooldown = new Cooldown();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public void StartPhaseOne()
    {
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
		transform_hand.gameObject.SetActive( false );
		transform_mask.gameObject.SetActive( false );
		phase_one_information.gameObject.SetActive( false );

		cooldown.Start( phase_two_cooldown_start, StartPhaseTwo );
	}

    public void StartPhaseTwo()
    {
		event_clock_selection_disable.Raise();

        transform_hand.gameObject.SetActive( true );
		phase_two_information.gameObject.SetActive( true );
	}

    public void OnClockSpawn()
    {
        FFLogger.Log( "OnClockSpawn" );
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
