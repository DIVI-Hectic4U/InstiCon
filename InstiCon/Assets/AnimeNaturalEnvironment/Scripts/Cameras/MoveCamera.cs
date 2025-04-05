using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kikingao
{
    public class MoveCamera : MonoBehaviour
    {
        public float sensitivity = 0.03f;
        public Transform destination;
        public float time = 5f;
        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, destination.position, time * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, destination.rotation, time * Time.deltaTime);
        }
    }
}