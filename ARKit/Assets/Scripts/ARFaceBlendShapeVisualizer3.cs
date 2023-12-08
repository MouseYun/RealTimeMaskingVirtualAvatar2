using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARKit;
using UnityEngine.XR.ARSubsystems;

public class ARFaceBlendShapeVisualizer3 : MonoBehaviour
{
    private const float CoefficientValueScale = 100f;

    public SkinnedMeshRenderer mouthMeshRenderer;
    public SkinnedMeshRenderer lEyeMeshRenderer;
    public SkinnedMeshRenderer rEyeMeshRenderer;
    public SkinnedMeshRenderer coneMeshRenderer;
    public SkinnedMeshRenderer faceMeshRenderer;

    private Renderer[] _characterRenderers; // ȭ�� �ۿ� ���� �� ��� ������ ��Ȱ��ȭ �ϱ� ���� ����;

    public Transform leftEyeTransform;
    public Transform rightEyeTransform;

    private ARFace _arFace;
    private ARFaceManager _arFaceManager;
    private ARKitFaceSubsystem _arKitFaceSubsystem;

    private const int BlendShapeIndexLeftEyeBlink = 1;
    private const int BlendShapeIndexRightEyeBlink = 1;

    private const int BlendShapeIndexMouthOpen = 1;
    private const int BlendShapeIndexMouthSmile = 3;
    private const int BlendShapeIndexMouthO = 5;

    // Face Custom
    private const int BlendShapeIndexNoseUpDown = 1;

    private const int BlendShapeIndexMouthSize = 7;

    private const int BlendShapeIndexLeyeSize = 3;
    private const int BlendShapeIndexReyeSize= 3;

    private const int BlendShapeIndexFaceSize = 1;

    //private bool emoteEnabled = false;

    private readonly Dictionary<ARKitBlendShapeLocation, float> _arKitBlendShapeValueTable =
        new Dictionary<ARKitBlendShapeLocation, float>();

    // Start is called before the first frame update
    private void Start()
    {
        _characterRenderers = GetComponentsInChildren<Renderer>();
        _arFace = GetComponent<ARFace>();
        _arFaceManager = FindObjectOfType<ARFaceManager>();

        _arKitFaceSubsystem = _arFaceManager.subsystem as ARKitFaceSubsystem;

        SetupARKitBlendShapeTable();

        _arFace.updated += OnFaceUpdated; // ARFace������ ������Ʈ �� ������ OnFaceUpdated() ����.

        ARSession.stateChanged += OnARSessionStateChanged;
    }

    private void OnARSessionStateChanged(ARSessionStateChangedEventArgs args)
    {
        if(args.state > ARSessionState.Ready && _arFace.trackingState == TrackingState.Tracking)
        {
            foreach(var characterRenderer in _characterRenderers)
            {
                characterRenderer.enabled = true;
            }
        }
        else
        {
            foreach (var characterRenderer in _characterRenderers)
            {
                characterRenderer.enabled = false;
            }
        }
    }

    private void SetupARKitBlendShapeTable()
    {
        _arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.EyeBlinkLeft, 0f);
        _arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.EyeBlinkRight, 0f);

        _arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.JawOpen, 0f);
        _arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.MouthClose, 0f);
        _arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.MouthPucker, 0f);
        _arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.MouthSmileRight, 0f);
    }

    private void OnFaceUpdated(ARFaceUpdatedEventArgs args)
    {
        UpdateArKitBlendShapeValues();
    }

    private void UpdateArKitBlendShapeValues()
    {
        var blendShapeCoefficients
            = _arKitFaceSubsystem.GetBlendShapeCoefficients(_arFace.trackableId, Unity.Collections.Allocator.Temp);

        foreach (var blendShapeCoefficient in blendShapeCoefficients)
        {
            var blendShapeLocation = blendShapeCoefficient.blendShapeLocation;

            if (_arKitBlendShapeValueTable.ContainsKey(blendShapeLocation))
            {
                _arKitBlendShapeValueTable[blendShapeLocation] = blendShapeCoefficient.coefficient * CoefficientValueScale;
            }
        }
    }

    private void Update()
    {
        Apply();
    }

    private void Apply()       
    {
        ApplyEyeBlink();
        ApplyMouse();
    }

    private void ApplyEyeBlink()
    {
        var leftBlinkValue = _arKitBlendShapeValueTable[ARKitBlendShapeLocation.EyeBlinkLeft];
        lEyeMeshRenderer.SetBlendShapeWeight(BlendShapeIndexLeftEyeBlink, leftBlinkValue);

        var RightBlinkValue = _arKitBlendShapeValueTable[ARKitBlendShapeLocation.EyeBlinkRight];
        rEyeMeshRenderer.SetBlendShapeWeight(BlendShapeIndexRightEyeBlink, RightBlinkValue);
    }

    private void ApplyMouse()
    {
        var mouthOpenValue = _arKitBlendShapeValueTable[ARKitBlendShapeLocation.JawOpen] -
            _arKitBlendShapeValueTable[ARKitBlendShapeLocation.MouthClose];

        mouthMeshRenderer.SetBlendShapeWeight(BlendShapeIndexMouthOpen, mouthOpenValue);

        var mouthSmileValue = (_arKitBlendShapeValueTable[ARKitBlendShapeLocation.MouthSmileRight]);

        mouthMeshRenderer.SetBlendShapeWeight(BlendShapeIndexMouthSmile, mouthSmileValue);

        var mouthOValue = (_arKitBlendShapeValueTable[ARKitBlendShapeLocation.MouthPucker]);

        mouthMeshRenderer.SetBlendShapeWeight(BlendShapeIndexMouthO, mouthOValue);
    }

    public void ResetBlendShape()
    {
        for(var i = 0; i < coneMeshRenderer.sharedMesh.blendShapeCount; i++)
        {
            //coneMeshRenderer.SetBlendShapeWeight(i, 0f);
        }
    }

    public void SetNoseUp()
    {
        ResetBlendShape();

        coneMeshRenderer.SetBlendShapeWeight(BlendShapeIndexNoseUpDown, 100f);
    }

    public void SetNoseCenter()
    {
        ResetBlendShape();

        coneMeshRenderer.SetBlendShapeWeight(BlendShapeIndexNoseUpDown, 50f);
    }

    public void SetNoseDown()
    {
        ResetBlendShape();

        coneMeshRenderer.SetBlendShapeWeight(BlendShapeIndexNoseUpDown, 0f);
    }



    public void SetMouthSizeBig()
    {
        ResetBlendShape();

        mouthMeshRenderer.SetBlendShapeWeight(BlendShapeIndexMouthSize, 100f);
    }

    public void SetMouthSizeMiddle()
    {
        ResetBlendShape();

        mouthMeshRenderer.SetBlendShapeWeight(BlendShapeIndexMouthSize, 50f);
    }

    public void SetMouthSizeSmall()
    {
        ResetBlendShape();

        mouthMeshRenderer.SetBlendShapeWeight(BlendShapeIndexMouthSize, 0f);
    }



    public void SetEyeSizeBig()
    {
        ResetBlendShape();

        lEyeMeshRenderer.SetBlendShapeWeight(BlendShapeIndexLeyeSize, 100f);
        rEyeMeshRenderer.SetBlendShapeWeight(BlendShapeIndexReyeSize, 100f);
    }

    public void SetEyeSizeMiddle()
    {
        ResetBlendShape();

        lEyeMeshRenderer.SetBlendShapeWeight(BlendShapeIndexLeyeSize, 50f);
        rEyeMeshRenderer.SetBlendShapeWeight(BlendShapeIndexReyeSize, 50f);
    }

    public void SetEyeSizeSmall()
    {
        ResetBlendShape();

        lEyeMeshRenderer.SetBlendShapeWeight(BlendShapeIndexLeyeSize, 0f);
        rEyeMeshRenderer.SetBlendShapeWeight(BlendShapeIndexReyeSize, 0f);
    }



    public void SetLongFace()
    {
        ResetBlendShape();

        faceMeshRenderer.SetBlendShapeWeight(BlendShapeIndexFaceSize, 0f);
    }

    public void SetShortFace()
    {
        ResetBlendShape();

        faceMeshRenderer.SetBlendShapeWeight(BlendShapeIndexFaceSize, 100f);
    }
}