using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class UIFollowCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FacingCamera();
        if (!IsUIInforntOfCamera())
        {
            MoveInFrontOfCamera();
        }
    }

    private float CameraDistance = 2f;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.3F;

    private void MoveInFrontOfCamera()
    {
        Vector3 targetPos = Camera.main.transform.TransformPoint(new Vector3(0, 0, CameraDistance));
        this.transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }

    private bool IsUIInforntOfCamera()
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(this.transform.position);
        bool inView = viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                      viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
                      viewportPoint.z > 0;

        return inView;
    }
    private float followSpeed = 5f;
    private void FacingCamera()
    {
        Quaternion lookRotation = Quaternion.LookRotation(this.transform.position - Camera.main.transform.position);

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * followSpeed);
    }
}
