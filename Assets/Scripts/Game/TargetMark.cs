using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMark : MonoBehaviour
{

    public bool IsTarget { get; set; } = false;

    private MeshRenderer _meshRenderer;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _meshRenderer.enabled = false;
    }
    private void LateUpdate()
    {
       _meshRenderer.enabled = IsTarget;
       IsTarget = false;
    }
}
