using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class DressUpTheGuy : StaticInstance<DressUpTheGuy>
{
    public ClothHandler[] clothes;
    public Dictionary<clothType, ClothHandler> clothesDict;

    public clothType currentCloth;

    protected override void Awake()
    {
        base.Awake();

        clothesDict = clothes.ToDictionary(cloth => cloth.clothType, cloth => cloth);
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Wardrobe.Instance.selectedClothType == clothType.NONE)
            {
                Debug.Log("Aucun vetement selectionné");
                return;
            }
            PutClothOn(Input.GetTouch(0).position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Wardrobe.Instance.selectedClothType == clothType.NONE)
            {
                Debug.Log("Aucun vetement selectionné");
                return;
            }
            PutClothOn(Input.mousePosition);
        }
    }

    void PutClothOn(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                clothesDict[Wardrobe.Instance.selectedClothType].ShowCloth();

                Wardrobe.Instance.HideCloth();

                if (currentCloth != clothType.NONE)
                {
                    clothesDict[currentCloth].HideCloth();
                }

                Debug.Log("Habillé avec " + Wardrobe.Instance.selectedClothType);
            }
        }
    }
}
