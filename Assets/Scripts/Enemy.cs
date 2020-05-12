using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Enemy : MonoBehaviour, IEnemy, IHitBox
    {        
        [SerializeField] private int health = 1;
        public void RegisterEnemy()
        {
            GameManager manager = FindObjectOfType<GameManager>();
            manager.Enemies.Add(this);
        }

        private void Awake()
        {
            RegisterEnemy();
        }
        
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