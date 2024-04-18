using Structure.Player.Stack;
using Structure.Pool_Spawner.Spawner.SpawnerWithPool;

namespace Project.Stacks
{
    public class StackSpawner : SpawnerMonoWithPoolWithType<PooledPlayerStack, PlayerStackType>
    {
        public static StackSpawner Instance { get; protected set; }
        
        protected override void InternalAwake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this);
                return;
            } 
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        protected override void InternalStart()
        {
            
        }

        protected override void OnSpawnDone(PooledPlayerStack stack)
        {
        }

        public void Push(PooledPlayerStack member)
        {
            SetSpawnType(member.StackType);
            GetPool().Push(member);
        }

        public PooledPlayerStack SpawnStack(PlayerStackType stackType)
        {
            SetSpawnType(stackType);
            return Spawn();
        }
    }
}