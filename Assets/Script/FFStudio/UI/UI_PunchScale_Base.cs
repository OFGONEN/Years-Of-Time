/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace FFStudio
{
    public class UI_PunchScale_Base< NotifierType > : UIEntity
    {
#region Fields
    [ Title( "Parameters" ) ]
        [ SerializeField ] float punchScale;

    [ Title( "Observed Shared Data" ) ]
        [ SerializeField ] SharedDataNotifier< NotifierType > notifier_count;

        Vector3 originalScale;
        RecycledTween punchScaleTween;
        float duration;
#endregion

#region Properties
#endregion

#region Unity API
        void OnEnable()
        {
            if( notifier_count )
                notifier_count.Subscribe( OnCountChange );
        }
        
        void OnDisable()
        {
            if( notifier_count )
    			notifier_count.Unsubscribe( OnCountChange );
        }
        
        void Awake()
        {
            originalScale = uiTransform.localScale;

            punchScaleTween = new RecycledTween();

            duration = GameSettings.Instance.ui_Entity_Scale_TweenDuration;
        }
#endregion

#region API
        public void PunchScale()
        {
            punchScaleTween.Kill();
            uiTransform.localScale = originalScale;
            punchScaleTween.Recycle( uiTransform.DOPunchScale( Vector3.one * punchScale, duration, 1, 1 ), OnPunchScaleComplete );

    #if UNITY_EDITOR
            punchScaleTween.Tween.SetId( name + "_ff_UIPunchScale" );
    #endif
        }

        public void PunchScale( float strength )
        {
            punchScaleTween.Kill();
            uiTransform.localScale = originalScale;
            punchScaleTween.Recycle( uiTransform.DOPunchScale( Vector3.one * strength, duration, 1, 1 ), OnPunchScaleComplete );

    #if UNITY_EDITOR
            punchScaleTween.Tween.SetId( name + "_ff_UIPunchScale" );
    #endif
        }
        
        public void CancelAllShakes()
        {
            punchScaleTween.Kill();
        }
#endregion

#region Implementation
        protected virtual void OnCountChange()
        {
            PunchScale();
        }
        
        protected virtual void OnPunchScaleComplete()
        {
            uiTransform.localScale = originalScale;
        }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
    }
}