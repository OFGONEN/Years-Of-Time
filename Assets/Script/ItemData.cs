/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[ CreateAssetMenu( fileName = "shared_item_data_", menuName = "FF/Game/Item Data" ) ]
public class ItemData : ScriptableObject
{
#region Fields
  [ Title( "Attributes" ) ]
    [ LabelText( "Level" ), SerializeField ] int item_level;
    [ Tooltip( "Once Duration Reduced to 1, Item will grant Currency" ), SerializeField ] 
    float item_duration;
    [ SerializeField ] float item_currency;
    [ SerializeField ] float item_cost;

  [ Title( "Visual" ) ]
    [ SerializeField ] Sprite item_sprite_background;
    [ SerializeField ] Sprite item_sprite_foreground;
    [ SerializeField ] float item_sprite_fill_bottom;
    [ SerializeField ] float item_sprite_fill_top;
#endregion

#region Properties
	public int ItemLevel               => item_level;
	public float ItemDuration          => item_duration;
	public float ItemCurrency          => item_currency;
	public float ItemCost              => item_cost;
	public Sprite ItemSpriteBackground => item_sprite_background;
	public Sprite ItemSpriteForeground => item_sprite_foreground;
	public float ItemSpriteFillBottom  => item_sprite_fill_bottom;
	public float ItemSpriteFillTop     => item_sprite_fill_top;
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}