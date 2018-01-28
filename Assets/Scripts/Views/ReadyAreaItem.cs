using System.Collections;
using System.Collections.Generic;
using Controllers;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class ReadyAreaItem : MonoBehaviour
{
    [HideInInspector]
    public bool IsReady = false;
    public SkeletonGraphic Skeleton;

    void Update()
    {
        if (IsReady)
        {
            Skeleton.enabled = true;
        }
        else
        {
            Skeleton.enabled = false;
        }
    }
}
