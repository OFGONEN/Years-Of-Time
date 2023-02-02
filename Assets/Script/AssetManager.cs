/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace FFStudio
{
	/* This class holds references to ScriptableObject assets. These ScriptableObjects are singletons, so they need to load before a Scene does.
	 * Using this class ensures at least one script from a scene holds a reference to these important ScriptableObjects. */
	public class AssetManager : MonoBehaviour
	{
#region Fields
	[ Title( "UnityEvent" ) ]
	[ SerializeField ] UnityEvent onAwakeEvent;
	[ SerializeField ] UnityEvent onEnableEvent;
	[ SerializeField ] UnityEvent onStartEvent;

	[ Title( "Setup" ) ]
		[ SerializeField ] GameSettings gameSettings;
		[ SerializeField ] CurrentLevelData currentLevelData;

	[ Title( "Pool" ) ]
		[ SerializeField ] Pool_UIPopUpText pool_UIPopUpText;
		[ SerializeField ] PoolClock pool_clock;
		[ SerializeField ] PoolUIPopUpText pool_item_popUp_text;
#endregion

#region UnityAPI
		void OnEnable()
		{
			onEnableEvent.Invoke();
		}

		void Awake()
		{
			Vibration.Init();

			pool_UIPopUpText.InitPool( transform, false );
			pool_item_popUp_text.InitPool( transform, false );
			pool_clock.InitPool( transform, false );

			onAwakeEvent.Invoke();
		}

		void Start()
		{
			onStartEvent.Invoke();
		}
#endregion

#region API
		public void VibrateAPI( IntGameEvent vibrateEvent )
		{
			switch ( vibrateEvent.eventValue )
			{
				case 0:
					Vibration.VibratePeek();
					break;
				case 1:
					Vibration.VibratePop();
					break;
				case 2:
					Vibration.VibrateNope();
					break;
				default:
					Vibration.Vibrate();
					break;
			}
		}
#endregion

#region Implementation
#endregion
	}
}