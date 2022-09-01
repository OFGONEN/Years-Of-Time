/* Created by and for usage of FF Studios (2021). */

using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using FFStudio;
using Sirenix.OdinInspector;

public class TrajectoryLineRenderer : MonoBehaviour
{
#region Fields
[ Title( "Setup" ) ]
    public float length = 10.0f;
	public float length_max = 20.0f;
    [ SerializeField ] LineRenderer lineRenderer_background;
    [ SerializeField ] LineRenderer lineRenderer_foreground;
	[ SerializeField ] int bounce_count = 10;

	[ SerializeField, LabelText( "Half-width of Platform" ), Tooltip( "Trajectory will \"bounce/zig-zag\" after advancing this far in X direction." ) ] float x_max = 3;
    
    List< Vector3 > waypoints = new List< Vector3 >();
    NativeArray< Vector3 > waypoints_foreground;
    NativeArray< Vector3 > waypoints_background;
#endregion

#region Properties
#endregion

#region Unity API
    void Awake()
    {
		waypoints_foreground = new NativeArray< Vector3 >( bounce_count, Allocator.Persistent );
		waypoints_background = new NativeArray< Vector3 >( bounce_count, Allocator.Persistent );

		StopDrawing();
	}
    
    void OnDisable()
    {
		waypoints_foreground.Dispose();
		waypoints_background.Dispose();
	}
    
    void Update()
    {
		SetLineRendererPoints( length, waypoints_foreground, lineRenderer_foreground );
		// SetLineRendererPoints( length_max, waypoints_background, lineRenderer_background );
	}
#endregion

#region API
    [ Button() ]
    public void StartDrawing()
    {
		// lineRenderer_background.enabled = true;
		lineRenderer_foreground.enabled = true;
	}
    
    [ Button() ]
    public void StopDrawing()
    {
		lineRenderer_background.enabled = false;
		lineRenderer_foreground.enabled = false;
	}
    
    public void DashWaypoints( List< Vector3 > waypoints )
    {
		waypoints.Clear();

		Vector3 posA = transform.position;
		Vector3 posB = posA + transform.forward * length;

        if( PositionIsOutOfBounds( posB ) == false )
            waypoints.Add( posB );
        else
        {
            while( PositionIsOutOfBounds( posB ) )
            {
                var delta = posB - posA;
                int signOfX = delta.x > 0 ? +1 : -1;
				var signOfZ = Mathf.Sign( delta.z );
				var alpha = Mathf.Atan2( delta.z, delta.x );
                var xToBorder = Mathf.Abs( posA.x - signOfX * x_max );
                var newZMagnitude = Mathf.Abs( Mathf.Tan( alpha ) * xToBorder );
                var newPosB = new Vector3( posA.x + signOfX * xToBorder, posA.y, posA.z + signOfZ * newZMagnitude );

                waypoints.Add( newPosB );

                var newDirection = Vector3.Reflect( delta.normalized, Vector3.left * signOfX );

                posA = newPosB;
                posB = new Vector3( newPosB.x + newPosB.x - posB.x, posB.y, posB.z );
            }

            waypoints.Add( posB );
        }
	}
#endregion

#region Implementation
    void SetLineRendererPoints( float length, NativeArray< Vector3 > waypoints, LineRenderer lineRenderer )
    {
        Vector3 posA = transform.position.SetY( lineRenderer.transform.position.y );
        Vector3 posB = posA + transform.forward * length;

		int index = 0;

		waypoints[ index++ ] = posA;

		if( PositionIsOutOfBounds( posB ) == false )
			waypoints[ index++ ] = posB;
		else
        {
            while( PositionIsOutOfBounds( posB ) )
            {
                var delta = posB - posA;
				int signOfX = delta.x > 0 ? +1 : -1;
				var signOfZ = Mathf.Sign( delta.z );
                var alpha = Mathf.Atan2( delta.z, delta.x );
                var xToBorder = Mathf.Abs( posA.x - signOfX * x_max );
                var newZMagnitude = Mathf.Abs( Mathf.Tan( alpha ) * xToBorder );
                var newPosB = new Vector3( posA.x + signOfX * xToBorder, posA.y, posA.z + signOfZ * newZMagnitude );

			    waypoints[ index++ ] = newPosB;

                var newDirection = Vector3.Reflect( delta.normalized, Vector3.left * signOfX );

                posA = newPosB;
                posB = new Vector3( newPosB.x + newPosB.x - posB.x, posB.y, posB.z );
            }

            waypoints[ index++ ] = posB;
        }

		lineRenderer.positionCount = index;
		lineRenderer.SetPositions( waypoints );
	}
    
    bool PositionIsOutOfBounds( Vector3 position )
    {
        return Mathf.Abs( position.x ) > x_max;
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}