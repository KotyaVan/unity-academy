using Unity.Entities;
using UnityEngine;

namespace ECS
{
    [GenerateAuthoringComponent]
    public struct EffectLifeComponent : IComponentData
    {
        public float LifeTime;
    }

}