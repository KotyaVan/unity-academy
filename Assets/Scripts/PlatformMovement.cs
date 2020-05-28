using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlatformMovement : MonoBehaviour
    {
        [SerializeField] private bool moved;
        [SerializeField] private float distance = 6f;
        [SerializeField] private float speed = 0.5f;

        private Vector3 startPosition;
        private Vector3 targetPosition;

        private void Start()
        {
            if (moved)
            {
                startPosition = transform.position;
                targetPosition = transform.position;
                targetPosition.x += distance;

                StartCoroutine(MovementProcess());
            }
        }

        private IEnumerator MovementProcess()
        {
            var k = 0f;
            var dir = 1f;

            while (true)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, k);

                k += Time.deltaTime * dir * speed;

                if (k > 1f)
                {
                    dir = -1;
                    k = 0;
                    yield return new WaitForSeconds(1f);
                }

                if (k < 0)
                {
                    dir = 1;
                    k = 0;
                }

                yield return null;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            bool isMovementObject = other.transform.GetComponent<CharacterMovement>();
            if (isMovementObject)
            {
                other.transform.parent = transform;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.transform.parent == transform)
            {
                other.transform.parent = null;
            }
        }
    }
    
}