using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelluleBehaviour : MonoBehaviour
{
    public List<GameObject> bodies;
    public List<GameObject> cores;
    public List<GameObject> eyebrows;
    public List<GameObject> mouths;
    public List<GameObject> viruses;
    public List<GameObject> shits;
    public int probability;

    private GameObject body;
    private GameObject core;
    private GameObject eyebrow;
    private GameObject mouth;
    private GameObject shit;
    private GameObject virus;

    public bool hasLeft = false;
    public bool isBad = false;
    public bool isRejected = false;
    // Start is called before the first frame update
    void Start()
    {
        //attribue un des randoms prefab de la liste au caracteristiques de la cellule
       
        this.core = cores[UnityEngine.Random.Range(0,cores.Count)];
        this.eyebrow = eyebrows[UnityEngine.Random.Range(0,eyebrows.Count)];
        this.mouth = mouths[UnityEngine.Random.Range(0,mouths.Count)];
        this.shit = shits[UnityEngine.Random.Range(0,shits.Count)];
         this.body = bodies[UnityEngine.Random.Range(0,bodies.Count)];
        

        //instancie les caracteristiques
       instantiateObject(core, "core");
       instantiateObject(eyebrow, "eyebrow");
       instantiateObject(mouth, "mouth");
       instantiateObject(shit, "shit");
       instantiateObject(body, "body");

       //decide si le virus est bon ou mauvais
       System.Random gen = new System.Random();
        isBad = gen.Next(100) < probability ? true : false;

        //s'il est mauvais instancie le virus
       if(isBad){
           this.virus = viruses[UnityEngine.Random.Range(0,viruses.Count)];
           this.virus.SetActive(true);
       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //methode qui permet d'instancier les caracteristiques
    void instantiateObject(GameObject part, string name){
        var objectToInstantiate = Instantiate(part,this.transform.Find(name).transform.position, Quaternion.identity);
        objectToInstantiate.transform.parent = this.transform.Find("body");
    }
}