using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserTrajectory : MonoBehaviour
{
    [SerializeField] private FruitCollectGun _fruitGunScript;
    [SerializeField] LineRenderer _trajectoryLR;
    [SerializeField] private int _resolution = 30; // Number of points in the line
    private float _launchSpeed; // Initial speed of the projectile
    [SerializeField] private Transform _shootPointTransform; // Transform to get the aim direction
    [SerializeField] private LayerMask _layerMask;
    [Header("Crosshair")]
    [SerializeField] private RawImage _crosshairImage;

    [Header("Color")]
    [SerializeField] private Color _alphaZero;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _correctColor;
    [SerializeField] private Color _incorrectColor;

    private void Start()
    {
        _fruitGunScript = GetComponent<FruitCollectGun>();
        _launchSpeed = _fruitGunScript.LaunchForce;


        if (_trajectoryLR == null)
        {
            _trajectoryLR = GetComponent<LineRenderer>();
        }
    }

    private void Update()
    {
        if (_shootPointTransform != null)
        {
            if(_fruitGunScript.IsUsing == true)
            {
                DrawTrajectory();
            }

            else
            {
                ClearTrajectory();
            }
            
        }
    }

    void DrawTrajectory()
    {
        Vector3[] points = new Vector3[_resolution];
        float timeStep = 0.1f;
        float g = Mathf.Abs(Physics.gravity.y); // Gravity
        Vector3 startPosition = _shootPointTransform.transform.position;
        Vector3 launchVelocity = _shootPointTransform.forward * _launchSpeed * Time.fixedDeltaTime;

        for (int i = 0; i < _resolution; i++)
        {
            float t = i * timeStep;
            points[i] = CalculatePoint(startPosition, launchVelocity, t);
            Vector3 endPoint = startPosition;
            // Check for collision
            if (i > 0)
            {
                if (Physics.Raycast(points[i - 1], (points[i] - points[i - 1]).normalized, out RaycastHit hit, Vector3.Distance(points[i - 1], points[i]), _layerMask))
                {
                    // Adjust the trajectory to end at the collision point
                    _trajectoryLR.positionCount = i + 1;
                    _trajectoryLR.SetPositions(points);

                    endPoint = hit.point;
                    

                    if(hit.collider.gameObject.GetComponent<FruitBase>() != null)
                    {
                        SetCrosshairColor(_correctColor);
                        SetCrosshairAtEndPoint(endPoint);
                        SetRayColor(_correctColor);
                    }

                    else
                    {
                        SetCrosshairColor(_incorrectColor);
                        SetCrossHairInvisible();
                        SetRayColor(_incorrectColor);
                    }

                    return;
                }

                else
                {
                    SetCrosshairColor(_defaultColor);
                    SetCrossHairInvisible();
                    SetRayColor(_defaultColor);
                }

               
            }
        }

        _trajectoryLR.positionCount = points.Length;
        _trajectoryLR.SetPositions(points);
    }

    Vector3 CalculatePoint(Vector3 start, Vector3 velocity, float t)
    {
        float g = Mathf.Abs(Physics.gravity.y); // Gravity
        float x = start.x + velocity.x * t;
        float y = start.y + velocity.y * t - 0.5f * g * t * t;
        float z = start.z + velocity.z * t;
        return new Vector3(x, y, z);
    }

    void ClearTrajectory()
    {
        _trajectoryLR.positionCount = 0;
    }

    void SetCrosshairAtEndPoint(Vector3 endPoint)
    {
        if (_crosshairImage != null)
        {
            _crosshairImage.transform.position = endPoint;
        }
    }

    void SetCrossHairInvisible()
    {
        _crosshairImage.color = _alphaZero;
    }

    void SetCrosshairColor(Color crosshairColor)
    {
        _crosshairImage.color = crosshairColor;
    }

    void SetRayColor (Color rayColor)
    {
        _trajectoryLR.startColor = rayColor;
        _trajectoryLR.endColor = rayColor;
    }
}
