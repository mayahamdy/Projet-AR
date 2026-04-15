using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothHandler : MonoBehaviour
{
    private SkinnedMeshRenderer renderer;
    public clothType clothType;

    private void Awake()
    {
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void ShowCloth()
    {
        renderer.enabled = true;
    }

    public void HideCLoth()
    {
        renderer.enabled = false;
    }
}

public enum clothType
{
    T_SHIRT,
    PULL,
    ROBE
}
