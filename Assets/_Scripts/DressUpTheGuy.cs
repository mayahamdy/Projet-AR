using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DressUpTheGuy : MonoBehaviour
{
    public ClothHandler[] clothes;
    public Dictionary<clothType, ClothHandler> clothesDict;

    public clothType selectedClothType;

    private void Awake()
    {
        clothesDict = clothes.ToDictionary(cloth => cloth.clothType, cloth => cloth);

        //TESTING
        selectedClothType = clothType.T_SHIRT;
    }

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
                clothesDict[selectedClothType].ShowCloth();
                Debug.Log("Objet AR cliqué !");
            }
        }
    }
}
