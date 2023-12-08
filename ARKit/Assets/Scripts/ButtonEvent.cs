using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public GameObject ScrollView_Face;
    public GameObject ScrollView_Nose;
    public GameObject ScrollView_Eyes;
    public GameObject ScrollView_Mouth;

    void start()
    {

    }

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
        ScrollView_Nose.SetActive(false);
        ScrollView_Eyes.SetActive(false);
        ScrollView_Mouth.SetActive(false);
    }

    public void Nose_button()
    {
        ScrollView_Nose.SetActive(true);
        ScrollView_Face.SetActive(false);
        ScrollView_Eyes.SetActive(false);
        ScrollView_Mouth.SetActive(false);
    }

    public void Eyes_button()
    {
        ScrollView_Eyes.SetActive(true);
        ScrollView_Face.SetActive(false);
        ScrollView_Nose.SetActive(false);
        ScrollView_Mouth.SetActive(false);
    }

    public void Mouth_button()
    {
        ScrollView_Mouth.SetActive(true);
        ScrollView_Face.SetActive(false);
        ScrollView_Nose.SetActive(false);
        ScrollView_Eyes.SetActive(false);
    }
}
