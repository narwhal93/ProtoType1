using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class test : MonoBehaviour {

    [SerializeField]
    SkeletonAnimator upsk;

    [SerializeField]
    SkeletonAnimation downsk;

    void Start()
    {
        Debug.Log("??");
        upsk.skeletonDataAsset = Resources.Load<SkeletonDataAsset>("SpineData/" + DataManager.Instance.m_teamData[0][0][0] + "_SkeletonData");
        downsk.skeletonDataAsset = Resources.Load<SkeletonDataAsset>("SpineData/" + DataManager.Instance.m_teamData[0][0][0] + "_SkeletonData");
        downsk.Initialize(true);
    }

}
