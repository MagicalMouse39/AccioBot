using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;

public class LookAround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
            Debug.Log(string.Format($"Device found with name '{device.name}' and role '{device.characteristics}'"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
