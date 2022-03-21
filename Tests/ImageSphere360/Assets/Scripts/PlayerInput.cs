using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public static UnityEvent OnTouchStart = new UnityEvent();
    public static UnityEvent OnTouchEnd = new UnityEvent();

    public static UnityEvent OnTouchpadLeftUp = new UnityEvent();
    public static UnityEvent OnTouchpadRightUp = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {

    }

    void OculusInput()
    {
        
    }

    void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            if (OnTouchStart != null)
                OnTouchStart.Invoke();
        if (Input.GetKeyUp(KeyCode.Space))
            if (OnTouchEnd != null)
                OnTouchEnd.Invoke();
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            if (OnTouchpadLeftUp != null)
                OnTouchpadLeftUp.Invoke();
        if (Input.GetKeyUp(KeyCode.RightArrow))
            if (OnTouchpadRightUp != null)
                OnTouchpadRightUp.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        this.OculusInput();
#elif UNITY_EDITOR
        this.KeyboardInput();
#endif
    }
}
