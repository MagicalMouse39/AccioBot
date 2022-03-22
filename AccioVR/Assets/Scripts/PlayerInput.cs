using OVR.OpenVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    // Oculus events
    public static UnityEvent JoystickMoved = new UnityEvent();

    // Debug events
    public static UnityEvent MovementForward = new UnityEvent();

    void Start()
    {
        MovementForward.AddListener(() => {
            Debug.Log("Player moved forwards");
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            if (MovementForward != null)
                MovementForward.Invoke();
        
    }
}
