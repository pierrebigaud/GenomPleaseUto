using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CellCycler : MonoBehaviour
{
    public GameObject[] cells;
    public GameObject cellObject;
    public float delayBeforeDestroy;
    private int cellId = 6;
    //the following properties contains the coordinates of each
    //cell position -> [coordX, coordy, scaleX, scaleY]
    private float[] pos0 = { -8.0f, 2.0f, 27f, 21f };
    private float[] pos1 = { -7.0f, 1.5f, 27f, 21f };
    private float[] pos2 = { -6.0f, 1.0f, 27f, 21f };
    private float[] pos3 = { -4.5f, 0.5f, 27f, 21f };
    private float[] pos4 = { -3.0f, 0.0f, 27f, 21f };
    private float[] pos5 = { -0.0f, 0.0f, 27f, 21f };
    AudioSource audioSource;
    public AudioClip badCellDestroy;
    public AudioClip goodCellDestroy;
    public AudioClip badCellPasses;
    public AudioClip goodCellPasses;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cells = new GameObject[6];
        StartCoroutine(initCellList());
    }

    void Update()
    {

    }

    //Position the 5 cells in the GUI on startup
    public IEnumerator initCellList()
    {
        cells[0] = Instantiate(cellObject, new Vector3(pos0[0], pos0[1], 0), Quaternion.identity);
        cells[0].transform.localScale = new Vector3(.4f, .4f, 1f);
        cells[0].transform.name = "Cell No 0";
        yield return new WaitForSeconds(0.1f);
        cells[1] = Instantiate(cellObject, new Vector3(pos1[0], pos1[1], 0), Quaternion.identity);
        cells[1].transform.localScale = new Vector3(.5f, .5f, 1f);
        cells[1].transform.name = "Cell No 1";
        yield return new WaitForSeconds(0.1f);
        cells[2] = Instantiate(cellObject, new Vector3(pos2[0], pos2[1], 0), Quaternion.identity);
         cells[2].transform.localScale = new Vector3(.6f, .6f, 1f);
        cells[2].transform.name = "Cell No 2";
        yield return new WaitForSeconds(0.1f);
        cells[3] = Instantiate(cellObject, new Vector3(pos3[0], pos3[1], 0), Quaternion.identity);
         cells[3].transform.localScale = new Vector3(.7f, .7f, 1f);
        cells[3].transform.name = "Cell No 3";
        yield return new WaitForSeconds(0.1f);
        cells[4] = Instantiate(cellObject, new Vector3(pos4[0], pos4[1], 0), Quaternion.identity);
        cells[4].transform.localScale = new Vector3(.8f, .8f, 8f);
        cells[4].transform.name = "Cell No 4";
        yield return new WaitForSeconds(0.1f);
        cells[5] = Instantiate(cellObject, new Vector3(pos5[0], pos5[1], 0), Quaternion.identity);
         cells[5].transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        cells[5].transform.name = "Cell No 5";
    }

    //Cycle the cells on the GUI and in the list and adds a new one
    //The function is called each time the player makes a decision
    public void doCellCycle()
    {
        depart(cells[5]);
        cells[4].transform.DOMove(new Vector3(pos5[0], pos5[1], 0), 1);
        cells[4].transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        cells[5] = cells[4];
        cells[3].transform.DOMove(new Vector3(pos4[0], pos4[1], 0), 1);
        cells[3].transform.localScale = new Vector3(.8f, .8f, 1f);
        cells[4] = cells[3];
        cells[2].transform.DOMove(new Vector3(pos3[0], pos3[1], 0), 1f);
        cells[2].transform.localScale = new Vector3(.7f, .7f, 1f);
        cells[3] = cells[2];
        cells[1].transform.DOMove(new Vector3(pos2[0], pos2[1], 0), 1);
        cells[1].transform.localScale = new Vector3(.6f, .6f, 1f);
        cells[2] = cells[1];
        cells[0].transform.DOMove(new Vector3(pos1[0], pos1[1], 0), 1);
        cells[0].transform.localScale = new Vector3(.5f, .5f, 1f);
        cells[1] = cells[0];
        StartCoroutine(addCell());
    }

    //Add a new Cell to the list
    public IEnumerator addCell()
    {
        yield return new WaitForSeconds(0);
        cells[0] = Instantiate(cellObject, new Vector3(pos0[0], pos0[1], 0), Quaternion.identity);
        cells[0].transform.localScale = new Vector3(.4f, .4f, 1f);
        cells[0].transform.name = "Cell No " + cellId++;
    }

    //Send the judge cell to its fate
    public void depart(GameObject cell)
    {
        cell.transform.DOScale(0.5f,1f);
        cell.GetComponent<Animator>().SetBool("isMoving", true);
        if (cell.GetComponentInChildren<CelluleBehaviour>().isRejected)
        {
            cell.transform.DOMove(new Vector3(10, 0, 0), 1); //move to killing zone
            //TODO - Do cell killing animation
            if (cell.GetComponentInChildren<CelluleBehaviour>().isBad)
            {
                //TODO - positive animation
                audioSource.PlayOneShot(badCellDestroy, 1);
            }
            else
            {
                //TODO - negative animation
                audioSource.PlayOneShot(goodCellDestroy, 1);
            }
        }
        else
        {
            cell.transform.DOMove(new Vector3(10, 0, 0), 1); //move out of screen
            if (cell.GetComponentInChildren<CelluleBehaviour>().isBad)
            {
                //TODO - negative animation
                audioSource.PlayOneShot(badCellPasses, 1);
            }
            else
            {
                //TODO - positive animation
                audioSource.PlayOneShot(goodCellPasses, 1);
            }
        }
        StartCoroutine(deleteCell(cell));
    }

    public IEnumerator deleteCell(GameObject cell)
    {
        yield return new WaitForSeconds(1);
        Destroy(cell.transform.gameObject);
    }
}
