using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class FruitCollectGun : MonoBehaviour
{
    #region Parameters
    [Header("Base")]
    [SerializeField] private bool _hitScanMode;
    [SerializeField] public bool IsHooked;
    [SerializeField] private bool _isUsing;
    public bool IsUsing => _isUsing;
    [SerializeField] private bool shakeToDropFruit = true;
    public bool ShakeToDropFruit => shakeToDropFruit;
    [SerializeField] private LineRenderer _gunRopeLR;
    [SerializeField] private GameObject _launchPoint;
    private Vector3 _lauchPointVector;
    [SerializeField] private float _timeTillReach;

    [Header("Code Assign / Inspector Debug")]
    [SerializeField] private GameObject _focusObject;
    [SerializeField] private FruitBase _focusFruitScript;
    [SerializeField] private GameObject _storeProjectile;

    [Header("Ext Feature / Depricate for projectile")]
    [SerializeField] private bool _safetyOverride;

    [Header("End Hook Related")]
    [SerializeField] private bool _hookIsAtOrigin;
    public bool HookIsAtOrigin => _hookIsAtOrigin;
    [SerializeField] private float _launchForce;
    public float LaunchForce => _launchForce;
    [SerializeField] private GameObject _endHookObj;
    //[SerializeField] private GameObject _hookProjectile;

    [SerializeField] private Rigidbody _endHookRB;
    [SerializeField] private EndHookAttach _endHookScript;


    [Header("Physics")]
    private Rigidbody _rb;

    private Quaternion _previousRotation;
    private float _previousTime;
    public float _sampleTime = 0.1f;

    [SerializeField] private LaserObjectDetection _laserObjectDetection;

    [Header("XR")]
    [SerializeField] private float _flickThreshold;


    [SerializeField]
    private GameObject ShakeHandUI;
    #endregion

    #region Base Method
    private void Awake()
    {
        
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _isUsing = false;

        _endHookObj.transform.position = _launchPoint.transform.position;
        ResetHookStatus();
        RopeIsNotHooked();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //_lauchPointVector = _launchPoint.transform.localPosition;
        
        if (!_hookIsAtOrigin)
        {
            //_gunRopeLR.SetPosition(1, _endHookObj.transform.position);
            //_gunRopeLR.SetPosition(0, _launchPoint.transform.position);
        }

        else
        {
            //_gunRopeLR.SetPosition(1, _gunRopeLR.GetPosition(0));
        }


        if(IsHooked)
        {
            if (shakeToDropFruit)
            {
                HandleFlicking();
            }
            else
            {
                ActivateHook();
                CancelHook();
            }
            
        }    
    }

    #endregion

    

    public void UnactivateAimingModule()
    {
        _isUsing = false;
    }

    public void ActivateAimingModule()
    {
        _isUsing = true;
    }
    public void LaunchProbe()
    {

        if(_safetyOverride)
        {
            if (!IsHooked) //is hooked is define by the probe
            {
                if (_laserObjectDetection.IsCorrectObject)
                {
                    RopeIsHooked();
                    _laserObjectDetection.LaserLineInvisible();
                    /* DrawRopesOverTime()*/
                    ;
                }

            }

            else
            {
                CancelHook();
                // IT's something else.
            }
        }

        else
        {
            if (_endHookObj != null)
            {
                if (_hookIsAtOrigin)
                {
                    //if(_storeProjectile = null)
                    //{
                    //    _endHookObj.SetActive(false);
                    //    LaunchProjectile();
                    //}

                    //else
                    //{
                    //    Destroy(_storeProjectile, 0.1f);
                    //    LaunchProjectile();
                    //}

                    _endHookRB.useGravity = true;
                    _endHookRB.isKinematic = false;


                    DetachHookParent();

                    _endHookRB.AddForce(this.transform.forward * _launchForce, ForceMode.Force);

                    RopeIsHooked();
                    
                    
                }

                else
                {
                    CancelHook();
                }
            }


            else
            {
                Debug.Log("No end hook");
            }
           
          
            //lauch probe
            //if probe hit fruit -> stuck -> check flick
            //else go back to original position.
        }
      
    }

    //public void LaunchProjectile()
    //{
    //    _storeProjectile = Instantiate(_hookProjectile, _launchPoint.transform.position, Quaternion.identity);
    //    _storeProjectile.GetComponent<Rigidbody>().AddForce(this.transform.forward * _launchForce, ForceMode.Force);
    //}
    public void CancelHook()
    {
        //if(_isHooked)
        //{

        

        //}
        if (!_hitScanMode)
        {
            //if(_storeProjectile != null)
            //{
            //    _endHookObj.transform.DOMove(_launchPoint.transform.position, _timeTillReach).OnComplete(ResetHookStatus);
            //}

            
            _endHookObj.transform.position = _launchPoint.transform.position;
            ResetHookStatus();
            RopeIsNotHooked();
        }

        else
        {
            RopeIsNotHooked();
        }
          
       
    }

    public void DetachHookParent()
    {
        _hookIsAtOrigin = false;
        //_endHookObj.transform.SetParent(null);

        //_gunRopeLR.SetPosition(1, _endHookObj.transform.position);
    }

    public void ResetHookStatus()
    {
        _hookIsAtOrigin = true;


        _endHookRB.useGravity = false;
        _endHookRB.isKinematic = true;
        //_endHookRB.velocity = Vector3.zero;
        /*_endHookObj.transform.SetParent(this.transform)*/;
    } 
    


    //public void DrawRopesOverTime()
    //{
    //    Vector3 fruitPosition = _laserObjectDetection.HitObject.transform.position;

    

    //    RopeIsHooked();

    //}

    public void GetFruitFromHook(GameObject desigFruit)
    {
        _focusObject = desigFruit;
        if (shakeToDropFruit)
        {
            ShowShakeText(true);
        }
        //_focusFruitScript = _focusObject.GetComponent<FruitBase>();
    }

    
    
    private void ShowShakeText(bool t)
    {
        if (ShakeHandUI != null)
        {
            ShakeHandUI.SetActive(t);
        }
    }


    public void RopeIsHooked()
    {
        
        if (_hitScanMode)
        {
            IsHooked = true;
            _focusObject = _laserObjectDetection.HitObject;
           
        }
       
        _laserObjectDetection.LaserLineInvisible();
    }
    public void RopeIsNotHooked()
    {
        ShowShakeText(false);
        IsHooked = false;
            _focusObject = null;
        _laserObjectDetection.LaserLineVisible();
        //_endHookObj.GetComponent<EndHookAttach>().ClearPosition(Vector3.zero);

        //if(!_endHookObj.activeSelf)
        //{
        //    _endHookObj.SetActive(true);
        //}

    }

    public void HandleFlicking()
    {

        float currentTime = Time.time;
        if (currentTime - _previousTime >= _sampleTime)
        {
            if (currentTime - _previousTime >= _sampleTime)
            {
                Quaternion currentRotation = this.transform.rotation;
                float angle = Quaternion.Angle(_previousRotation, currentRotation);
                float timeElapsed = currentTime - _previousTime;

                float angularSpeed = angle / timeElapsed;

                if (angularSpeed >= _flickThreshold)
                {
               
                        if (_focusObject.GetComponent<FruitBase>() != null)
                        {
                            _focusFruitScript = _focusObject.GetComponent<FruitBase>();

                            _focusFruitScript.CompareFruitToTool(FruitBase.ToolType.HighTool);
                        }

                    ActivateHook();
                    CancelHook();
                }

                _previousRotation = currentRotation;
                _previousTime = currentTime;
            }
        }

    }
    public void ActivateHook()
    {
        if (_focusObject == null)
            return;
        if(_focusObject.GetComponent<FruitBase>() != null)
        {
            _focusFruitScript.ActiveFruitBunchOnHook();
        }
        
    }

}
