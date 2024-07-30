using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;

namespace RopeToolkit
{
    public class RopeMouseInteraction : MonoBehaviour
    {
        public Mesh indicatorMesh;
        public Material indicatorMaterial;

        public List<Rope> ropes;

        protected bool ready;
        protected Rope pulledRope;
        protected int pulledParticle;
        protected float pulledDistance;
        protected float3 currentPosition;
        protected float3 targetPosition;

        protected Rope GetClosestRope(Ray ray, out int closestParticleIndex, out float closestDistanceAlongRay)
        {
            closestParticleIndex = -1;
            closestDistanceAlongRay = 0.0f;

            var closestRopeIndex = -1;
            var closestDistance = 0.0f;
            for (int i = 0; i < ropes.Count; i++)
            {
                ropes[i].GetClosestParticle(ray, out int particleIndex, out float distance, out float distanceAlongRay);

                if (distance < closestDistance || closestRopeIndex == -1)
                {
                    closestRopeIndex = i;
                    closestParticleIndex = particleIndex;
                    closestDistance = distance;
                    closestDistanceAlongRay = distanceAlongRay;
                }
            }

            return closestRopeIndex != -1 ? ropes[closestRopeIndex] : null;
        }
        
        public void FixedUpdate()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Input.GetMouseButton(0))
            {
                // Mouse down
                if (ready && pulledRope == null)
                {
                    // Not pulling a rope, find the closest one to the mouse
                    var closestRope = GetClosestRope(ray, out int closestParticleIndex, out float closestDistanceAlongRay);

                    if (closestRope != null && closestParticleIndex != -1 && closestRope.GetMassMultiplierAt(closestParticleIndex) > 0.0f)
                    {
                        // Found a rope and particle on the rope, start pulling that particle!
                        pulledRope = closestRope;
                        pulledParticle = closestParticleIndex;
                        pulledDistance = closestDistanceAlongRay;

                        ready = false;
                    }
                }
            }
            else
            {
                // Mouse up
                if (pulledRope != null)
                {
                    // Stop pulling the rope
                    pulledRope.SetMassMultiplierAt(pulledParticle, 1.0f);
                    pulledRope = null;
                }
            }

            if (pulledRope != null)
            {
                // We are pulling the rope

                // Adjust the grab plane
                pulledDistance += Input.mouseScrollDelta.y * 2.0f;

                // Move the rope particle to the mouse position on the grab-plane
                currentPosition = pulledRope.GetPositionAt(pulledParticle);
                targetPosition = ray.GetPoint(pulledDistance);

                pulledRope.SetPositionAt(pulledParticle, targetPosition);
                pulledRope.SetVelocityAt(pulledParticle, float3.zero);
                pulledRope.SetMassMultiplierAt(pulledParticle, 0.0f);

                // Split the rope if spacebar is pressed!
                if (Input.GetKey(KeyCode.Space))
                {
                    ropes.Remove(pulledRope);

                    var newRopes = new Rope[2];
                    pulledRope.SplitAt(pulledParticle, newRopes);
                    if (newRopes[0] != null) ropes.Add(newRopes[0]);
                    if (newRopes[1] != null) ropes.Add(newRopes[1]);

                    pulledRope = null;
                }
            }
        }

        public void Update()
        {
            if (!Input.GetMouseButton(0))
            {
                ready = true;
            }

            if (indicatorMesh == null || indicatorMaterial == null)
            {
                return;
            }

            if (pulledRope != null)
            {
                Graphics.DrawMesh(indicatorMesh, Matrix4x4.TRS(currentPosition, Quaternion.identity, Vector3.one * 0.25f), indicatorMaterial, 0);
            }
        }
    }
}
