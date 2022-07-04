/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

namespace FFStudio
{
	public class UI_Fade_Text : MonoBehaviour
	{
#region Fields
		public UnityEvent ui_OnComplete;

		TextMeshProUGUI ui_Text;
		RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
		public Tween Tween => recycledTween.Tween;
		public TextMeshProUGUI UI_Text => ui_Text;
#endregion

#region Unity API
		void Awake()
		{
			ui_Text = GetComponentInChildren< TextMeshProUGUI >();
		}
#endregion

#region API
		public Tween DoFade( float endValue, float duration )
		{
			recycledTween.Recycle(
				ui_Text.DOFade( endValue, duration ),
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