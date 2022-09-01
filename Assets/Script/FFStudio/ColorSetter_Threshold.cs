/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public class ColorSetter_Threshold : MonoBehaviour
	{
#region Fields (Inspector Interface)
	[ Title( "Setup" ) ]
		public Color color_start;
    	[ SerializeField ] ColorPerThreshold[] colors;
    	[ SerializeField ] SharedFloatNotifier notifier;
#endregion

#region Fields (Private)
		private static int SHADER_ID_COLOR = Shader.PropertyToID( "_BaseColor" );

		private Renderer theRenderer;
		private MaterialPropertyBlock propertyBlock;
#endregion

#region Properties
#endregion

#region Unity API
		void OnEnable()
		{
			if( notifier != null )
				notifier.Subscribe( OnValueChange );
		}
		
		void OnDisable()
		{
			if( notifier != null )
				notifier.Unsubscribe( OnValueChange );
		}

		private void Awake()
		{
			theRenderer = GetComponent< Renderer >();

			propertyBlock = new MaterialPropertyBlock();

			ResetColor();
		}
#endregion

#region API
		public void ResetColor()
		{
			SetColor( color_start );
		}

		[ Button() ]
		public void SetColor( float normalizedValue )
		{
#if UNITY_EDITOR
			if( colors == null )
			{
				FFLogger.LogWarning( name + ": Colors array is null!", this );
				return;
			}
#endif
			
			for( var i = 0; i < colors.Length; i++ )
				if( normalizedValue >= colors[ i ].threshold )
				{
					if( i < colors.Length - 1 )
					{
						var lerpBy = Mathf.InverseLerp( colors[ i ].threshold, colors[ i + 1 ].threshold, normalizedValue );
						SetColor( Color.Lerp( colors[ i ].color, colors[ i + 1 ].color, lerpBy ) );
					}
					else
						SetColor( colors[ i ].color );
				}
		}

		[ Button ]
		public void SetColor( Color color )
		{
			theRenderer.GetPropertyBlock( propertyBlock );
			propertyBlock.SetColor( SHADER_ID_COLOR, color );
			theRenderer.SetPropertyBlock( propertyBlock );
		}
#endregion

#region Implementation
		void OnValueChange()
		{
			SetColor( notifier.SharedValue );
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}