/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace FFStudio
{
	[ System.Serializable ]
	public abstract class TweenData
	{		
#region Fields (Inspector Interface)
		[ BoxGroup( "Other", false ) ] public string description;
		
	[ Title( "Chain" ) ]
		[ BoxGroup( "Other", false ) ] public bool chain;
		[ BoxGroup( "Other", false ), ShowIf( "chain" ), LabelText( "Next To Play" ) ] public int index_nextUp;

	[ Title( "Start" ) ]
		[ BoxGroup( "Other", false ), LabelText( "Delay" ) ] public bool hasDelay;
		[ BoxGroup( "Other", false ), ShowIf( "hasDelay" ) ] public float delayAmount;
	
	[ Title( "Events" ), SerializeReference ]
		[ BoxGroup( "Other", false ) ] public TweenEventData[] tweenEventDatas;
		
#if UNITY_EDITOR
		[ HideIf( "HideBaseClassLoopCheckBox" ) ]
#endif
		[ BoxGroup( "Tween", false ), DisableIf( "IsPlaying" ) ] public bool loop;
		[ BoxGroup( "Tween", false ), ShowIf( "loop" ) ] public LoopType loopType = LoopType.Restart;
		[ BoxGroup( "Tween", false ) ] public Ease easing = Ease.Linear;
		[ BoxGroup( "Tween", false ) ] public UnityEvent onCompleteEvent;

		public Tween Tween => recycledTween.Tween;
		
		protected RecycledTween recycledTween = new RecycledTween();
		protected RecycledTween recycledTween_Delay = new RecycledTween();
		protected Transform transform;

		UnityMessage onComplete;
#endregion

#region Properties
		public bool IsDelayPaused => recycledTween_Delay.Tween != null && recycledTween_Delay.Tween.IsPlaying() == false;
		public bool IsPaused => recycledTween.Tween != null && recycledTween.Tween.IsPlaying() == false;
		public bool IsDelayPlaying => recycledTween_Delay.Tween != null && recycledTween_Delay.Tween.IsPlaying();
		public bool IsPlaying => recycledTween.Tween != null && recycledTween.Tween.IsPlaying();
#endregion

#region API
		public virtual void Initialize( Transform transform )
		{
			this.transform = transform;
		}

		public void Play( UnityMessage onComplete = null )
		{
			if( IsDelayPaused )
				recycledTween_Delay.Tween.Play();
			else if( IsPaused )
				recycledTween.Tween.Play();
			else
			{
				if( hasDelay )
					recycledTween_Delay.Recycle( DOVirtual.DelayedCall( delayAmount, () => CreateAndStartTween( onComplete ) ) );
				else
					CreateAndStartTween( onComplete );
			}
		}

		public void Pause()
		{
			if( IsDelayPlaying )
				recycledTween_Delay.Tween.Pause();
			else if( IsPlaying )
				recycledTween.Tween.Pause();
		}
		
		public void Stop()
		{
			recycledTween_Delay.Kill();

			if( IsPlaying )
				Tween.Rewind();
		}
		
		public void Kill()
		{
			recycledTween.Kill();
			recycledTween_Delay.Kill();
		}
#endregion

#region Implementation
		protected virtual void CreateAndStartTween( UnityMessage onComplete, bool isReversed = false )
		{
			this.onComplete = onComplete;
			recycledTween.OnComplete( OnComplete );

			if( tweenEventDatas != null && tweenEventDatas.Length > 0 )
			{
				for( int i = 0; i < tweenEventDatas.Length; i++ )
					tweenEventDatas[ i ].isConsumed = false;
				Tween.OnUpdate( OnUpdate );
			}
		}
		
		void OnUpdate()
		{
			for( int i = 0; i < tweenEventDatas.Length; i++ )
			{
				TweenEventData tweenEventData = tweenEventDatas[ i ];
				
				if( tweenEventData.isConsumed == false )
					tweenEventData.InvokeEventIfThresholdIsPassed( Tween, easing );
			}
		}

		void OnComplete()
		{
			onCompleteEvent.Invoke();
			onComplete?.Invoke();
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
		public virtual bool HideBaseClassLoopCheckBox() => false;
#endif
#endregion
	}
}