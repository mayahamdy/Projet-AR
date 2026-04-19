using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wardrobe : StaticInstance<Wardrobe>
{
    public ClothSelector[] clothSelectors;
    public Dictionary<clothType, ClothSelector> clothesDict;

    public clothType selectedClothType;

    protected override void Awake()
    {
        base.Awake();

        clothesDict = clothSelectors.ToDictionary(cloth => cloth.clothType, cloth => cloth);
    }

    public void HideCloth()
    {
        clothesDict[selectedClothType].gameObject.SetActive(false);
    }

    public void ShowCloth()
    {
        clothesDict[selectedClothType].gameObject.SetActive(true);
    }

    public void ShowCloth(clothType clothType)
    {
        clothesDict[clothType].gameObject.SetActive(true);
    }
}
