using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class InputManager : MonoBehaviour
    {
        public static float HorizontalAxis;

        private void Start()
        {
            HorizontalAxis = 0;
        }

        private void Update()
        {
            HorizontalAxis = Input.GetAxis("Horizontal");
        }
    }
}