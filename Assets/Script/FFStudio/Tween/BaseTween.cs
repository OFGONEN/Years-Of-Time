/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace FFStudio
{
    public abstract class BaseTween : MonoBehaviour
    {
        [ TableColumnWidth( 10 ), PropertyOrder( int.MinValue ) ]
        public string description;
        
#region Fields (Inspector Interface)
    [ Title( "Start Options" ) ]
        [ FoldoutGroup( "Base Tween Options", 100 ) ] public bool playOnStart;
		[ FoldoutGroup( "Base Tween Options", 100 ) ] public bool hasDelay;
        [ FoldoutGroup( "Base Tween Options", 100 ), ShowIf( "hasDelay" ) ] public float delayAmount;
        
    [ Title( "Tween" ) ]
        [ FoldoutGroup( "Base Tween Options", 100 ), DisableIf( "IsPlaying" ) ] public bool loop;
        [ FoldoutGroup( "Base Tween Options", 100 ), ShowIf( "loop" ) ] public LoopType loopType = LoopType.Restart;
        [ FoldoutGroup( "Base Tween Options", 100 ) ] public Ease easing = Ease.Linear;
        
    [ Title( "Event Flow" ) ]
        [ FoldoutGroup( "Base Tween Options", 100 ), SerializeField ] MultipleEventListenerDelegateResponse triggeringEvents;
        [ FoldoutGroup( "Base Tween Options", 100 ), SerializeField ] GameEvent[] events_fireOnComplete;
        [ FoldoutGroup( "Base Tween Options", 100 ), SerializeField ] UnityEvent unityEvents_FireOnComplete;
		[ FoldoutGroup( "Base Tween Options", 100 ) ] public bool hasDelay_beforeEvents;
		[ FoldoutGroup( "Base Tween Options", 100 ), ShowIf( "hasDelay_beforeEvents" ) ] public float delayAmount_beforeEvents;
#endregion

#region Fields (Private)
		protected RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
        [ field: SerializeField, ReadOnly ]
        public bool IsPlaying { get; private set; }
        
        public Tween Tween => recycledTween.Tween;
#endregion

#region Unity API
        private void OnEnable()
        {
            triggeringEvents.OnEnable();
        }
        
        private void OnDisable()
        {
            triggeringEvents.OnDisable();
        }
        
        protected virtual void Awake()
        {
            triggeringEvents.response = EventResponse;
            
			IsPlaying = false;
		}

        private void Start()
        {
            if( !enabled )
                return;

            if( playOnStart )
            {
                if( hasDelay )
					DOVirtual.DelayedCall( delayAmount, Play );
                else
					Play();
			}
        }
        
        private void OnDestroy()
        {
            KillTween();
        }
#endregion

#region API
        [ Button() ]
        public void Play()
        {
            if( recycledTween.Tween == null )
                CreateAndStartTween();
            else
                recycledTween.Tween.Play();
                
			IsPlaying = true;
		}
        
        [ Button() ]
		public void PlayBackwards()
		{
			if( recycledTween.Tween == null )
				CreateAndStartTween( true /* reversed. */ );
			else
				recycledTween.Tween.Play();

			IsPlaying = true;
		}
        
        [ Button(), EnableIf( "IsPlaying" ) ]
        public void Pause()
        {
            if( recycledTween.Tween == null )
                return;

            recycledTween.Tween.Pause();

            IsPlaying = false;
        }
        
        [ Button(), EnableIf( "IsPlaying" ) ]
        public void Stop()
        {
            if( recycledTween.Tween == null )
                return;
                
            recycledTween.Tween.Rewind();

            IsPlaying = false;
        }
        
        [ Button(), EnableIf( "IsPlaying" ) ]
        public void Restart()
        {
            if( recycledTween.Tween == null )
                Play();
            else
            {
                recycledTween.Tween.Restart();

                IsPlaying = true;
            }
        }
#endregion

#region Implementation
        private void EventResponse()
		{
			if( hasDelay_beforeEvents )
				DOVirtual.DelayedCall( delayAmount, Play );
			else
				Play();
		}
        
		protected abstract void CreateAndStartTween( bool isReversed = false );

        protected void OnTweenComplete()
        {
			IsPlaying = false;

            for( var i = 0; i < events_fireOnComplete.Length; i++ )
				events_fireOnComplete[ i ].Raise();

			unityEvents_FireOnComplete.Invoke();
		}

        private void KillTween()
        {
            IsPlaying = false;

			recycledTween.Kill();
		}
#endregion

#region EditorOnly
#if UNITY_EDITOR
#endif
#endregion
    }
}
