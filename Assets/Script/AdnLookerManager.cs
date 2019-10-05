using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AdnLookerManager : MonoBehaviour
{
    /// this script is used to see the genome from the cell
    /// he possess two method, appear and hide
    /// 

    // sprite inside the Magnyfying Glass (MG) (Glass)
    public GameManager glass;

    // placeToMove the glass to function
    public Vector3[] pathToCell;

    public Vector3 pathToHide;

    // gameObject Glass
    public Transform MG, genomePlace;

    // game manager to take the last cell
    public GameManager gameManagerCellExam;

    // genome
    public GameObject genomeToTest;

    //appear is a method that shows the genome from the game manager celltoexam 
    public void Appear()
    {
        // first take the genome and place it under the transparent loop

        //genomeToTest = gameManagerCellExam.cellToExam.Genome;
        genomeToTest.transform.position = genomePlace.position;

        //to DOmoveby
        MG.transform.DOBlendableMoveBy(pathToCell[0], 1);
        MG.transform.DOBlendableMoveBy(pathToCell[1], 2);
    }

    // move the MG
    public void Hide()
    {
        MG.transform.DOBlendableMoveBy(pathToHide, 1);
    }
}