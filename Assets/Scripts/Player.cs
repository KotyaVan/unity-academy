using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour, IPlayer, IHitBox
    {
        [SerializeField] private int health = 1;
        public void RegisterPlayer()
        {
            GameManager manager = FindObjectOfType<GameManager>();

            if (manager.Player == null)
            {
                manager.Player = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            
        }

        private void Awake()
        {
            RegisterPlayer();
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
            print("Player died");
        }
    }
}