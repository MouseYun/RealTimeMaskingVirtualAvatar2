using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom : MonoBehaviour
{
     public GameObject cone;
    public GameObject longface;
    public GameObject shortface;
    public GameObject leye;
    public GameObject reye;
    public GameObject mouth;
    public GameObject pinkhair;
    public GameObject blackhair;
    public GameObject whitehair;

    //GameObject instance;
    //public GameObject prefab;
    //GameObject c6;

    /*void Start()
    {
        c6 = Instantiate(prefab);
    }*/

    public void longfaceclick()
    {
        // instance = (GameObject)Instantiate(longface);
        //instance.SetActive(true);
        longface.SetActive(true);
        shortface.SetActive(false);
    }

    public void shortfaceclick()
    {
        //instance = (GameObject)Instantiate(shortface);
        //instance.SetActive(true);
        shortface.SetActive(true);
        longface.SetActive(false);
    }

}
