using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : CharacterMovement
    {

        [SerializeField] private float maxSpeed = 10f;
        [SerializeField] private Transform graphics;

        private Rigidbody2D rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 direction = new Vector2(InputManager.HorizontalAxis, 0f);
            Move(direction);
        }

        private void Update()
        {
            if (Mathf.Abs(rigidbody.velocity.x) < 0.01f)
            {
                return;
            }

            float xAngle = rigidbody.velocity.x > 0 ? 0 : 180;
            graphics.localEulerAngles = new Vector3(0f, xAngle, 0f);
        }

        public override void Move(Vector2 direction)
        {
            Vector2 velocity = rigidbody.velocity;
            velocity.x = direction.x * maxSpeed;
            rigidbody.velocity = velocity;
        }

        public override void Stop(float timer)
        {
            throw new System.NotImplementedException();
        }

        public override void Jumb(float force)
        {
            throw new System.NotImplementedException();
        }
    }
}