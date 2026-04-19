using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class DressUpTheGuy : StaticInstance<DressUpTheGuy>
{
    public ClothHandler[] clothes;
    public Dictionary<clothType, ClothHandler> clothesDict;
    bool isDressing = false;

    public clothType currentCloth;

    protected override void Awake()
    {
        base.Awake();

        clothesDict = clothes.ToDictionary(cloth => cloth.clothType, cloth => cloth);
    }

    void Update()
    {
        if (isDressing)
        {
            return;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PutClothOn(Input.GetTouch(0).position);
        }

        else if (Input.GetMouseButtonDown(0))
        {
            PutClothOn(Input.mousePosition);
        }
    }

    void PutClothOn(Vector2 screenPosition)
    {
        if (Wardrobe.Instance.selectedClothType == clothType.NONE)
        {
            Debug.Log("Aucun vetement selectionné");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isDressing = true;
                clothesDict[Wardrobe.Instance.selectedClothType].ShowCloth();

                Wardrobe.Instance.HideCloth();

                if (currentCloth != clothType.NONE)
                {
                    clothesDict[currentCloth].HideCloth();
                    Wardrobe.Instance.ShowCloth(currentCloth);
                }

                currentCloth = Wardrobe.Instance.selectedClothType;
                Wardrobe.Instance.selectedClothType = clothType.NONE;

                Debug.Log("Habillé avec " + currentCloth);
            }
        }

        StartCoroutine(SetIsDressingFalseWithDelay());
    }

    IEnumerator SetIsDressingFalseWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isDressing = false;
    }
}
