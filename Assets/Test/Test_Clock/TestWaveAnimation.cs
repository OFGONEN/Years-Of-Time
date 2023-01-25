/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using FFStudio;

public class TestWaveAnimation : MonoBehaviour
{
#region Fields
  
    [ FoldoutGroup( "Animation One" ) ] public float animation_one_radius;
    [ FoldoutGroup( "Animation One" ) ] public float animation_one_speed;

    [ FoldoutGroup( "Animation Two" ) ] public float animation_two_radius;
    [ FoldoutGroup( "Animation Two" ) ] public float animation_two_speed;

    Vector3 position_start;
	float animation_two_cofactor = 1f;

	RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
	void Awake()
    {
		position_start = transform.position;
	}
#endregion

#region API
    [ Button() ]
    public void DoWaveAnimationTypeOne()
    {
        FFLogger.Log( "DoWaveAnimation One", this );
		recycledTween.Recycle( transform.DOMove(
			position_start + Random.insideUnitCircle.ConvertV3() * animation_one_radius,
			animation_one_speed )
            .SetSpeedBased(), DoWaveAnimationTypeOne );
	}

	[ Button() ]
	public void DoWaveAnimationTypeTwo()
	{
		var targetPosition = position_start + Random.insideUnitCircle.ConvertV3() * animation_two_radius + Vector3.right * animation_two_radius * animation_two_cofactor;

		FFLogger.Log( "DoWaveAnimation Two: " + targetPosition, this );

		recycledTween.Recycle( transform.DOMove(
			targetPosition,
			animation_two_speed )
			.SetSpeedBased(), DoWaveAnimationTypeTwo );

		animation_two_cofactor *= -1f;
	}

    [ Button() ]
    public void StopWaveAnimation()
    {
		recycledTween.Kill();
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}