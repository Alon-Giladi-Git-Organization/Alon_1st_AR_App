using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackingHandler : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject objectToSpawn;

    private void OnEnable()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackablesChanged.AddListener(OnTrackablesChanged);
        }
    }

    private void OnDisable()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackablesChanged.RemoveListener(OnTrackablesChanged);
        }
    }

    private void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            if (objectToSpawn != null)
            {
                objectToSpawn.transform.position = trackedImage.transform.position;
                objectToSpawn.transform.rotation = trackedImage.transform.rotation;
                objectToSpawn.SetActive(true);
            }
        }
        else
        {
            if (objectToSpawn != null)
            {
                objectToSpawn.SetActive(false);
            }
        }
    }
}
