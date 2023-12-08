using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public GameObject ScrollView_Face;
    public GameObject ScrollView_Hair;
    public GameObject ScrollView_Eyes;
    public GameObject ScrollView_Mouth;

    public void GoStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void GoMainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Face_button()
    {
        ScrollView_Face.SetActive(true);
        ScrollView_Hair.SetActive(false);
        ScrollView_Eyes.SetActive(false);
        ScrollView_Mouth.SetActive(false);
    }

    public void Hair_button()
    {
        ScrollView_Hair.SetActive(true);
        ScrollView_Face.SetActive(false);
        ScrollView_Eyes.SetActive(false);
        ScrollView_Mouth.SetActive(false);
    }

    public void Eyes_button()
    {
        ScrollView_Eyes.SetActive(true);
        ScrollView_Face.SetActive(false);
        ScrollView_Hair.SetActive(false);
        ScrollView_Mouth.SetActive(false);
    }

    public void Mouth_button()
    {
        ScrollView_Mouth.SetActive(true);
        ScrollView_Face.SetActive(false);
        ScrollView_Hair.SetActive(false);
        ScrollView_Eyes.SetActive(false);
    }
}
