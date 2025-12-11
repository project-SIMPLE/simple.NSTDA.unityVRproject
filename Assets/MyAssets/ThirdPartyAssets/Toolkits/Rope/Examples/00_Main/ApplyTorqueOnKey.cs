using UnityEngine;

namespace RopeToolkit.Example
{
    public class ApplyTorqueOnKey : MonoBehaviour
    {
        public Vector3 relativeTorque;
        public float maxAngularSpeed;

        public KeyCode key;

        protected Rigidbody rb;

        public void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            if (rb == null)
            {
                return;
            }

            if (Input.GetKey(key))
            {
                var torqueAxis = relativeTorque.normalized;
                var strength = Mathf.SmoothStep(relativeTorque.magnitude, 0.0f, Vector3.Dot(torqueAxis, rb.angularVelocity) / maxAngularSpeed);
                rb.AddRelativeTorque(torqueAxis * strength, ForceMode.Force);
            }
        }
    }
}