using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static float _xInput;
    static float _zInput;

    public static float xInput { get { return _xInput; } private set { } }
    public static float zInput { get { return _zInput; } private set { } }

    private void Update()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _zInput = Input.GetAxisRaw("Vertical");
    }
}
