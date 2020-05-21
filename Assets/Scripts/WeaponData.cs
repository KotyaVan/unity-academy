using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Objects/Weapon objects", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public string weaponName = "Weapon Name";
        public int damage = 1;
        public float range = 1f;
        public float fireRate = 1f;
        public GameObject bullet;
        public float bulletSpeed;
    }
}