using Structure.GenericObjectPooling;
using Structure.Player.Stack;
using UnityEngine;

namespace Project.Machines
{
    public class ProductSpawner : MonoBehaviour
    {
        public PooledPlayerStack SpawnProduct(PlayerStackType stackType)
        {
            var pool = PoolsManager.Instance.GetStackPool(stackType);
            var product = pool.Pull();
            return product;
        }
    }
}