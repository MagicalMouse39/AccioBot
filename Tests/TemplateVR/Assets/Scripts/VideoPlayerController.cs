using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VideoPlayerController : MonoBehaviour
{
    private UdpServer server;

    public int ImageWidth { get; set; }
    public int ImageHeight { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        var server = new UdpServer();

        server.ReceivedFrame += (frameBytes) =>
        {
            var tex = new Texture2D(this.ImageWidth, this.ImageHeight, TextureFormat.PVRTC_RGBA4, false);
            using (var sw = new StreamWriter("test.bmp"))
                sw.Write(frameBytes);
            tex.LoadRawTextureData(frameBytes);
            tex.Apply();
        };

        server.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
