using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RopeToolkit
{
    public static class PointsExtensions
    {
        // Curve length
        public static float GetLengthOfCurve(this NativeArray<float3> curve, ref float4x4 transform, bool isLoop = false)
        {
            if (curve == null || curve.Length == 0)
            {
                return 0.0f;
            }
            var sum = 0.0f;
            var firstPoint = math.mul(transform, new float4(curve[0], 1.0f)).xyz;
            var lastPoint = firstPoint;
            for (int i = 1; i < curve.Length; i++)
            {
                var point = math.mul(transform, new float4(curve[i], 1.0f)).xyz;
                sum += math.distance(lastPoint, point);
                lastPoint = point;
            }
            if (isLoop)
            {
                sum += math.distance(lastPoint, firstPoint);
            }
            return sum;
        }

        public static float GetLengthOfCurve(this NativeArray<float3> curve, bool isLoop = false)
        {
            var transform = float4x4.identity;
            return curve.GetLengthOfCurve(ref transform, isLoop);
        }

        public static float GetLengthOfCurve(this IEnumerable<float3> curve, ref float4x4 transform, bool isLoop = false)
        {
            var array = new NativeArray<float3>(curve.ToArray(), Allocator.Temp);
            var sum = array.GetLengthOfCurve(ref transform, isLoop);
            array.Dispose();
            return sum;
        }

        public static float GetLengthOfCurve(this IEnumerable<float3> curve, bool isLoop = false)
        {
            var transform = float4x4.identity;
            return curve.GetLengthOfCurve(ref transform, isLoop);
        }

        // Curve points
        private static void GetPointAlongCurve(this NativeArray<float3> curve, ref float4x4 transform, float distance, out float3 point, ref int currentTargetIndex, ref float accumulatedLength)
        {
            if (curve.Length < 2)
            {
                throw new System.ArgumentException(nameof(curve));
            }
            if (currentTargetIndex < 1 || currentTargetIndex >= curve.Length)
            {
                throw new System.ArgumentOutOfRangeException(nameof(currentTargetIndex));
            }

            var previousTarget = curve[currentTargetIndex - 1];
            while (currentTargetIndex < curve.Length)
            {
                var target = curve[currentTargetIndex];
                var segmentLength = math.distance(previousTarget, target);

                if (distance <= accumulatedLength + segmentLength)
                {
                    var interpolated = math.lerp(previousTarget, target, (distance - accumulatedLength) / segmentLength);
                    point = math.mul(transform, new float4(interpolated, 1.0f)).xyz;
                    return;
                }

                currentTargetIndex++;
                accumulatedLength += segmentLength;
                previousTarget = target;
            }

            // numerical precision made this happen, just return last point
            currentTargetIndex = curve.Length - 1;
            point = math.mul(transform, new float4(previousTarget, 1.0f)).xyz;
        }

        public static void GetPointAlongCurve(this NativeArray<float3> curve, ref float4x4 transform, float distance, out float3 point)
        {
            var currentTargetIndex = 1;
            var accumulatedLength = 0.0f;
            curve.GetPointAlongCurve(ref transform, distance, out point, ref currentTargetIndex, ref accumulatedLength);
        }

        public static void GetPointAlongCurve(this NativeArray<float3> curve, float distance, out float3 point)
        {
            var transform = float4x4.identity;
            curve.GetPointAlongCurve(ref transform, distance, out point);
        }

        public static void GetPointAlongCurve(this IEnumerable<float3> curve, ref float4x4 transform, float distance, out float3 point)
        {
            var array = new NativeArray<float3>(curve.ToArray(), Allocator.Temp);
            array.GetPointAlongCurve(ref transform, distance, out point);
            array.Dispose();
        }

        public static void GetPointAlongCurve(this IEnumerable<float3> curve, float distance, out float3 point)
        {
            var transform = float4x4.identity;
            curve.GetPointAlongCurve(ref transform, distance, out point);
        }

        public static void GetPointsAlongCurve(this NativeArray<float3> curve, ref float4x4 transform, float desiredPointDistance, NativeArray<float3> result)
        {
            var currentTargetIndex = 1;
            var accumulatedLength = 0.0f;
            for (int i = 0; i < result.Length; i++)
            {
                curve.GetPointAlongCurve(ref transform, desiredPointDistance * i, out float3 point, ref currentTargetIndex, ref accumulatedLength);

                result[i] = point;
            }
        }

        public static void GetPointsAlongCurve(this NativeArray<float3> curve, float desiredPointDistance, NativeArray<float3> result)
        {
            var transform = float4x4.identity;
            curve.GetPointsAlongCurve(ref transform, desiredPointDistance, result);
        }

        public static void GetPointsAlongCurve(this IEnumerable<float3> curve, ref float4x4 transform, float desiredPointDistance, NativeArray<float3> result)
        {
            var array = new NativeArray<float3>(curve.ToArray(), Allocator.Temp);
            array.GetPointsAlongCurve(ref transform, desiredPointDistance, result);
            array.Dispose();
        }

        public static void GetPointsAlongCurve(this IEnumerable<float3> curve, float desiredPointDistance, NativeArray<float3> result)
        {
            var transform = float4x4.identity;
            curve.GetPointsAlongCurve(ref transform, desiredPointDistance, result);
        }

        // Closest point
        public static void GetClosestPoint(this NativeArray<float3> curve, float3 point, out int index, out float distance)
        {
            index = 0;
            var closestDistanceSq = math.distancesq(curve[0], point);
            for (int i = 1; i < curve.Length; i++)
            {
                var distSq = math.distancesq(curve[i], point);
                if (distSq < closestDistanceSq)
                {
                    index = i;
                    closestDistanceSq = distSq;
                }
            }
            distance = math.sqrt(closestDistanceSq);
        }

        public static void GetClosestPoint(this NativeArray<float3> curve, Ray ray, out int index, out float distance, out float distanceAlongRay)
        {
            index = 0;
            var origin = (float3)ray.origin;
            var dir = math.normalizesafe(ray.direction);
            var closestDistanceAlongRay = math.dot(curve[0] - origin, dir);
            var closestDistanceSq = math.distancesq(origin + closestDistanceAlongRay * dir, curve[0]);
            for (int i = 1; i < curve.Length; i++)
            {
                var position = curve[i];
                var rayDist = math.dot(position - origin, dir);
                var distSq = math.distancesq(origin + rayDist * dir, position);
                if (distSq < closestDistanceSq)
                {
                    index = i;
                    closestDistanceAlongRay = rayDist;
                    closestDistanceSq = distSq;
                }
            }
            distance = math.sqrt(closestDistanceSq);
            distanceAlongRay = closestDistanceAlongRay;
        }

        // Distance
        public static void KeepAtDistance(this ref float3 point, ref float3 otherPoint, float distance, float stiffness = 1.0f)
        {
            var delta = otherPoint - point;

            var currentDistance = math.length(delta);
            if (currentDistance > 0.0f)
            {
                delta /= currentDistance;
            }
            else
            {
                delta = float3.zero;
            }
            delta *= (currentDistance - distance) * stiffness;

            point += delta;
            otherPoint -= delta;
        }
    }
}
