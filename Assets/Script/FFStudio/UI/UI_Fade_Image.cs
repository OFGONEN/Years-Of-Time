/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

namespace FFStudio
{
	public class UI_Fade_Image : MonoBehaviour
	{
#region Fields
        public UnityEvent ui_OnComplete;

        Image ui_Image;
        RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
        public Tween Tween => recycledTween.Tween;
#endregion

#region Unity API
        void Awake()
        {
            ui_Image = GetComponentInChildren< Image >();
        }
#endregion

#region API
        public Tween DoFade( float endValue, float duration )
        {
			recycledTween.Recycle( 
                ui_Image.DOFade( endValue, duration ),
                OnTweenComplete );

			return recycledTween.Tween;
		}
#endregion

#region Implementation
        void OnTweenComplete()
        {
			ui_OnComplete.Invoke();
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}