using HTC.UnityPlugin.Multimedia;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmCamController : VideoSourceController
{
    protected override void Start()
    {
        this.LOG_TAG = "[ArmCamSourceController]";
        base.Start();
    }

    void Update()
    {

    }
}
