using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserObjectDetection : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private Transform _laserOrigin;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _rayDistance;
    [SerializeField] private Vector3 _hitPoint;
    [SerializeField] private GameObject _hitObj;
    public GameObject HitObject => _hitObj;
    [SerializeField] private LayerMask _rayLayerMask;


    [Header("Boolean")]
    [SerializeField] private bool _isCorrectObject;
    [SerializeField] private bool _laserWorking;

    public bool IsCorrectObject => _isCorrectObject;

    [Header("VisualStuff")]
    [SerializeField] private bool _busyBool;
    [SerializeField] private Color _hideColor;
    [SerializeField] private Color _busyColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _incorrectObjColor;
    [SerializeField] private Color _correctObjColor;

    [Header("Crosshair")]
    [SerializeField] private RawImage _crosshairImage;
    [Header("Ref")]
    [SerializeField] private FruitCollectGun fruitCollectGun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleRaycast();
    }

    public void HandleRaycast()
    {
        if(fruitCollectGun.IsUsing)
        {
            if (_laserWorking)
            {
                if (Physics.Raycast(_laserOrigin.position, _laserOrigin.transform.forward, out RaycastHit hit, _rayDistance, _rayLayerMask))
                {

                    _hitPoint = hit.point;
                    _hitObj = hit.collider.gameObject;


                    _lineRenderer.SetPosition(0, transform.position);
                    _lineRenderer.SetPosition(1, _hitPoint);

                    //_crosshairImage.transform.position = _hitPoint;


                    if (_hitObj.GetComponent<FruitBase>() != null)
                    {
                        _isCorrectObject = true;

                        _lineRenderer.startColor = _correctObjColor;
                        _lineRenderer.endColor = _correctObjColor;
                        _crosshairImage.color = _correctObjColor;
                    }

                    else
                    {
                        _isCorrectObject = false;

                        _lineRenderer.startColor = _incorrectObjColor;
                        _lineRenderer.endColor = _incorrectObjColor;
                        //_crosshairImage.color = _incorrectObjColor;
                    }
                }
                else
                {




                    _hitPoint = Vector3.zero;



                    _lineRenderer.SetPosition(0, transform.position);
                    _lineRenderer.SetPosition(1, transform.position + transform.forward * _rayDistance);
                    //_crosshairImage.transform.position = transform.position + transform.forward * _rayDistance;
                    //_crosshairImage.color = _defaultColor;
                    _lineRenderer.startColor = _defaultColor;
                    _lineRenderer.endColor = _defaultColor;

                }

            }

            else
            {
                _isCorrectObject = false;
            }
        }




       else
        {
            LaserLineInvisible();
        }

    }

    public void LaserLineInvisible()
    {
        _laserWorking = false;
        if (_busyBool)
        {
            _lineRenderer.startColor = _busyColor;
            _lineRenderer.endColor = _busyColor;
        }

        else
        {
            _lineRenderer.startColor = _hideColor;
            _lineRenderer.endColor = _hideColor;

            //_crosshairImage.color = _busyColor;
        }

        if(!fruitCollectGun.IsUsing)
        {
            //_crosshairImage.color = _hideColor;
        }

    }

    public void LaserLineVisible()
    {
        _laserWorking = true;
    }
}
