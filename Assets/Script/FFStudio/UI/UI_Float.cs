/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace FFStudio
{
	public class UI_Float : MonoBehaviour
	{
#region Fields
        public UnityEvent ui_OnComplete;

        RectTransform ui_rectTransform;
        RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
		public Tween Tween => recycledTween.Tween;
        public RectTransform UI_RectTransform => ui_rectTransform;
#endregion

#region Unity API
        void Awake()
        {
            ui_rectTransform = GetComponent< RectTransform >();
        }
#endregion

#region API
        public Tween DoFloat( float relativeValue, float duration ) 
        {
			recycledTween.Recycle( 
				ui_rectTransform.DOMove( ui_rectTransform.position + Vector3.up * relativeValue, duration ),
			 	OnTweenComplete );

			return recycledTween.Tween;
		}

		public void KillTween()
		{
			recycledTween.Kill();
		}

		public void CompleteTween()
		{
			recycledTween.Tween.Complete();
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