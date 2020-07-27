using Unity.Entities;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Transforms;
using UnityEditorInternal;

namespace ECS
{
    public class EffectMovementSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle jobHandle)
        {
            var deltaTime = World.Time.DeltaTime;

            // ...
            // Entities.WithBurst().WithStructuralChanges().ForEach(ref Entity entity, ref EffectLifeComponent component) =>
            // {
            //     
            // }).Run();
            return default;
        }
    }
}