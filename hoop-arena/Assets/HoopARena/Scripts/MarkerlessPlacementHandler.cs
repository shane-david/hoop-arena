using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MarkerlessPlacementHandler : MonoBehaviour
{   
    // References
    [Header("References")]
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private ARAnchorManager anchorManager;
    [SerializeField] private GameObject prefabObject;

    [Header("Spawn Data")]
    [SerializeField] private float wallOfset = 0.05f; 


    // keep track of the AR hits
    private static List<ARRaycastHit> hits = new();

    // keep track of the instanc of the object we spawn 
    private GameObject spawnedObject;

    // subscribe/unsubscribe to OnTouchedScreen
    void OnEnable() => TouchInputHandler.OnTouchedScreen += HandleTouch;
    void OnDisable() => TouchInputHandler.OnTouchedScreen -= HandleTouch;

    // Raycast to the screen position and spawn the prefab if a hit is detected
    private void HandleTouch(Vector2 screenPosition)
    {
        if (spawnedObject != null) return;

        if (raycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            if (hits.Count == 0) return;

            var hit = hits[0];

            var plane = planeManager.GetPlane(hit.trackableId);

            if (plane == null) return;

            var anchor = anchorManager.AttachAnchor(plane, hit.pose);

            if (anchor == null) return;

            var surfaceNormal = hit.pose.rotation * Vector3.up; 
            var placementRotation = Quaternion.LookRotation(surfaceNormal, Vector3.up); 

            spawnedObject = Instantiate(prefabObject, anchor.transform.position, placementRotation, anchor.transform);  
            spawnedObject.transform.localPosition = Vector3.up * wallOfset; 
            spawnedObject.GetComponent<HoopLevelChanger>().CapturePose(); 


            HidePlanes();
        }
    }

    private void HidePlanes()
    {   
        // disable the plane manager
        planeManager.enabled = false;

        foreach (var plane in planeManager.trackables)
        {   
            // hide the mesh visualizers 
            if (plane.TryGetComponent(out ARPlaneMeshVisualizer vis)) vis.enabled = false; 

            // hide the renderers
            if (plane.TryGetComponent(out MeshRenderer mr)) mr.enabled = false;
            if (plane.TryGetComponent(out LineRenderer lr)) lr.enabled = false; 
        }
    }
}
