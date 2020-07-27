using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECS
{
    public struct EffectMovementComponent : IComponentData
    {
        public float3 Direction;
        public float Speed;
    }
}