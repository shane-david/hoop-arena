using UnityEngine; 
using System;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems; 

public class WallDetectionHandler : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] private ARPlaneManager _planeManager;
    [SerializeField] private float _minWallSize = 0.3f; // in meters

    private bool _wallFound;

    // subscribe to trackables changed event
    void OnEnable()
    {
        _planeManager.trackablesChanged.AddListener(OnPlanesChanged); 
    }

    void OnDisable()
    {
        _planeManager.trackablesChanged.RemoveListener(OnPlanesChanged); 
    }

    // determine if the plan that was just created was a wall or not 
    private void OnPlanesChanged(ARTrackablesChangedEventArgs<ARPlane> args)
    {
        // if a wall has alread been found this method does not matter
        if (_wallFound) return;

        // look at all the planes in the args and see if it is a wall
        foreach (var plane in args.added)
        {
            if (IsWall(plane))
            {
                WallFound();
                return; 
            }
        }

        // if the same plane has grown to a usable size, call WallFound
        foreach (var plane in args.updated)
        {
            if (IsWall(plane))
            {
                WallFound();
                return; 
            }
        }
    }

    // determine if a plane is a usable wall
    private bool IsWall(ARPlane plane)
    {
        return plane.alignment == PlaneAlignment.Vertical && plane.size.x >= _minWallSize && plane.size.y >= _minWallSize; 
    }

    // invoke the wall event
    private void WallFound()
    {
        _wallFound = true;
        EventHandler.InvokeWallDetected(); 
    }
}