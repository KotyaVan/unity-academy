using UnityEngine;

namespace DefaultNamespace
{
    public class HopInput : MonoBehaviour
    {
        private float  strafe;

        public float Strafe
        {
            get { return strafe; }
        }

        private float screenCenter;

        private void Start()
        {
            screenCenter = Screen.width * 0.5f;
        }

        private void Update()
        {
            if (!Input.GetMouseButton(0))
            {
               return; 
            }

            float mousePos = Input.mousePosition.x;

            if (mousePos < screenCenter)
            {
                strafe = (mousePos - screenCenter) / screenCenter;
            }
            else
            {
                strafe = 1 - mousePos / screenCenter;
                strafe *= -1f;
            }
        }
    }
}