using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 _offsetXYZ;
    [SerializeField] private float _smooth = 0.5f;
    [SerializeField] private float _forwardOffsetMultiplier = 1.0f;
    private Transform _target;
    private Vector3 _velocity;

    public void SetTarget(Transform target)
    {
        _target = target;   
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(_target == null) 
        {
            return;
        }
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, _target.position + _offsetXYZ, ref _velocity, _smooth);
        transform.position = newPosition;
    }
}
