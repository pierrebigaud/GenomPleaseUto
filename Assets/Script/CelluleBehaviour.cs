using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelluleBehaviour : MonoBehaviour
{
    public List<Sprite> bodies;
    public List<Sprite> cores;
    public List<Sprite> eyebrows;
    public List<Sprite> mouths;
    public List<GameObject> viruses;
    public List<Sprite> shits;
    public int probability;

    public GameObject body;
    public GameObject core;
    public GameObject eyebrow;
    public GameObject mouth;
    public GameObject shit;
    private GameObject virus;
    private GameObject genome;

    public bool hasLeft = false;
    public bool isBad = false;
    public bool isRejected = false;
    public bool hasWrongGenome = false;
    // Start is called before the first frame update
    void Start()
    {
        //attribue un des randoms prefab de la liste au caracteristiques de la cellule
        this.core.GetComponent<SpriteRenderer>().sprite = cores[UnityEngine.Random.Range(0, cores.Count)];
        this.eyebrow.GetComponent<SpriteRenderer>().sprite = eyebrows[UnityEngine.Random.Range(0, eyebrows.Count)];
        this.mouth.GetComponent<SpriteRenderer>().sprite = mouths[UnityEngine.Random.Range(0, mouths.Count)];
        this.shit.GetComponent<SpriteRenderer>().sprite = shits[UnityEngine.Random.Range(0, shits.Count)];
        this.body.GetComponent<SpriteRenderer>().sprite = bodies[UnityEngine.Random.Range(0, bodies.Count)];

        //decide si le virus est bon ou mauvais
        System.Random gen = new System.Random();
        isBad = gen.Next(100) < probability ? true : false;

        //s'il est mauvais instancie le virus
        if (isBad)
        {
            this.virus = viruses[UnityEngine.Random.Range(0, viruses.Count)];
            this.virus.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}