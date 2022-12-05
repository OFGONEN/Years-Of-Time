/* Created by and for usage of FF Studios (2021). */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using DG.Tweening;

#if UNITY_EDITOR
using Shapes;
using UnityEditor;
#endif

namespace FFStudio
{
    public class TweenChain : MonoBehaviour
    {
#region Fields (Inspector Interface)
	[ Title( "Setup" ) ]
		[ InfoBox( "Transform of this GO will be used unless another one is provided.", "TransformIsNull" ) ]
		[ SerializeField, LabelText( "Target Transform" ) ] Transform transform_target;

    [ Title( "Start Options" ) ]
        public bool playOnStart = false;
        
	[ Title( "Sequence" ) ]
		public Ease easing = Ease.Linear;
		public bool loop;
		[ ShowIf( "loop" ), LabelText( "Loop Type" ) ] public LoopType loop_type = LoopType.Restart;
		[ ShowIf( "loop" ), LabelText( "Infinite Loop" ) ] public bool loop_isInfinite = true;
		[ HideIf( "loop_isInfinite" ), LabelText( "Loop Count" ), Min( 1 ) ] public int loop_count = 1;
		
	[ Title( "Tween Data" ) ]
#if UNITY_EDITOR
	[ TableList( ShowIndexLabels = true ) ]
#endif
	[ SerializeReference ]
        public List< TweenData > tweenDatas = new List< TweenData >();

	[ Title( "Fired Unity Events" ) ]
		[ SerializeField, LabelText( "On Sequence Completion" ) ] UnityEvent unityEvent_onChainComplete;
		
		Transform transform_ToTween;
		
		RecycledSequence recycledSequence = new RecycledSequence();
#endregion

#region Properties
        [ ShowInInspector, ReadOnly ]
		public bool IsPlaying => recycledSequence.Sequence != null && recycledSequence.IsPlaying;
		
		public Sequence Sequence => recycledSequence.Sequence;
#endregion

#region Unity API
        void Awake()
        {
			recycledSequence = new RecycledSequence();
			
			transform_ToTween = transform_target == null ? transform : transform_target;

			foreach( var tweenData in tweenDatas )
				tweenData.Initialize( transform_ToTween );
		}
		
		void OnDisable()
		{
			KillProper();
		}

        void Start()
        {
            if( !enabled )
                return;

            if( playOnStart )
                Play();
        }
#endregion

#region API
        [ Button() ]
        public void Play()
        {
#if UNITY_EDITOR
            if( tweenDatas == null || tweenDatas.Count == 0 )
                FFLogger.LogError( name + ": Tween data array is null or has no elements! Fix this before build!", this );
#endif
			if( IsPlaying )
				Sequence.KillProper();

			recycledSequence.Recycle( unityEvent_onChainComplete.Invoke );

			for( var i = 0; i < tweenDatas.Count; i++ )
			{
				var tweenData = tweenDatas[ i ];
				
				if( tweenData.sequenceElementType == SequenceElementType.Append )
					Sequence.Append( tweenData.CreateTween() );
				else if( tweenData.sequenceElementType == SequenceElementType.Join )
					Sequence.Join( tweenData.CreateTween() );
				else /* if( tweenData.sequenceElementType == SequenceElementType.Insert ) */
					Sequence.Insert( tweenData.insertion_time, tweenData.CreateTween() );
			}

			Sequence.SetEase( easing );

			if( loop )
				Sequence.SetLoops( loop_isInfinite ? -1 : loop_count, loop_type );
		}
		
		public void PlayTween( int index )
		{
			Sequence.KillProper();

			tweenDatas[ index ].CreateTween();
		}
		
		public void JoinTween( int index )
		{
#if UNITY_EDITOR
			if( Sequence != null && Sequence.IsPlaying() )
				FFLogger.LogWarning( name + ": Sequence is already playing.\n" +
									 "Tween will not be joined but played IMMEDIATELY (along with what is already been playing).", this );
#endif
			Sequence.Join( tweenDatas[ index ].CreateTween() );
		}
		
        [ Button(), EnableIf( "IsPlaying" ) ]
        public void Pause()
        {
			Sequence.Pause();
		}
		
        [ Button(), EnableIf( "IsPlaying" ) ]
        public void Stop()
        {
			Sequence.Rewind();
		}
		
        [ Button(), EnableIf( "IsPlaying" ) ]
		public void Complete()
		{
			Sequence.Complete();
		}
		
		[ Button() ]
        public void Kill()
        {
			Sequence.Kill();
		}
		
		[ Button() ]
		public void KillProper()
		{
			Sequence.KillProper();
		}
#endregion

#region Implementation
#endregion

#region EditorOnly
#if UNITY_EDITOR
		Vector3 inPlayMode_currentStartPos;
		GUIStyle style;

		void DrawMovementTweenGizmo( MovementTweenData tweenData, ref Vector3 lastPos, Vector3 verticalOffset, int tweenNo )
		{
			Vector3 startPos = lastPos;

			if( Application.isPlaying )
				startPos = inPlayMode_currentStartPos;

			Color color = new Color( 1.0f, 0.75f, 0.0f );

			Draw.UseDashes = true;
			Draw.DashStyle = DashStyle.RelativeDashes( DashType.Basic, 1, 1 );

			var deltaPosition = tweenData.endValue;

			lastPos = startPos + deltaPosition;

			Draw.Line( startPos + verticalOffset, lastPos + verticalOffset, 0.1f, LineEndCap.None, color );
			var direction = deltaPosition.normalized;
			var deltaMagnitude = deltaPosition.magnitude;
			var coneLength = 0.2f;
			var conePos = Vector3.Lerp( startPos, lastPos, 1.0f - coneLength / deltaMagnitude );
			Draw.Cone( conePos + verticalOffset, deltaPosition.normalized, 0.1f, 0.2f, color );
			Handles.Label( ( lastPos + startPos ) / 2 + verticalOffset, tweenNo.ToString() + ": " + tweenData.description, style );
		}

		void OnDrawGizmosSelected()
		{
			var theTransform = transform_target == null ? transform : transform_target;

			style = new GUIStyle { normal = new GUIStyleState { textColor = Color.red }, fontSize = 20 };

			Vector3 lastPos = theTransform.position;
			if( Application.isPlaying )
			{
				if( IsPlaying == false )
					return;

				// TODO: Only call DrawXXXTweenGizmo() for the current Tween of the Sequence. Hint: May utilize OnUpdate of TweenDatas.
			}
			else
				for( var i = 0; i < tweenDatas.Count; i++ )
					if( tweenDatas[ i ] is MovementTweenData )
						DrawMovementTweenGizmo( tweenDatas[ i ] as MovementTweenData, ref lastPos, Vector3.up * i * 0.3f, i + 1 );
		}
#endif
#endregion
	}
}
