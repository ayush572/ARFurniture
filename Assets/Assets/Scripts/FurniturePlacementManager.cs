using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.XR.CoreUtils;


public class FurniturePlacementManager : MonoBehaviour
{
    public GameObject SpawnableFurniture;
    public XROrigin sessionOrigin;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    //logic for adding the furniture
    private void Update()
    {
        //first to check if any touch on the screen in the app has happened
        if (Input.touchCount > 0)
        {
            //getting the very first touch
            //whenever we tap on screen, this is called
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                bool collision = raycastManager.Raycast(Input.GetTouch(0).position, raycastHits, TrackableType.PlaneWithinPolygon);
                if (collision)
                {
                    //if collision has happened, then we'll be plaing that object at the position of the raycast
                    GameObject _object = Instantiate(SpawnableFurniture); //creating an instance of the object

                    //placing the object in the real world
                    _object.transform.position = raycastHits[0].pose.position;
                    _object.transform.rotation = raycastHits[0].pose.rotation;
                }
                foreach (var planes in planeManager.trackables) //these trackables store all the planes that has been generated
                {
                    planes.gameObject.SetActive(false);
                }
                //disabling the planeManager so that no more artificial planes are being generated
                planeManager.enabled = false;
            }
        }
    }

    //to switch between furnitures
    public void SwitchFurniture(GameObject _furniture)
    {
        SpawnableFurniture = _furniture;
    }

}
