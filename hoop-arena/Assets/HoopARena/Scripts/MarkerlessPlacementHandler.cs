using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MarkerlessPlacementHandler : MonoBehaviour
{   
    // References
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private ARAnchorManager anchorManager;
    [SerializeField] private GameObject prefabObject;

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

            spawnedObject = Instantiate(prefabObject, hit.pose.position, hit.pose.rotation, anchor.transform);
            spawnedObject.transform.localRotation = Quaternion.Euler(-90, 0, 90);

            HidePlanes();

        }
    }

    private void HidePlanes()
    {
        planeManager.enabled = false;

        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
