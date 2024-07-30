using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairRemainScale : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera; // Assign the main camera in the inspector
    private Vector3 _initialScale;
    private float _initialDistance;

    void Start()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }

        _initialScale = transform.localScale;
        _initialDistance = Vector3.Distance(transform.position, _mainCamera.transform.position);
    }

    void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, _mainCamera.transform.position);
        float scaleFactor = currentDistance / _initialDistance;
        transform.localScale = _initialScale * scaleFactor;
    }
}
