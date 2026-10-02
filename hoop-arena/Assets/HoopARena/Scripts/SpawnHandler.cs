using UnityEngine;
using UnityEngine.XR.ARFoundation; 

public class SpawnHandler : MonoBehaviour
{
    // reference to Ar image manager
    [SerializeField] private ARTrackedImageManager _arImageManager; 

    // reference to the prefab that we want to spawn 
    [SerializeField] private GameObject _hoopPrefab; 

    // subscribe and unsubscribe from OnTrackedImagesChanged
    private void OnEnable()
    {
        // ensure that the image manager exits
        if (_arImageManager == null) return; 

        _arImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged); 
    }

    private void OnDisable()
    {
        // ensure that the image manager exits
        if (_arImageManager == null) return; 

        _arImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged); 
    }

    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {   
        foreach (ARTrackedImage image in args.added)
        {
            SpawnModel(image); 
        }
    }

    private void SpawnModel(ARTrackedImage image)
    {
        GameObject modelInstance = Instantiate(_hoopPrefab, image.transform);
        modelInstance.transform.localPosition = Vector3.zero; 
        modelInstance.transform.localRotation = Quaternion.Euler(0, 180, 0);  
    }
}
