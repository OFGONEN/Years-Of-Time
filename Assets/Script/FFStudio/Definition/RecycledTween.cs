/* Created by and for usage of FF Studios (2021). */

using DG.Tweening;

namespace FFStudio
{
	public class RecycledTween
	{
		UnityMessage onComplete;
		Tween tween;

		public Tween Tween => tween;

		public void Recycle( Tween tween_unsafe, UnityMessage onComplete )
		{
			tween = tween.KillProper();
			tween = tween_unsafe;

			this.onComplete = onComplete;

			tween.OnComplete( OnComplete_Safe );

#if UNITY_EDITOR
			tween.SetId( "_ff_RecycledTween" );
#endif
		}

		public void Recycle( Tween tween_unsafe )
		{
			tween = tween.KillProper();
			tween = tween_unsafe;

			tween.OnComplete( OnComplete_Safe );

#if UNITY_EDITOR
			tween.SetId( "_ff_RecycledTween" );
#endif
		}
		
		public void OnComplete( UnityMessage onComplete )
		{
			this.onComplete = onComplete;
		}

		public void Kill()
		{
			tween = tween.KillProper();
		}

		void OnComplete_Safe()
		{
			tween = null;
			onComplete?.Invoke();
		}
	}
}