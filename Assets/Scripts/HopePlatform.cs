using UnityEngine;

namespace DefaultNamespace
{
    public class HopePlatform : MonoBehaviour
    {
        [SerializeField] private GameObject m_BasePlatform;
        [SerializeField] private GameObject m_LargePlatform;

        private void Start()
        {
            m_BasePlatform.SetActive(true);
            m_BasePlatform.SetActive(false);
        }
        
        public void SetupDone()
        {
            m_BasePlatform.SetActive(false);
            m_BasePlatform.SetActive(true);
        }
    }
}