using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceCube : MonoBehaviour
{
    [SerializeField] private ARRaycastManager rayCastManager;
    private bool isPlacing;

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
        var rayHits = new List<ARRaycastHit>();

        rayCastManager.Raycast(touchPosition, rayHits, TrackableType.AllTypes);

        if (rayHits.Count > 0)
        {
            Vector3 hitPosePosition = rayHits[0].pose.position;
            Quaternion hitPoseRotation = rayHits[0].pose.rotation;
            Instantiate(rayCastManager.raycastPrefab, hitPosePosition, hitPoseRotation);
        }

        StartCoroutine(SetIsPlacingFalseWithDelay());
    }

    IEnumerator SetIsPlacingFalseWithDelay()
    {
        yield return new WaitForSeconds(0.25f);
        isPlacing = false;
    }
}
