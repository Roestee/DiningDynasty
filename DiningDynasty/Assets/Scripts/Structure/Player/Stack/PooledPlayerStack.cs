using Structure.Pool_Spawner.Interfaces;

namespace Structure.Player.Stack
{
    public class PooledPlayerStack : PlayerStack, IPoolMemberWithType<PlayerStackType>
    {

        #region Pool

        public PlayerStackType GetTypeForPool() => stackType;

        public void OnCreate()
        {
            
        }

        public void OnEnterPool()
        {
            
        }

        public void OnExitPool()
        {
            
        }
        
        #endregion
    }
}