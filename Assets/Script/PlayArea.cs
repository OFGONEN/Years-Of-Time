/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class PlayArea : MonoBehaviour
{
#region Fields
    [ SerializeField ] SharedIntNotifier notif_playArea_size;

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
		notif_playArea_size.sharedValue = Mathf.Min( notif_playArea_size.sharedValue + 1, GameSettings.Instance.playArea_size_array.Length - 1 );

		TweenSize();

		notif_playArea_size.SaveToPlayerPrefs();
	}
#endregion

#region Implementation
    [ Button() ]
    void ChangeSize()
    {
		var size = GameSettings.Instance.playArea_size_array[ notif_playArea_size.sharedValue ];
		transform.localScale = Vector3.one.SetX( size ).SetZ( size );
    }

    void TweenSize()
    {
		var size = GameSettings.Instance.playArea_size_array[ notif_playArea_size.sharedValue ];

		recycledTween.Recycle( transform.DOScale( Vector3.one.SetX( size ).SetZ( size ), GameSettings.Instance.playArea_size_duration )
			.SetEase( GameSettings.Instance.playArea_size_ease ), notif_playArea_size.Notify );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
