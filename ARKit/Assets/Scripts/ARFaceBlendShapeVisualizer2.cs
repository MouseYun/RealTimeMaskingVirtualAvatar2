using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARKit;
using UnityEngine.XR.ARSubsystems;

public class ARFaceBlendShapeVisualizer2 : MonoBehaviour
{
    /*private const float EyeUpRotationX = -15f;
    private const float EyeDownRotationX = 15f;

    private const float EyeLeftRotationZ = 10f;
    private const float EyeRightRotationZ = -10f;*/

    private const float CoefficientValueScale = 100f;

    public SkinnedMeshRenderer mouthMeshRenderer;
    public SkinnedMeshRenderer lEyeMeshRenderer;
    public SkinnedMeshRenderer rEyeMeshRenderer;

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

        /*_arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.EyeLookInLeft, 0f);
        _arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.EyeLookOutLeft, 0f);
        _arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.EyeLookUpLeft, 0f);
        _arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.EyeLookDownLeft, 0f);

        _arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.EyeLookInRight, 0f);
        _arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.EyeLookOutRight, 0f);
        _arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.EyeLookUpRight, 0f);
        _arKitBlendShapeValueTable.Add(ARKitBlendShapeLocation.EyeLookDownRight, 0f);*/

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
        //ApplyEyeMovement();
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

    /*private void ApplyEyeMovement()
    {
        // 0 ~ 0.5 ~ 1
        // Left Eye
        var leftEyeUpDownLerpValue = (_arKitBlendShapeValueTable[ARKitBlendShapeLocation.EyeLookDownLeft] * -1f
            + _arKitBlendShapeValueTable[ARKitBlendShapeLocation.EyeLookUpLeft]
            + CoefficientValueScale) / (2f * CoefficientValueScale);

        var eyeLeftXRotation = Mathf.Lerp(EyeDownRotationX, EyeUpRotationX, leftEyeUpDownLerpValue);

        var leftEyeInOutLerpValue = (_arKitBlendShapeValueTable[ARKitBlendShapeLocation.EyeLookInLeft] * -1f
            + _arKitBlendShapeValueTable[ARKitBlendShapeLocation.EyeLookOutLeft]
            + CoefficientValueScale) / (2f * CoefficientValueScale);

        var eyeLeftZRotation = Mathf.Lerp(EyeLeftRotationZ, EyeRightRotationZ, leftEyeInOutLerpValue);

        leftEyeTransform.localRotation = Quaternion.Euler(eyeLeftXRotation, 0f, eyeLeftZRotation);

        // Right Eye
        var rightEyeUpDownLerpValue = (_arKitBlendShapeValueTable[ARKitBlendShapeLocation.EyeLookDownRight] * -1f
            + _arKitBlendShapeValueTable[ARKitBlendShapeLocation.EyeLookUpRight]
            + CoefficientValueScale) / (2f * CoefficientValueScale);

        var eyeRightXRotation = Mathf.Lerp(EyeDownRotationX, EyeUpRotationX, rightEyeUpDownLerpValue);

        var rightEyeInOutLerpValue = (_arKitBlendShapeValueTable[ARKitBlendShapeLocation.EyeLookOutRight] * -1f
            + _arKitBlendShapeValueTable[ARKitBlendShapeLocation.EyeLookInRight]
            + CoefficientValueScale) / (2f * CoefficientValueScale);

        var eyeRightZRotation = Mathf.Lerp(EyeLeftRotationZ, EyeRightRotationZ, rightEyeInOutLerpValue);

        rightEyeTransform.localRotation = Quaternion.Euler(eyeRightXRotation, 0f, eyeRightZRotation);
    }*/

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

}