using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeDoor : MonoBehaviour
{
    public Animator animator;
    bool isMoving;
    bool isDoorOpen;
    void Update()
    {
        if (isMoving) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OpenDoor(Input.GetTouch(0).position);
        }

        else if (Input.GetMouseButtonDown(0))
        {
            OpenDoor(Input.mousePosition);
        }
    }

    void OpenDoor(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isMoving = true;
                if (!isDoorOpen)
                {
                    Debug.Log("Opening door");
                    animator.SetTrigger("Open Door");
                }
                else
                {
                    Debug.Log("Closing door");
                    animator.SetTrigger("Close Door");
                }
                isDoorOpen = !isDoorOpen;
            }
        }

        StartCoroutine(SetIsMovingFalseWithDelay());
    }

    IEnumerator SetIsMovingFalseWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isMoving = false;
    }
}
