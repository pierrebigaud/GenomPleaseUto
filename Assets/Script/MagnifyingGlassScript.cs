using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using UnityEngine.UI;

public class MagnifyingGlassScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float[] pos0 = { 1.6f, -9.0f };
    private float[] pos1 = { 1.6f, 0.0f };
    public bool isOn = false;
    public GameObject tentacule;
    public GameObject blackScreen;
    private SpriteRenderer sptRenderer;

    public Button destroyButton;
    public Button okButton;
    public Button mgButton;

    public void Start()
    {
        sptRenderer = blackScreen.GetComponent<SpriteRenderer>();
    }

    public void useMagnifyingGlass(float speed=1)
    {
        if (isOn) //desactive la loupe et lance un fade out du fond noir
        {
            isOn = false;
            sptRenderer.DOFade(0f, 1f);
            destroyButton.GetComponent<Button>().interactable = true;
            okButton.GetComponent<Button>().interactable = true;
            transform.DOMove(new Vector3(pos0[0], pos0[1], -6), speed);
            StartCoroutine(disableBlackScreen(speed));            
        }
        else //active la loupe et lance un fade in du fond noir
        {
            isOn = true;
            blackScreen.SetActive(true);
            sptRenderer.DOFade(0.9f, 1f);
            tentacule.GetComponent<Animator>().ResetTrigger("pushButton");
            tentacule.GetComponent<Animator>().SetTrigger("pushButton");
            transform.DOMove(new Vector3(pos1[0], pos1[1], -6), speed);
            destroyButton.GetComponent<Button>().interactable = false;
            okButton.GetComponent<Button>().interactable = false;
            mgButton.GetComponent<Button>().interactable = false;
        }
    }

    IEnumerator disableBlackScreen(float speed)
    {
        yield return new WaitForSeconds(0.2f);
        mgButton.GetComponent<Button>().interactable = true; //il faut empecher le bouton d'être cliqué en même temps que le fond noir.
        yield return new WaitForSeconds(speed-0.2f);
        if (!isOn)
        {
            blackScreen.SetActive(false);
            
        }
    }

  
}