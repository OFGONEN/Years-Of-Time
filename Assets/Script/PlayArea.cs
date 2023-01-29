/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;

public class PlayArea : MonoBehaviour
{
#region Fields
    [ SerializeField ] SharedIntNotifier notif_item_unlock_index;

    RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
    private void Start()
    {
		ChangeSize();
	}
#endregion

#region API
    public void OnItemUnlocked()
    {
		notif_item_unlock_index.SharedValue = Mathf.Min( notif_item_unlock_index.sharedValue + 1, GameSettings.Instance.playArea_size_array.Length - 1 );

		TweenSize();
	}
#endregion

#region Implementation
    void ChangeSize()
    {
		var size = GameSettings.Instance.playArea_size_array[ notif_item_unlock_index.sharedValue ];
		transform.localScale = Vector3.one.SetX( size ).SetZ( size );
    }

    void TweenSize()
    {
		var size = GameSettings.Instance.playArea_size_array[ notif_item_unlock_index.sharedValue ];

		recycledTween.Recycle( transform.DOScale( size, GameSettings.Instance.playArea_size_duration )
			.SetEase( GameSettings.Instance.playArea_size_ease ) );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
