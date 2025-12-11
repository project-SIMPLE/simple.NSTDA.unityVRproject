using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeToolkit.Example
{
    public class DynamicAttach : MonoBehaviour
    {
        public Material ropeMaterial;

        public Vector3 attachPoint;
        public Transform target;
        public Vector3 targetAttachPoint;

        protected GameObject ropeObject;

        public void Detach()
        {
            if (ropeObject)
            {
                Destroy(ropeObject);
            }
            ropeObject = null;
        }

        public void Attach()
        {
            Detach();

            ropeObject = new GameObject();
            ropeObject.name = "Rope";

            var start = transform.TransformPoint(attachPoint);
            var end = target.TransformPoint(targetAttachPoint);

            var rope = ropeObject.AddComponent<Rope>();
            rope.material = ropeMaterial;
            rope.spawnPoints.Add(ropeObject.transform.InverseTransformPoint(start));
            rope.spawnPoints.Add(ropeObject.transform.InverseTransformPoint(end));

            var conn0 = ropeObject.AddComponent<RopeConnection>();
            conn0.type = RopeConnectionType.PinRopeToTransform;
            conn0.ropeLocation = 0.0f;
            conn0.transformSettings.transform = transform;
            conn0.localConnectionPoint = attachPoint;

            var conn1 = ropeObject.AddComponent<RopeConnection>();
            conn1.type = RopeConnectionType.PinRopeToTransform;
            conn1.ropeLocation = 1.0f;
            conn1.transformSettings.transform = target;
            conn1.localConnectionPoint = targetAttachPoint;
        }

        public void OnGUI()
        {
            if (GUI.Button(new Rect(16, 16, 100, 32), "Attach"))
            {
                Attach();
            }
            if (GUI.Button(new Rect(16, 64, 100, 32), "Detach"))
            {
                Detach();
            }
        }
    }
}
