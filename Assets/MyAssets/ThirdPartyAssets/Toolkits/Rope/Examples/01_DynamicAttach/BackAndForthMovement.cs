using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeToolkit.Example
{
    public class BackAndForthMovement : MonoBehaviour
    {
        public Vector3 amount = new Vector3(2.0f, 0.0f, 0.0f);

        protected Vector3 startPos;

        public void Start()
        {
            startPos = transform.position;
        }

        public void Update()
        {
            transform.position = startPos + amount * Mathf.Sin(Time.time);
        }
    }
}