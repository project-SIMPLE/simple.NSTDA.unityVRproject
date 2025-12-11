using UnityEngine;
using Unity.Mathematics;

namespace RopeToolkit.Example
{
    [RequireComponent(typeof(Rigidbody))]
    public class RopeBridgePlank : MonoBehaviour
    {
        public Rope ropeLeft;
        public Rope ropeRight;
        public float extentLeft = -0.5f;
        public float extentRight = 0.5f;
        public float extentPivot = 0.5f;

        [Tooltip("A measure of the longitudal stiffness of the plank. That is, how quickly should the particles on the opposite ropes move to the correct distance between them.")]
        [Range(0.0f, 1.0f)] public float longitudalStiffness = 0.25f;

        public float restingRigidbodyMassMultiplier = 5.0f;

        protected Rigidbody rb;
        protected int particleLeft;
        protected int particleRight;
        protected int particlePivotLeft;
        protected int particlePivotRight;
        protected float distance;
        protected float frameTotalMass;

        public void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            var pointOnBodyLeft = transform.TransformPoint(Vector3.right * extentLeft);
            var pointOnBodyRight = transform.TransformPoint(Vector3.right * extentRight);
            var pointOnBodyPivot = transform.TransformPoint(Vector3.forward * extentPivot);

            if (ropeLeft != null)
            {
                ropeLeft.GetClosestParticle(pointOnBodyLeft, out particleLeft, out float distance);
                ropeLeft.GetClosestParticle(pointOnBodyPivot, out particlePivotLeft, out distance);
            }
            if (ropeRight != null)
            {
                ropeRight.GetClosestParticle(pointOnBodyRight, out particleRight, out float distance);
                ropeRight.GetClosestParticle(pointOnBodyPivot, out particlePivotRight, out distance);
            }

            if (ropeLeft != null && ropeRight != null)
            {
                distance = math.distance(ropeLeft.GetPositionAt(particleLeft), ropeRight.GetPositionAt(particleRight));
            }
        }

        public void FixedUpdate()
        {
            if (rb == null)
            {
                return;
            }
            if (ropeLeft == null || ropeRight == null)
            {
                rb.isKinematic = false;
                return;
            }

            var left = ropeLeft.GetPositionAt(particleLeft);
            var right = ropeRight.GetPositionAt(particleRight);
            var pivot = (ropeLeft.GetPositionAt(particlePivotLeft) + ropeRight.GetPositionAt(particlePivotRight)) * 0.5f;

            left.KeepAtDistance(ref right, distance, longitudalStiffness);

            var middle = (left + right) * 0.5f;

            rb.MoveRotation(Quaternion.LookRotation(pivot - middle, Vector3.Cross(pivot - middle, right - left)));
            rb.MovePosition((Vector3)middle - transform.TransformVector(Vector3.right * (extentLeft + extentRight) * 0.5f));

            ropeLeft.SetPositionAt(particleLeft, left);
            ropeRight.SetPositionAt(particleRight, right);

            var massMultiplier = 1.0f + frameTotalMass * restingRigidbodyMassMultiplier;
            frameTotalMass = 0.0f;

            if (ropeLeft.GetMassMultiplierAt(particleLeft) > 0.0f)
            {
                ropeLeft.SetMassMultiplierAt(particleLeft, massMultiplier);
            }
            if (ropeRight.GetMassMultiplierAt(particleRight) > 0.0f)
            {
                ropeRight.SetMassMultiplierAt(particleRight, massMultiplier);
            }
        }

        public void OnCollisionStay(Collision collision)
        {
            if (collision.rigidbody != null)
            {
                frameTotalMass += collision.rigidbody.mass;
            }
        }

#if UNITY_EDITOR
        public void OnDrawGizmosSelected()
        {
            var pointOnBodyLeft = transform.TransformPoint(Vector3.right * extentLeft);
            var pointOnBodyRight = transform.TransformPoint(Vector3.right * extentRight);
            var pointOnBodyPivot = transform.TransformPoint(Vector3.forward * extentPivot);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pointOnBodyLeft, 0.05f);
            Gizmos.DrawWireSphere(pointOnBodyRight, 0.05f);
            Gizmos.DrawLine(pointOnBodyLeft, pointOnBodyRight);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(pointOnBodyPivot, 0.05f);
            Gizmos.DrawLine(pointOnBodyLeft, pointOnBodyPivot);
            Gizmos.DrawLine(pointOnBodyRight, pointOnBodyPivot);
        }
#endif
    }
}
