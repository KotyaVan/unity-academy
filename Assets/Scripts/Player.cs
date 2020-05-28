using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour, IPlayer, IHitBox
    {
        [SerializeField] private int health = 1;
        [SerializeField] public Animator animator;
        private PlayerWeapon[] weapons;
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

        private void Start()
        {
            weapons = GetComponents<PlayerWeapon>();
            InputManager.FireAction += OnAttack;
        }

        private void OnDestroy()
        {
            InputManager.FireAction -= OnAttack;
        }

        public int Health
        {
            get => health;
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

        private void OnAttack(string axis)
        {
            foreach (var weapon in weapons )
            {
                if (weapon.Axis == axis)
                {
                    weapon.SetDamage();
                    animator.SetTrigger("Attack");
                }
            }
        }
    }
}