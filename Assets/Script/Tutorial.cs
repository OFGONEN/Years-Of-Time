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
    [ SerializeField ] RectTransform transform_information;
    [ SerializeField ] TextMeshProUGUI textRenderer_information;

  [ Title( "Settings" ) ]
    [ SerializeField ] float hand_click_scale;
    [ SerializeField ] float hand_click_cooldown;
    
    [ FoldoutGroup( "Phase 1" ), SerializeField ] RectTransform target_hand;

    RecycledSequence recycledSequence = new RecycledSequence();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public void StartPhaseOne()
    {
		transform_hand.position = target_hand.position;
		transform_hand.rotation = target_hand.rotation;

		transform_hand.gameObject.SetActive( true );
		transform_mask.gameObject.SetActive( true );
		transform_information.gameObject.SetActive( true );

		DoHandClickAnimation();
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
