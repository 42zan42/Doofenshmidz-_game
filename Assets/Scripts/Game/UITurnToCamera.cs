using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITurnToCamera : MonoBehaviour
{

    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Work(); 
    }

    private void Work()
    {
        transform.forward = _camera.transform.forward;
        //transform.Rotate(0, 180, 0);
    }
}
