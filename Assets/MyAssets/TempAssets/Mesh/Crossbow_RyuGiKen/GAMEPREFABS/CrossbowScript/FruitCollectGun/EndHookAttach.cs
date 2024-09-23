using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndHookAttach : MonoBehaviour
{
    private Transform _hookTransform;

    [Header("Inspector Assign")]
    [SerializeField] private Transform _shootPointTransform;
    [SerializeField] private LineRenderer _hookRope;
    [SerializeField] private GameObject _fruitGunObj;
    private Rigidbody _rb;
    private Vector3 _storeHitPosition;
  

    [Header("Code Assign / Inspector Debug")]
    [SerializeField] private GameObject _attachedObject;
    [SerializeField] private GameObject _fruitObjectDesig;
    [SerializeField] private FruitCollectGun _fruitCollectGun;

    public GameObject FruitObjectDesig => _fruitObjectDesig;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _fruitCollectGun = _fruitGunObj.GetComponent<FruitCollectGun>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!_fruitCollectGun.HookIsAtOrigin)
        {
            //_hookRope.SetPosition(1, _shootPointTransform.position);
        }

        else
        {
            this.transform.position = _shootPointTransform.position;
        }
    }
    void Update()
    {
        /*if (!_fruitCollectGun.HookIsAtOrigin)
        {
            //_hookRope.SetPosition(1, _shootPointTransform.position);
        }

        else
        {
            this.transform.position = _shootPointTransform.position;
        }*/
    }


    private void OnCollisionEnter(Collision collision)
    {
        _attachedObject = collision.gameObject;

        if(_attachedObject.GetComponent<FruitBase>() != null)
        {
            if(_attachedObject.GetComponent<FruitBunchWithCenter>() != null)
            {
                _storeHitPosition = _attachedObject.transform.position;
                Debug.Log(_storeHitPosition);
            }
            else
            {
                _storeHitPosition = this.transform.position;
            }
            
            FreezePosition(_storeHitPosition);
            _fruitObjectDesig = _attachedObject;
            _fruitCollectGun.GetFruitFromHook(_fruitObjectDesig);

        }


    }

    private void OnTriggerEnter(Collider collision)
    {
        _attachedObject = collision.gameObject;

        if (_attachedObject.GetComponent<FruitBase>() != null)
        {
            if (_attachedObject.GetComponent<FruitBunchWithCenter>() != null)
            {
                _storeHitPosition = _attachedObject.transform.position;
                
            }
            else
            {
                _storeHitPosition = this.transform.position;
            }

            FreezePosition(_storeHitPosition);
            _fruitObjectDesig = _attachedObject;
            _fruitCollectGun.GetFruitFromHook(_fruitObjectDesig);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //_fruitCollectGun.IsHooked = false;

        //if (FruitObjectDesig != null)
        //{
           

        //    _fruitObjectDesig = null;
        //}
       
    }

    public void FreezePosition(Vector3 stopAtLocation)
    {
        _fruitCollectGun.IsHooked = true;
        this.transform.position = stopAtLocation;
        _rb.useGravity = false;
        _rb.isKinematic = true;
    }

    public void ClearPosition(Vector3 clearVector)
    {
        this.transform.position = clearVector;
    }    
}
