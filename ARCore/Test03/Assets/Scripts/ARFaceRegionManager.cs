using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using Unity.Collections;

public class ARFaceRegionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] regionPrefabs;

    private ARFaceManager faceManager;
    private ARSessionOrigin sessionOrigin;

    private NativeArray<ARCoreFaceRegionData> faceRegions;

    private void Awake()
    {
        faceManager = GetComponent<ARFaceManager>();
        sessionOrigin = GetComponent<ARSessionOrigin>();

        for ( int i = 0; i < regionPrefabs.Length; ++ i)
        {
            regionPrefabs[i] = Instantiate(regionPrefabs[i], sessionOrigin.trackablesParent);
        }
    }

    private void Update()
    {
        ARCoreFaceSubsystem subSystem = (ARCoreFaceSubsystem)faceManager.subsystem;

        foreach (ARFace face in faceManager.trackables)
        {
            subSystem.GetRegionPoses(face.trackableId, Allocator.Persistent, ref faceRegions);

            foreach (ARCoreFaceRegionData faceRegion in faceRegions)
            {
                ARCoreFaceRegion regionType = faceRegion.region;

                regionPrefabs[(int)regionType].transform.localPosition = faceRegion.pose.position;
                regionPrefabs[(int)regionType].transform.localRotation = faceRegion.pose.rotation;
            }
        }
    }
}
