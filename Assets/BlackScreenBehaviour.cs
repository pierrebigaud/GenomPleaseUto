using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlackScreenBehaviour : MonoBehaviour
{
    public MagnifyingGlassScript mgScript;

    void OnMouseDown()
    {
        if (mgScript.isOn)
        {
            mgScript.useMagnifyingGlass();
        }
    }
}
