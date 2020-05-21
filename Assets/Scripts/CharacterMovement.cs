using UnityEngine;

namespace DefaultNamespace
{
    public abstract class CharacterMovement : MonoBehaviour
    {
        public bool IsFrizing;
        public abstract void Move(Vector2 direction);
        public abstract void Stop(float timer);
        public abstract void Jump(float force);
    }
}