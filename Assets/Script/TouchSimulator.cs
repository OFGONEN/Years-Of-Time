using UnityEngine;
using CW.Common;
using Lean.Touch;
using Sirenix.OdinInspector;

/// <summary>This component can be added alongside the <b>LeanTouch</b> component to add simulated touch controls using the mouse.</summary>
[ ExecuteInEditMode ]
[ DisallowMultipleComponent ]
[ RequireComponent( typeof( LeanTouch ) ) ]
[ AddComponentMenu( "Touch Simulator" ) ]
public class TouchSimulator : MonoBehaviour
{
	/// <summary>This allows you to set which texture will be used to show the cursor.</summary>
	public Texture2D FingerTexture { set { texture_hand = value; } get { return texture_hand; } }
	[ SerializeField ] private Texture2D texture_hand;
    
    [ SerializeField, LabelText( "Scale on Click" ) ] Vector2 scale_onClick = Vector2.one;

	private LeanTouch cachedTouch;

#if UNITY_EDITOR
	protected virtual void Reset()
	{
		// Set the finger texture?
		if( FingerTexture == null )
		{
			var guids = UnityEditor.AssetDatabase.FindAssets( "tex_tutorial_hand t:texture2d" );

			if( guids.Length > 0 )
			{
				var path = UnityEditor.AssetDatabase.GUIDToAssetPath( guids[ 0 ] );

				FingerTexture = UnityEditor.AssetDatabase.LoadMainAssetAtPath( path ) as Texture2D;
			}
		}
	}
#endif

	protected virtual void OnEnable()
	{
		cachedTouch = GetComponent<LeanTouch>();
	}

	protected virtual void OnGUI()
	{
		// Show simulated multi fingers?
		if( FingerTexture != null && LeanTouch.Fingers.Count > 0 )
		{
			var isHovering = LeanTouch.Fingers.Count == 1;
			LeanFinger currentFinger= LeanTouch.Fingers[ 0 ];
            // if( isHovering == false )
            //     foreach( var finger in LeanTouch.Fingers )
            //         if( finger.Index != LeanTouch.HOVER_FINGER_INDEX )
			// 			currentFinger = finger;

			var screenPosition = currentFinger.ScreenPosition;

			var scale = isHovering ? Vector2.one : scale_onClick;

			var yScale = FingerTexture.height * scale.y;

			var screenRect = new Rect( 0, 0, FingerTexture.width * scale.x, yScale );

			screenRect.center = new Vector2( screenPosition.x, Screen.height - screenPosition.y + yScale / 2.0f );

			GUI.DrawTexture( screenRect, FingerTexture );
            
			/* var count = 0;

            foreach (var finger in LeanTouch.Fingers)
            {
                if (finger.Index < 0 && finger.Index != LeanTouch.HOVER_FINGER_INDEX)
                {
                    count += 1;
                }
            }

            if (count > 1)
            {
                foreach (var finger in LeanTouch.Fingers)
                {
                    // Simulated fingers have a negative index
                    if (finger.Index < 0)
                    {
                        var screenPosition = finger.ScreenPosition;
                        var screenRect     = new Rect(0, 0, FingerTexture.width, FingerTexture.height);

                        screenRect.center = new Vector2(screenPosition.x, Screen.height - screenPosition.y);

                        GUI.DrawTexture(screenRect, FingerTexture);
                    }
                }
            } */
		}
	}
}

#if UNITY_EDITOR
namespace Lean.Touch.Editor
{
	using UnityEditor;
	using TARGET = TouchSimulator;

	[ CanEditMultipleObjects ]
	[ CustomEditor( typeof( TARGET ) ) ]
	public class TouchSimulator_Editor : CwEditor
	{
		[ InitializeOnLoadMethod ]
		static void Hook()
		{
			LeanTouch_Editor.OnExtendInspector += HandleExtendInspector;
		}

		protected override void OnInspector()
		{
			TARGET tgt; TARGET[] tgts; GetTargets( out tgt, out tgts );

			Draw( "texture_hand", "This allows you to set which texture will be used to show the cursor." );
			Draw( "scale_onClick", "This allows you to set the scale the texture will be shrinked to upon clicking the left mouse button." );
		}

		private static void HandleExtendInspector( LeanTouch touch )
		{
			if( touch.GetComponent< TouchSimulator >() == null )
			{
				if( GUILayout.Button( "Add Tutorial Simulator" ) == true )
				{
					Undo.AddComponent< TouchSimulator >( touch.gameObject );
				}
			}
		}
	}
}
#endif