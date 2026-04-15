using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothSelector : MonoBehaviour
{
    public clothType clothType;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            DetectTouch(Input.GetTouch(0).position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            DetectTouch(Input.mousePosition);
        }
    }

    void DetectTouch(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (Wardrobe.Instance.selectedClothType != clothType.NONE)
                {
                    Wardrobe.Instance.ShowCloth();
                }
                Wardrobe.Instance.selectedClothType = clothType;
                Debug.Log("Selected: " + clothType);
            }
        }
    }
}
