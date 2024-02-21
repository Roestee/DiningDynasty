using Structure.GenericObjectPooling;
using UnityEngine;

namespace Project.MeshOpener
{
    public class CoinThrower : MonoBehaviour
    {
        public void Throw(Transform targetTf)
        {
            var coin = PoolsManager.Instance.CoinPool.Pull();
            coin.transform.position = transform.position;
            coin.Throw(targetTf);
        }
    }
}