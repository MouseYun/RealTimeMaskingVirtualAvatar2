using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonScript : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public GameObject face1;
    public GameObject face2;

    void Start()
    {
        //btn1.OnClick.AddListener(btn1print);
        //btn2.OnClick.AddListener(delegate{btn2print("Hello");});
        //btn3.onClick.AddListener(() => btn3print("goodbye"));
    
    }

    public void btn1print()
    {
        //Debug.Log("You have clicked the button");
        GameObject face1;
        face1 = Resources.Load("Face1") as GameObject;
        Instantiate(face1);

    }

    public void btn2print()
    {
        //Debug.Log("df");
        GameObject face2;
        face2 = Resources.Load("Face2") as GameObject;
        Instantiate(face2);
    }

    public void btn3print()
    {
        Debug.Log("dg");
    }
}
