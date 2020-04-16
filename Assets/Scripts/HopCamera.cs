using System;
using UnityEngine;

namespace DefaultNamespace
{
    
    public class HopCamera : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;

        [SerializeField] private float m_Distance = 2f;
        [SerializeField] private float m_Height = 2f;

        private void Update()
        {
            float z = Mathf.Lerp(transform.position.z, m_Target.position.z - m_Distance, Time.deltaTime * 1f);
            Vector3 pos = new Vector3(0f, m_Height, z);

            transform.position = pos;
        }
    }
}