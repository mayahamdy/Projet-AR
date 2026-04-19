using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARPlaceAvatar : MonoBehaviour
{
    public GameObject Avatar;
    public GameObject Wardrobe;
    public Vector3 WardrobeOffset;
    public Vector3 WardrobeRotationOffset;
    private ARRaycastManager rayCastManager;
    private bool isPlacing;
    private bool isPlaced;
    private GameObject placedAvatar;
    private GameObject placedWardrobe;

    private void Awake()
    {
        rayCastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (!rayCastManager) return;

        if (!isPlacing && (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began ||
            Input.GetMouseButtonDown(0)))
        {
            isPlacing = true;

            if (Input.touchCount > 0)
            {
                PlaceObject(Input.GetTouch(0).position);
            }
            else
            {
                PlaceObject(Input.mousePosition);
            }
        }
    }

    void PlaceObject(Vector2 touchPosition)
    {
        if (isPlaced) return;

        var rayHits = new List<ARRaycastHit>();

        rayCastManager.Raycast(touchPosition, rayHits, TrackableType.AllTypes);

        if (rayHits.Count > 0)
        {
            Vector3 hitPosePosition = rayHits[0].pose.position;
            Quaternion hitPoseRotation = rayHits[0].pose.rotation;

            if (placedAvatar != null)
            {
                Destroy(placedAvatar);
            }
            if (placedWardrobe != null)
            {
                Destroy(placedWardrobe);
            }

            placedAvatar = Instantiate(Avatar, hitPosePosition, hitPoseRotation);
            placedWardrobe = Instantiate(Wardrobe, hitPosePosition, hitPoseRotation);

            Vector3 directionToCamera = Camera.main.transform.position - placedAvatar.transform.position;
            directionToCamera.y = 0;
            placedAvatar.transform.rotation = Quaternion.LookRotation(directionToCamera);

            placedWardrobe.transform.rotation = placedAvatar.transform.rotation * Quaternion.Euler(WardrobeRotationOffset);

            placedWardrobe.transform.position = placedAvatar.transform.position + placedAvatar.transform.TransformDirection(WardrobeOffset);

            isPlaced = true;
        }

        StartCoroutine(SetIsPlacingFalseWithDelay());
    }

    IEnumerator SetIsPlacingFalseWithDelay()
    {
        yield return new WaitForSeconds(0.25f);
        isPlacing = false;
    }
}
