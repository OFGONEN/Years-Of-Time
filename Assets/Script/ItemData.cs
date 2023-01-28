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
    [ SerializeField ] Sprite item_texture_background;
    [ SerializeField ] Sprite item_texture_foreground;
#endregion

#region Properties
    public Sprite ItemTextureBackground => item_texture_background;
    public Sprite ItemTextureForeground => item_texture_foreground;
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}