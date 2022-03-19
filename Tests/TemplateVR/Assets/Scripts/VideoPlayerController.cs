using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    private UdpServer server;

    public int ImageWidth;
    public int ImageHeight;

    // Start is called before the first frame update
    void Start()
    {
        this.server = new UdpServer();

        var rand = new System.Random();

        server.ReceivedFrame += (frameBytes) =>
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                var tex = new Texture2D(this.ImageWidth, this.ImageHeight);
                tex.LoadImage(frameBytes);
                tex.Apply();

                this.GetComponent<RawImage>().texture = tex;
            });
        };

        server.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
