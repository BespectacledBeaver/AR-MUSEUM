using UnityEngine;
using Vuforia;
using System.Collections.Generic;

public class CornerTracking : MonoBehaviour
{
    public List<ImageTargetBehaviour> imageTargets; // Assign 8 targets (index 0–7)
    public GameObject placedObject; // Object to place

    private Vector3[] trackedPositions = new Vector3[8];
    private bool[] hasEverBeenTracked = new bool[8];

    void Start()
    {
        for (int i = 0; i < imageTargets.Count; i++)
        {
            int index = i;
            imageTargets[i].OnTargetStatusChanged += (behaviour, status) =>
            {
                if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
                {
                    trackedPositions[index] = behaviour.transform.position;
                    hasEverBeenTracked[index] = true;
                }
            };
        }

        placedObject.SetActive(false); // Hide initially
    }

    void Update()
    {
        bool has1 = hasEverBeenTracked[0]; // Target 1
        bool has3 = hasEverBeenTracked[2]; // Target 3
        bool has8 = hasEverBeenTracked[7]; // Target 8

        if (has1 && has3 && has8)
        {
            Vector3 pos1 = trackedPositions[0];
            Vector3 pos3 = trackedPositions[2];
            Vector3 pos8 = trackedPositions[7];

            Vector3 result = pos1;

            // Axis of greatest difference between 1 and 8
            Vector3 diff18 = pos8 - pos1;
            int primaryAxis = GetMaxAxisIndex(diff18);
            result[primaryAxis] = (pos1[primaryAxis] + pos8[primaryAxis]) / 2f;

            // Next greatest axis between 1 and 3
            Vector3 diff13 = pos3 - pos1;
            int secondaryAxis = GetNextMaxAxisIndex(diff13, primaryAxis);
            result[secondaryAxis] = pos1[secondaryAxis] + 0.25f * (pos3[secondaryAxis] - pos1[secondaryAxis]);

            placedObject.transform.position = result;
            placedObject.SetActive(true);
        }
        else
        {
            placedObject.SetActive(false);
        }
    }

    private int GetMaxAxisIndex(Vector3 v)
    {
        float[] absVals = { Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z) };
        if (absVals[0] >= absVals[1] && absVals[0] >= absVals[2]) return 0;
        if (absVals[1] >= absVals[0] && absVals[1] >= absVals[2]) return 1;
        return 2;
    }

    private int GetNextMaxAxisIndex(Vector3 v, int excludeIndex)
    {
        float[] absVals = { Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z) };
        absVals[excludeIndex] = -1f; // Exclude primary axis
        if (absVals[0] >= absVals[1] && absVals[0] >= absVals[2]) return 0;
        if (absVals[1] >= absVals[0] && absVals[1] >= absVals[2]) return 1;
        return 2;
    }
}
