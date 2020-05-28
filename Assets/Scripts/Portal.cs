using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Transform exitPoint;
        [SerializeField] private Animator animator;

        private Player player;
        
        private void OnTriggerEnter2D(Collider other)
        {
            player = other.transform.GetComponent<Player>();
            
            if (other.transform.GetComponent<Player>())
            {
                animator.SetTrigger("Portal");
                StartCoroutine(PortalProcess());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            
        }

        private IEnumerator PortalProcess()
        {
            yield return new WaitForSeconds(1f);

            var playerAnimator = player.GetComponent<Animator>();
            playerAnimator.SetTrigger("Portal");

            yield return new WaitForSeconds(1f);
            player.transform.position = exitPoint.position;
            
            playerAnimator.SetTrigger("PortalExit");

        }
        
        
    }
}