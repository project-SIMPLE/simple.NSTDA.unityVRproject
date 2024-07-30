using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RopeToolkit
{
    public static class RigidbodyExtensions
    {
        public static void GetLocalInertiaTensor(this Rigidbody rb, out float3x3 localInertiaTensor)
        {
            var rot = new float3x3(rb.inertiaTensorRotation);
            var invRot = math.transpose(rot);

            localInertiaTensor = math.mul(math.mul(rot, float3x3.Scale(rb.inertiaTensor)), invRot);
        }

        public static void GetInertiaTensor(this Rigidbody rb, out float3x3 inertiaTensor)
        {
            rb.GetLocalInertiaTensor(out float3x3 localInertiaTensor);

            var rot = new float3x3(rb.rotation);
            var invRot = math.transpose(rot);

            inertiaTensor = math.mul(math.mul(rot, localInertiaTensor), invRot);
        }

        public static void GetInvInertiaTensor(this Rigidbody rb, out float3x3 invInertiaTensor)
        {
            rb.GetLocalInertiaTensor(out float3x3 localTensor);

            float3x3 invLocalTensor = float3x3.zero;
            if (math.determinant(localTensor) != 0.0f)
            {
                invLocalTensor = math.inverse(localTensor);
            }

            var rot = new float3x3(rb.rotation);
            var invRot = math.transpose(rot);

            invInertiaTensor = math.mul(math.mul(rot, invLocalTensor), invRot);
        }

        public static void ApplyImpulseNow(this Rigidbody rb, ref float3x3 invInertiaTensor, float3 point, float3 impulse)
        {
            if (rb.mass == 0.0f)
            {
                return;
            }

            var relativePoint = point - (float3)rb.worldCenterOfMass;
            var angularMomentumChange = math.cross(relativePoint, impulse);
            var angularVelocityChange = math.mul(invInertiaTensor, angularMomentumChange);

            rb.velocity += (Vector3)impulse / rb.mass;
            rb.angularVelocity += (Vector3)angularVelocityChange;
        }

        public static void ApplyImpulseNow(this Rigidbody rb, float3 point, float3 impulse)
        {
            rb.GetInvInertiaTensor(out float3x3 invInertiaTensor);
            rb.ApplyImpulseNow(ref invInertiaTensor, point, impulse);
        }

        public static void SetPointVelocityNow(this Rigidbody rb, ref float3x3 invInertiaTensor, float3 point, float3 normal, float desiredSpeed, float damping = 1.0f)
        {
            if (rb.mass == 0.0f)
            {
                return;
            }

            var velocityChange = desiredSpeed - math.dot(rb.GetPointVelocity(point), normal) * damping;
            var relativePoint = point - (float3)rb.worldCenterOfMass;

            var denominator = (1.0f / rb.mass) + math.dot(math.cross(math.mul(invInertiaTensor, math.cross(relativePoint, normal)), relativePoint), normal);
            if (denominator == 0.0f)
            {
                return;
            }

            var j = velocityChange / denominator;
            rb.ApplyImpulseNow(ref invInertiaTensor, point, j * normal);
        }

        public static void SetPointVelocityNow(this Rigidbody rb, float3 point, float3 normal, float desiredSpeed, float damping = 1.0f)
        {
            rb.GetInvInertiaTensor(out float3x3 invInertiaTensor);
            rb.SetPointVelocityNow(ref invInertiaTensor, point, normal, desiredSpeed, damping);
        }
    }
}
