using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class UIFollowCam : MonoBehaviour
{
    [SerializeField]
    private float CameraDistance = 2f;
    [SerializeField]
    private float CameraMinimumDistance = 0.5f;

    private void OnEnable()
    {
        FacingCamera();
        MoveInFrontOfCamera();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void LateUpdate()
    {
        FacingCamera();
        if (!IsUIInforntOfCamera() || !IsUIOnCorrectDistanceFromCamera())
        {
            SmoothMoveInFrontOfCamera();
        }
    }
    void Update()
    {
        
    }


    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.1f;

    private void SmoothMoveInFrontOfCamera()
    {
        Vector3 targetPos = Camera.main.transform.TransformPoint(new Vector3(0, 0, CameraDistance));
        targetPos.y = 1.2f;
        this.transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
    public void MoveInFrontOfCamera()
    {
        Vector3 targetPos = Camera.main.transform.TransformPoint(new Vector3(0, 0, CameraDistance));
        targetPos.y = 1.2f;
        this.transform.position = targetPos;
    }

    private bool IsUIInforntOfCamera()
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(this.transform.position);
        bool inView = viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                      viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
                      viewportPoint.z > 0;

        return inView;
    }
    private float followSpeed = 20f;
    private void FacingCamera()
    {
        Quaternion lookRotation = Quaternion.LookRotation(this.transform.position - Camera.main.transform.position, Vector3.up);

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * followSpeed);
    }
    private bool IsUIOnCorrectDistanceFromCamera()
    {
        float distance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
        if (distance >= 2.0f || distance <= CameraMinimumDistance)
        {
            return false;
        }
        else return true;
    }
}
