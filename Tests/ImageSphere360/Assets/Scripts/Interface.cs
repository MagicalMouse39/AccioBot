using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interface : MonoBehaviour
{
    public EnvironmentLibrary m_EnvironmentLibrary = null;

    [Serializable]
    public class NewEnvironment : UnityEvent<Environment> { }
    public NewEnvironment OnNewEnvironment = null;

    private int m_Index = 0;

    private void Awake()
    {
        PlayerInput.OnTouchStart.AddListener(this.Show);
        PlayerInput.OnTouchEnd.AddListener(this.Hide);

        PlayerInput.OnTouchpadLeftUp.AddListener(this.Previous);
        PlayerInput.OnTouchpadRightUp.AddListener(this.Next);
    }

    void Start()
    {
        this.Select();
    }

    private void OnDestroy()
    {
        PlayerInput.OnTouchStart.RemoveListener(this.Show);
        PlayerInput.OnTouchEnd.RemoveListener(this.Hide);

        PlayerInput.OnTouchpadLeftUp.RemoveListener(this.Previous);
        PlayerInput.OnTouchpadRightUp.RemoveListener(this.Next);
    }

    private void Next()
    {
        this.m_Index++;

        if (this.m_Index == this.m_EnvironmentLibrary.m_Environments.Count)
            this.m_Index = 0;

        this.Select();
    }

    private void Previous()
    {
        this.m_Index--;

        if (this.m_Index == -1)
            this.m_Index = this.m_EnvironmentLibrary.m_Environments.Count - 1;

        this.Select();
    }

    private void Select()
    {
        this.OnNewEnvironment.Invoke(this.m_EnvironmentLibrary.m_Environments[this.m_Index]);
    }

    private void Show()
    {

    }

    private void Hide()
    {

    }
}
