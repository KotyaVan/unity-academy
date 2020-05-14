using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class StaticObject : MonoBehaviour, IHitBox
    {
        [SerializeField] private LevelObjectData objectData;
        private int health = 1;
        private Rigidbody2D rigidbody;
        private void Start()
        {
            health = objectData.Health;
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.bodyType = objectData.isStatic ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
        }

        #if UNITY_EDITOR
        [ContextMenu("Rename this")]
        private void Rename()
        {
            if (objectData != null)
            {
                gameObject.name = objectData.name;
            }
        }
        
        [ContextMenu("Move Right")]
        private void MoveRight()
        {
            Move(Vector2.right);
        }
        [ContextMenu("Move Left")]
        private void MoveLeft()
        {
            Move(Vector2.left);
        }
        [ContextMenu("Create And Up")]
        private void CreateAndMove()
        {
            var next = Instantiate(this);
            Move(Vector2.left);
        }

        [ContextMenu("Move Copy Up")]
        private void CopyAndMoveUp()
        {
            Instantiate(this);
            Move(Vector2.up);
        }
        
        private void Move(Vector2 direction)
        {

            var collider = GetComponent<Collider2D>();
            var size = collider.bounds.size.x;
            transform.Translate(direction * size);
        }
        #endif
        public int Health
        {
            get => Health;
            private set
            {
                health = value;
                if (health <= 0f)
                {
                    Die();
                }
            }
        }
        public void Hit(int damage)
        {
            Health -= damage;
        }

        public void Die()
        {
            print("Enemy died");
        }
    }
}