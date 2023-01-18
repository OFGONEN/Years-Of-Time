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
	public class TweenPlayer : MonoBehaviour
	{
#region Fields
	[ Title( "Setup" ) ]
		[ InfoBox( "Transform of this GO will be used unless another one is provided.", "TransformIsNull" ) ]
		[ SerializeField, LabelText( "Target Transform" ) ] Transform transform_target;

    [ Title( "Start Options" ) ]
        public bool playOnStart = false;
	
	[ Title( "Tween Data" ) ]
	[ SerializeReference ]
        public TweenData tweenData;

		Transform transform_ToTween;
		RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
        [ ShowInInspector, ReadOnly ]
		public bool IsPlaying => recycledTween.IsPlaying;
		public Tween Tween    => recycledTween.Tween;
#endregion

#region Unity API
        void Awake()
        {
			recycledTween = new RecycledTween();
			
			transform_ToTween = transform_target == null ? transform : transform_target;
			tweenData.Initialize( transform_ToTween );
		}
		
		void OnDisable()
		{
			Kill();
		}

        void Start()
        {
            if( playOnStart ) 
				Play();

		}
#endregion

#region API
#endregion

#region Implementation
        [ Button() ]
        public void Play()
        {
			recycledTween.Recycle( tweenData.CreateTween() );
		}

        [ Button(), EnableIf( "IsPlaying" ) ]
        public void Pause()
        {
			Tween.Pause();
		}
		
        [ Button(), EnableIf( "IsPlaying" ) ]
        public void Stop()
        {
			Tween.Rewind();
		}
		
        [ Button(), EnableIf( "IsPlaying" ) ]
		public void Complete()
		{
			recycledTween.CompleteAndKill();
		}
		
		[ Button() ]
        public void Kill()
        {
			recycledTween.Kill();
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}