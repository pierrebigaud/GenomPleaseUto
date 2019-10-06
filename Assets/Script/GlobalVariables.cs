using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVariables : MonoBehaviour
{
    public Sprite[] genomeList;
    public Sprite[] genomeTemplateList;
    public List<Sprite> goodGenomes;
    public Sprite wrongGenome;
    public Image infoBoard;

    public void Start()
    {
        initGenomes();
    }

    public void initGenomes()
    {
        int nbGenome = Random.Range(0, genomeList.Length);
     

        wrongGenome = genomeList[nbGenome];
        infoBoard.GetComponentInChildren<Image>().sprite = genomeTemplateList[nbGenome];

        foreach (Sprite sprite in genomeList)
        {
            if (sprite != wrongGenome)
            {
                goodGenomes.Add(sprite);
            }
        }
    }
}