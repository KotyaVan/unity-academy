using UnityEngine;

namespace DefaultNamespace
{
    public class StaticObject : MonoBehaviour, IHitBox
    {
        [SerializeField] private int health = 1;
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