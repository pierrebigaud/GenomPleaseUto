using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public List<Sprite> goodGenomes;
    public Sprite wrongGenome;

    public void Start()
    {
        //select a wrong genome in the list
        int nbGenome = Random.Range(0, goodGenomes.Count);
        wrongGenome = goodGenomes[nbGenome];
        goodGenomes.RemoveAt(nbGenome);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
