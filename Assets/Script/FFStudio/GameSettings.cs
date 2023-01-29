/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class GameSettings : ScriptableObject
    {
#region Fields (Settings)
    // Info: You can use Title() attribute ONCE for every game-specific group of settings.
    [ Title( "Clock Movement" ) ]
		[ LabelText( "Clock Height While Sitting Idle" ) ] public float clock_height_idle;
		[ LabelText( "Clock Movement Height" ) ] public float clock_movement_height;
		[ LabelText( "Clock Movement Speed Vertical" ) ] public float clock_movement_speed_vertical;
		[ LabelText( "Clock Movement Speed Horizontal" ) ] public float clock_movement_speed_horizontal;
		[ LabelText( "Clock Movement Rotate Speed" ) ] public float clock_movement_speed_rotate;
		[ LabelText( "Clock Movement Rotate Clamp" ) ] public float clock_movement_rotate_clamp;

    [ Title( "Clock" ) ]
		[ LabelText( "Clock Spawn Punch Scale" ) ] public PunchScaleTween clock_spawn_punchScale;
		[ LabelText( "Clock Wave Animation Radius" ) ] public float clock_animation_wave_radius;
		[ LabelText( "Clock Wave Animation Speed" ) ] public float clock_animation_wave_speed;
		[ LabelText( "Clock Slot Return Duration" ) ] public float clock_slot_return_duration;
		[ LabelText( "Clock Slot Return Ease" ) ] public Ease clock_slot_return_ease;
		[LabelText( "Clock Slot Target Slot Move Duration" )] public float clock_slot_go_duration;
		[LabelText( "Clock Slot Target Slot Move Ease" )] public Ease clock_slot_go_ease;
		[ LabelText( "Clock Slot Search Distance" ) ] public float clock_slot_search_distance;

    [ Title( "Spawn Slot" ) ]
		[ LabelText( "Highlight Positive Color" ) ] public Color slot_spawn_highlight_color_positive;
		[ LabelText( "Highlight Negative Color" ) ] public Color slot_spawn_highlight_color_negative;
		[ LabelText( "Highlight Default Color" ) ] public Color slot_spawn_highlight_color_default;

    [ Title( "Clock Slot" ) ]
		[ LabelText( "Highlight Positive Color" ) ] public Color slot_clock_highlight_color_positive;
		[ LabelText( "Highlight Negative Color" ) ] public Color slot_clock_highlight_color_negative;
		[ LabelText( "Highlight Default Color" ) ] public Color slot_clock_highlight_color_default;
		[ LabelText( "Highlight Alpha Cofactor" ) ] public float slot_clock_highlight_alpha_cofactor;

    [ Title( "Item" ) ]
		[ LabelText( "Produce Punch Scale" ) ] public PunchScaleTween item_produce_tween_punchScale;
		[ LabelText( "Produce Start Color" ) ] public Color item_produce_start_color;
		[ LabelText( "Produce Stop Color" ) ] public Color item_produce_stop_color;
		[ LabelText( "Produce Tween Duration" ) ] public float item_produce_duration;
		[ LabelText( "Produce Tween Ease" ) ] public Ease item_produce_ease;
		[ LabelText( "Locked Sprite" ) ] public Sprite item_locked_sprite;

    [ Title( "Item Unlock" ) ]
		[ LabelText( "UI Height Offset" ) ] public float item_unlock_height;

    [ Title( "Play Area" ) ]
		[ LabelText( "Size Array" ) ] public float[] playArea_size_array;
		[ LabelText( "Size Change Duration" ) ] public float playArea_size_duration;
		[ LabelText( "Size Change Ease" ) ] public Ease playArea_size_ease;
		[ LabelText( "Size Count Row" ) ] public int playArea_size_count_row;
		[ LabelText( "Size Count Column" ) ] public int playArea_size_count_column;
		[ LabelText( "Spawn Slot Count" ) ] public int playArea_spawn_slot_count;

    [ Title( "Camera" ) ]
        [ LabelText( "Follow Speed (Z)" ), SuffixLabel( "units/seconds" ), Min( 0 ) ] public float camera_follow_speed_depth = 2.8f;
    
    [ Title( "Project Setup", "These settings should not be edited by Level Designer(s).", TitleAlignments.Centered ) ]
        public int maxLevelCount;
		[ Layer(), SerializeField ] int game_selection_layer;
		[ System.NonSerialized ] public int game_selection_layer_mask;
		public float game_selection_distance;
        
        // Info: 3 groups below (coming from template project) are foldout by design: They should remain hidden.
		[ FoldoutGroup( "Remote Config" ) ] public bool useRemoteConfig_GameSettings;
        [ FoldoutGroup( "Remote Config" ) ] public bool useRemoteConfig_Components;

        [ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the movement for ui element"          ) ] public float ui_Entity_Move_TweenDuration;
        [ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the fading for ui element"            ) ] public float ui_Entity_Fade_TweenDuration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the scaling for ui element"           ) ] public float ui_Entity_Scale_TweenDuration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the movement for floating ui element" ) ] public float ui_Entity_FloatingMove_TweenDuration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Joy Stick"                                        ) ] public float ui_Entity_JoyStick_Gap;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Pop Up Text relative float height"                ) ] public float ui_PopUp_height;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Pop Up Text float duration"                       ) ] public float ui_PopUp_duration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Random Spawn Area in Screen" ), SuffixLabel( "percentage" ) ] public float ui_particle_spawn_width; 
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Spawn Duration" ), SuffixLabel( "seconds" ) ] public float ui_particle_spawn_duration; 
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Spawn Ease" ) ] public Ease ui_particle_spawn_ease;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Wait Time Before Target" ) ] public float ui_particle_target_waitTime;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Target Travel Time" ) ] public float ui_particle_target_duration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Target Travel Ease" ) ] public Ease ui_particle_target_ease;
        [ FoldoutGroup( "UI Settings" ), Tooltip( "Percentage of the screen to register a swipe"     ) ] public int swipeThreshold;
        [ FoldoutGroup( "UI Settings" ), Tooltip( "Safe Area Base Top Offset" ) ] public int ui_safeArea_offset_top = 88;

    [ Title( "UI Particle" ) ]
		[ LabelText( "Random Spawn Area in Screen Witdh Percentage" ) ] public float uiParticle_spawn_width_percentage = 10;
		[ LabelText( "Spawn Movement Duration" ) ] public float uiParticle_spawn_duration = 0.1f;
		[ LabelText( "Spanwn Movement Ease" ) ] public DG.Tweening.Ease uiParticle_spawn_ease = DG.Tweening.Ease.Linear;
		[ LabelText( "Target Travel Wait Time" ) ] public float uiParticle_target_waitDuration = 0.16f;
		[ LabelText( "Target Travel Duration" ) ] public float uiParticle_target_duration = 0.4f;
		[ LabelText( "Target Travel Duration (REWARD)" ) ] public float uiParticle_target_duration_reward = 0.85f;
		[ LabelText( "Target Travel Ease" ) ] public DG.Tweening.Ease uiParticle_target_ease = DG.Tweening.Ease.Linear;


        [ FoldoutGroup( "Debug" ) ] public float debug_ui_text_float_height;
        [ FoldoutGroup( "Debug" ) ] public float debug_ui_text_float_duration;
#endregion

#region Fields (Singleton Related)
        static GameSettings instance;

        delegate GameSettings ReturnGameSettings();
        static ReturnGameSettings returnInstance = LoadInstance;

#endregion

#region Properties
		public static GameSettings Instance => returnInstance();
		public int PlayAreaSize => playArea_size_count_row * playArea_size_count_column;
#endregion

#region API
		public void OnAwake()
		{
			game_selection_layer_mask = 1 << game_selection_layer;
		}
#endregion

#region Implementation
        static GameSettings LoadInstance()
		{
			if( instance == null )
				instance = Resources.Load< GameSettings >( "game_settings" );

			returnInstance = ReturnInstance;

			return instance;
		}

		static GameSettings ReturnInstance()
        {
            return instance;
        }
#endregion
    }
}