/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class ColorSetter : MonoBehaviour
	{
#region Fields
		[ TitleGroup( "Setup" ), SerializeField ] Color color;

		static int SHADER_ID_COLOR = Shader.PropertyToID( "_BaseColor" );

		Renderer theRenderer;
		MaterialPropertyBlock propertyBlock;
#endregion

#region Properties
#endregion

#region Unity API
		void Awake()
		{
			theRenderer = GetComponent< Renderer >();

			propertyBlock = new MaterialPropertyBlock();
		}
#endregion

#region API
		public void SetColor( Color color )
		{
			this.color = color;

			SetColor();
		}

		[ Button ]
		public void SetColor()
		{
			theRenderer.GetPropertyBlock( propertyBlock );
			propertyBlock.SetColor( SHADER_ID_COLOR, color );
			theRenderer.SetPropertyBlock( propertyBlock );
		}

		public Color GetColor()
		{
			theRenderer.GetPropertyBlock( propertyBlock );
			return propertyBlock.GetColor( SHADER_ID_COLOR );
		}
		
		public void SetAlpha( float alpha )
		{
			theRenderer.GetPropertyBlock( propertyBlock );
			var currentColor = theRenderer.sharedMaterial.GetColor( SHADER_ID_COLOR );
			propertyBlock.SetColor( SHADER_ID_COLOR, currentColor.SetAlpha( alpha ) );
			theRenderer.SetPropertyBlock( propertyBlock );
		}

		public Tweener TweenColor( Color from, Color to, float duration )
		{
			return DOVirtual.Float( 0, 1, duration,
							 		( float lerpBy ) => SetColor( Color.Lerp( from, to, lerpBy ) ) );
		}

		public Tweener TweenAlpha( float from, float to, float duration )
		{
			return DOVirtual.Float( from, to, duration,
							 		( float val ) => SetAlpha( val ) );
		}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}