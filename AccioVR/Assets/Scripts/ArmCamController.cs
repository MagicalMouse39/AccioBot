using HTC.UnityPlugin.Multimedia;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmCamController : MonoBehaviour
{
    void Start()
    {
        ViveMediaDecoder.loadVideoThumb(this.gameObject, @"E:\Scuola\Superiori\5° Anno\AccioBot\Tests\TestServer\info.sdp", 0);
    }

    void Update()
    {

    }
}
