//=============================================================================================================================
//
// Copyright (c) 2015-2018 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
// EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
// and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
//
//=============================================================================================================================

using UnityEngine;
using EasyAR;

namespace Sample
{
    public class SampleImageTargetBehaviour : ImageTargetBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            TargetFound += OnTargetFound;
            TargetLost += OnTargetLost;
            TargetLoad += OnTargetLoad;
            TargetUnload += OnTargetUnload;
        }

        void OnTargetFound(TargetAbstractBehaviour behaviour)
        {

            Debug.Log("Found: " + Target.Id);
            Driver player = GameObject.FindGameObjectWithTag("Player").GetComponent<Driver>();
            player.UpdateNumOfTargets(1);
        }

        void OnTargetLost(TargetAbstractBehaviour behaviour)
        {

            Debug.Log("Lost: " + Target.Id);
            Driver player = GameObject.FindGameObjectWithTag("Player").GetComponent<Driver>();
            player.UpdateNumOfTargets(-1);
        }

        void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
        {
            Debug.Log("Load target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);

        }

        void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
        {
            Driver player = GameObject.FindGameObjectWithTag("Player").GetComponent<Driver>();
        }
    }
}
