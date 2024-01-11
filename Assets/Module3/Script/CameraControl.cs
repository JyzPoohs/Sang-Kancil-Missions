using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CameraController3 : MonoBehaviour
    {
        public Transform target;
        public Transform farBackground, middleBackground;
        private Vector2 lastPosition;

        void Start()
        {
            lastPosition = target.position;
        }

        void Update()
        {
            Vector2 currentPosition = target.position;
            Vector2 newPosition = currentPosition - lastPosition;

            transform.position = new Vector3(transform.position.x + newPosition.x, transform.position.y + newPosition.y, transform.position.z);
            farBackground.position += new Vector3(newPosition.x, newPosition.y, 0f);
            middleBackground.position += new Vector3(newPosition.x * 0.5f, newPosition.y * 0.5f, 0f);

            lastPosition = currentPosition;
        }
    }
}
