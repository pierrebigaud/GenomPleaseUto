using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagnifyingGlassScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float[] pos0 = { 0.0f, 0.0f };
    private float[] pos1 = { 0.0f, 2.0f };
    private bool isOn = false;

    void useMagnifyingGlass()
    {
        if (isOn)
        {
            transform.DOMove(new Vector3(pos0[0], pos0[1], 0), 1);
            isOn = false;
        }
        else
        {
            transform.DOMove(new Vector3(pos1[0], pos1[1], 0), 1);
            isOn = true;
        }
    }

}
