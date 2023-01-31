/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FFStudio;
using DG.Tweening;

public class UIItemPopUpText : MonoBehaviour
{
#region Fields
    [ SerializeField ] TextMeshProUGUI _textRenderer;
    [ SerializeField ] PoolUIPopUpText pool_popUp;

    RecycledSequence recycledSequence = new RecycledSequence();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( string text, Vector3 position )
    {
		transform.position = position;

		var sequence = recycledSequence.Recycle( OnSequenceComplete );

		_textRenderer.color = GameSettings.Instance.item_popUp_color_start;

		sequence.Append( _textRenderer.DOColor( GameSettings.Instance.item_popUp_color_end,
			GameSettings.Instance.ui_PopUp_duration )
			.SetEase( GameSettings.Instance.item_popUp_color_ease ) );

		sequence.Join( transform.DOMoveZ( GameSettings.Instance.item_popUp_movement_delta,
			GameSettings.Instance.ui_PopUp_duration )
			.SetEase( GameSettings.Instance.item_popUp_movement_ease )
			.SetRelative() );
	}
#endregion

#region Implementation
    void OnSequenceComplete()
    {
		recycledSequence.Kill();
		pool_popUp.ReturnEntity( this );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
