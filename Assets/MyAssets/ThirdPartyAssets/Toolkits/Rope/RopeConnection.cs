using UnityEngine;
using Unity.Mathematics;

namespace RopeToolkit
{
    public enum RopeConnectionType : int
    {
        PinRopeToTransform = 0,
        PinTransformToRope = 1,
        PullRigidbodyToRope = 2,
        TwoWayCouplingBetweenRigidbodyAndRope = 3,
    }

    [RequireComponent(typeof(Rope))]
    public class RopeConnection : MonoBehaviour
    {
        protected static readonly Color[] colors = new Color[4]
        {
            new Color(0.69f, 0.0f, 1.0f), // purple
            new Color(1.0f, 0.0f, 0.0f), // red
            new Color(1.0f, 0.0f, 0.0f), // red
            new Color(1.0f, 1.0f, 0.0f), // yellow
        };

        [System.Serializable]
        public struct RigidbodySettings
        {
            [Tooltip("The rigidbody to connect to")]
            public Rigidbody body;

            [Tooltip("A measure of the stiffness of the connection. Lower values are usually more stable.")]
            [Range(0.0f, 1.0f)] public float stiffness;

            [Tooltip("The amount of the rigidbody velocity to remove when the impulse is from the rope is applied to the rigidbody")]
            [Range(0.0f, 1.0f)] public float damping;
        }

        [System.Serializable]
        public struct TransformSettings
        {
            [Tooltip("The transform to connect to")]
            public Transform transform;
        }

        [DisableInPlayMode] public RopeConnectionType type;
        [DisableInPlayMode, Range(0.0f, 1.0f)] public float ropeLocation;
        public bool autoFindRopeLocation = false;

        public RigidbodySettings rigidbodySettings = new RigidbodySettings()
        {
            stiffness = 0.1f,
            damping = 0.1f,
        };

        public TransformSettings transformSettings = new TransformSettings()
        {};

        [Tooltip("The point in local object space to connect to")]
        public float3 localConnectionPoint;

        protected Rope rope;
        protected int particleIndex;

        public Component connectedObject
        {
            get
            {
                switch (type)
                {
                    case RopeConnectionType.PinRopeToTransform:
                    case RopeConnectionType.PinTransformToRope: {
                        return transformSettings.transform;
                    }
                    case RopeConnectionType.PullRigidbodyToRope:
                    case RopeConnectionType.TwoWayCouplingBetweenRigidbodyAndRope: {
                        return rigidbodySettings.body;
                    }
                    default: {
                        return null;
                    }
                }
            }
        }

        public float3 connectionPoint
        {
            get
            {
                var obj = connectedObject;
                if (obj)
                {
                    return obj.transform.TransformPoint(localConnectionPoint);
                }
                else
                {
                    return float3.zero;
                }
            }
        }

        public void Initialize(bool forceReset)
        {
            if (rope && !forceReset)
            {
                return;
            }

            rope = GetComponent<Rope>();
            Debug.Assert(rope); // required component!

            if (autoFindRopeLocation)
            {
                rope.GetClosestParticle(connectionPoint, out particleIndex, out float distance);
                ropeLocation = rope.GetScalarDistanceAt(particleIndex);
            }
            else
            {
                var ropeDistance = ropeLocation * rope.measurements.realCurveLength;
                particleIndex = rope.GetParticleIndexAt(ropeDistance);
            }
        }

        public void OnRopeSplit(Rope.OnSplitParams p)
        {
            if (autoFindRopeLocation)
            {
                // There is no way to determine which side of the split this component was located, just remove it...
                Destroy(this);
            }
            else
            {
                var idx = p.preSplitMeasurements.GetParticleIndexAt(ropeLocation * p.preSplitMeasurements.realCurveLength);
                if (idx < p.minParticleIndex || idx > p.maxParticleIndex)
                {
                    Destroy(this);
                }
            }
        }

        public void OnDisable()
        {
            if (rope && type == RopeConnectionType.PinRopeToTransform)
            {
                rope.SetMassMultiplierAt(particleIndex, 1.0f);
            }
        }

        protected void EnforceConnection()
        {
            Initialize(false);

            if (!rope || !connectedObject)
            {
                return;
            }
            
            switch (type)
            {
                case RopeConnectionType.PinRopeToTransform:
                {
                    rope.SetMassMultiplierAt(particleIndex, 0.0f);
                    rope.SetPositionAt(particleIndex, connectionPoint);
                    break;
                }
                case RopeConnectionType.PinTransformToRope:
                {
                    var target = rope.GetPositionAt(particleIndex, true);
                    var offset = (float3)(transformSettings.transform.TransformPoint(localConnectionPoint) - transformSettings.transform.position);
                    transformSettings.transform.position = target - offset;
                    break;
                }
                case RopeConnectionType.PullRigidbodyToRope:
                {
                    var target = rope.GetPositionAt(particleIndex, false);
                    var current = connectionPoint;
                    var delta = target - current;
                    var dist = math.length(delta);
                    if (dist > 0.0f)
                    {
                        var normal = delta / dist;
                        var correctionVelocity = dist * rigidbodySettings.stiffness / Time.fixedDeltaTime;
                        rigidbodySettings.body.SetPointVelocityNow(current, normal, correctionVelocity, rigidbodySettings.damping);
                    }
                    break;
                }
                case RopeConnectionType.TwoWayCouplingBetweenRigidbodyAndRope:
                {
                    rope.RegisterRigidbodyConnection(
                        particleIndex,
                        rigidbodySettings.body,
                        rigidbodySettings.damping,
                        connectionPoint,
                        rigidbodySettings.stiffness);
                    break;
                }
            }
        }

        protected bool ShouldEnforceInFixedUpdate()
        {
            // Prefer FixedUpdate() whenever possible to avoid stalling while waiting for jobs to complete
            bool isPhysics =
                type != RopeConnectionType.PinRopeToTransform &&
                type != RopeConnectionType.PinTransformToRope;

            bool isInterpolating = rope && rope.interpolation != RopeInterpolation.None;

            return isPhysics || !isInterpolating;
        }

        public void Update()
        {
            if (!ShouldEnforceInFixedUpdate())
            {
                EnforceConnection();
            }
        }

        public void FixedUpdate()
        {
            if (ShouldEnforceInFixedUpdate())
            {
                EnforceConnection();
            }
        }

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                return;
            }
            var rope = GetComponent<Rope>();
            if (!rope || rope.spawnPoints.Count < 2 || !connectedObject)
            {
                return;
            }

            var objPoint = connectionPoint;

            Gizmos.color = colors[(int)type];

            Gizmos.DrawWireCube(objPoint, Vector3.one * 0.05f);

            if (!autoFindRopeLocation)
            {
                var localToWorld = (float4x4)rope.transform.localToWorldMatrix;
                var ropeLength = rope.spawnPoints.GetLengthOfCurve(ref localToWorld);
                rope.spawnPoints.GetPointAlongCurve(ref localToWorld, ropeLength * ropeLocation, out float3 ropePoint);

                Gizmos.DrawWireCube(ropePoint, Vector3.one * 0.05f);
                Gizmos.DrawLine(ropePoint, objPoint);
            }
        }
#endif
    }
}
