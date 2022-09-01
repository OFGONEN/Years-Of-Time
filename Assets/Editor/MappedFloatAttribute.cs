/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

public class MappedFloatAttributeDrawer : OdinAttributeDrawer< FFStudio.MappedFloat, float >
{
	GUIStyle suffixStyle = new GUIStyle { normal = new GUIStyleState { textColor = Color.gray },
										  fontSize = ( int )( GUI.skin.font.fontSize * 0.85f ),
										  alignment = TextAnchor.MiddleCenter };

	protected override void DrawPropertyLayout( GUIContent label )
    {
        float value = this.ValueEntry.SmartValue;
		var mappedFloat = this.Attribute;

		if( mappedFloat.SuffixLabel != null && label != null )
		{
		    Rect rect = EditorGUILayout.GetControlRect();

			GUIContent guiContent = new GUIContent( mappedFloat.SuffixLabel );
			var textWidth = suffixStyle.CalcSize( guiContent ).x + 5;

			value = SirenixEditorFields.RangeFloatField( rect.SubXMax( textWidth ), label, value * mappedFloat.RangeCeiling,
													     mappedFloat.Minimum,
													     mappedFloat.Maximum ) / mappedFloat.RangeCeiling;

			EditorGUI.LabelField( rect.AlignRight( textWidth - 5 ), mappedFloat.SuffixLabel, suffixStyle );
			
		}
        else
			value = SirenixEditorFields.RangeFloatField( label, value * mappedFloat.RangeCeiling,
													     mappedFloat.Minimum,
													     mappedFloat.Maximum ) / mappedFloat.RangeCeiling;

		this.ValueEntry.SmartValue = value;
    }
}