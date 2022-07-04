/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public abstract class UIProgressIndicator : UIEntity
	{
#region Fields
		public SharedFloatNotifier indicatorProgress;

		public float offsetPercentage;

		protected Vector3[] indicatingParentWorldPos = new Vector3[ 4 ];
		protected Vector3 indicator_BasePosition;
		protected Vector3 indicator_EndPosition;

		RectTransform indicatingParent;
#endregion

#region Unity API
        void OnEnable()
        {
			indicatorProgress.Subscribe( OnProgressChange );
		}

        void OnDisable()
        {
			indicatorProgress.Unsubscribe( OnProgressChange );
        }

        public override void Start()
        {
			base.Start();

			indicatingParent = uiTransform.parent.GetComponent< RectTransform >();
			indicatingParent.GetWorldCorners( indicatingParentWorldPos );

			offsetPercentage = offsetPercentage / 100;

			GetIndicatorPositions();
			OnProgressChange();
		}
#endregion

#region API
#endregion

#region Implementation
        protected abstract void OnProgressChange();
		protected abstract void GetIndicatorPositions();
#endregion
	}
}