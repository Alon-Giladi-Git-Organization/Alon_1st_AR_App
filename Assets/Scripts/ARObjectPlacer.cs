 using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.XR.ARFoundation; 
using UnityEngine.XR.ARSubsystems; 
  
public class ARObjectPlacer : MonoBehaviour 
{ 
    public GameObject prefabToPlace; 
  
    private ARRaycastManager raycastManager; 
    private static List<ARRaycastHit> hits = new(); 
  
    private GameObject spawnedObject; 
  
    void Awake() 
    { 
        raycastManager = GetComponent<ARRaycastManager>(); 
    } 
  
    void Update() 
    { 
        if (Input.touchCount == 0) 
            return; 
  
        Touch touch = Input.GetTouch(0); 
  
        if (touch.phase != TouchPhase.Began) 
            return; 
  
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon)) 
        { 
            Pose hitPose = hits[0].pose; 
  
            if (spawnedObject == null) 
            { 
                spawnedObject = Instantiate( 
                    prefabToPlace, 
                    hitPose.position, 
                    hitPose.rotation); 
            } 
            else 
            { 
                spawnedObject.transform.position = hitPose.position; 
            } 
        } 
    } 
} 
