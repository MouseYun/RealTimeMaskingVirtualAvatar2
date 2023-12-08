using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCustomSet : MonoBehaviour
{
    // >>>>>>>>>>>>>>>>>>>>>>>>>>>>> Nose

    public void SetNoseUp()
    {
        FindObjectOfType<ARFaceBlendShapeVisualizer3>().SetNoseUp();
    }

    public void SetNoseCenter()
    {
        FindObjectOfType<ARFaceBlendShapeVisualizer3>().SetNoseCenter();
    }

    public void SetNoseDown()
    {
        FindObjectOfType<ARFaceBlendShapeVisualizer3>().SetNoseDown();
    }

    // >>>>>>>>>>>>>>>>>>>>>>>>>>>>> Mouth

    public void SetMouthSizeBig()
    {
        FindObjectOfType<ARFaceBlendShapeVisualizer3>().SetMouthSizeBig();
    }

    public void SetMouthSizeMiddle()
    {
        FindObjectOfType<ARFaceBlendShapeVisualizer3>().SetMouthSizeMiddle();
    }

    public void SetMouthSizeSmall()
    {
        FindObjectOfType<ARFaceBlendShapeVisualizer3>().SetMouthSizeSmall();
    }

    // >>>>>>>>>>>>>>>>>>>>>>>>>>>>> Eye

    public void SetEyeSizeBig()
    {
        FindObjectOfType<ARFaceBlendShapeVisualizer3>().SetEyeSizeBig();
    }

    public void SetEyeSizeMiddle()
    {
        FindObjectOfType<ARFaceBlendShapeVisualizer3>().SetEyeSizeMiddle();
    }

    public void SetEyeSizeSmall()
    {
        FindObjectOfType<ARFaceBlendShapeVisualizer3>().SetEyeSizeSmall();
    }

    // >>>>>>>>>>>>>>>>>>>>>>>>>>>>> Face

    public void SetShortFace()
    {
        FindObjectOfType<ARFaceBlendShapeVisualizer3>().SetShortFace();
    }

    public void SetLongFace()
    {
        FindObjectOfType<ARFaceBlendShapeVisualizer3>().SetLongFace();
    }
}
