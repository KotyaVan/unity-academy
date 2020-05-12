using UnityEngine;

namespace DefaultNamespace
{
    public abstract class CharacterMovement : MonoBehaviour
    {
        public abstract void Move(Vector2 direction);
    }
}