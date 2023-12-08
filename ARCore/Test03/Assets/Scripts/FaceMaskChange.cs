using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;

public class FaceMaskChange : MonoBehaviour
{
    /*public Material[] facemask;
    private ARFaceManager arFaceManager;

    void Start()
    {
        arFaceManager = gameObject.GetComponent<ARFaceManager>();
    }

    void Update()
    {
        
    }

    void ChangeFaceMask(string faceMaskName)
    {
        for(int i = 0; i < facemask.Length; i++)
        {
            Material faceMaskMaterial = facemask[i];
            if (faceMaskMaterial.name == faceMaskName)
            {
                foreach (ARFace arFace in arFaceManager.trackables)
                {
                    arFace.GetComponent<MeshRenderer>().material = faceMaskMaterial;
                }
                break;
            }
        }
    }*/

    public GameObject[] faceMasks;
    ARFaceManager arFaceManager;

    void Start()
    {
        arFaceManager = gameObject.GetComponent<ARFaceManager>();
    }

    void Update()
    {
        
    }

    public void ChangeFaceMask(string maskName)
    {
        for (int i = 0; i < faceMasks.Length; i++)
        {
            GameObject go = faceMasks[i];

            if(go.name == maskName)
            {
                arFaceManager.facePrefab = go;
                break;
            }
        }
    }

}
