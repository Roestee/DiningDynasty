using DG.Tweening;
using UnityEngine;

namespace Project.Coins
{
    public class Coin : MonoBehaviour, IThrowable
    {
        [SerializeField] private float jumpPower = 5f;
        [SerializeField] private float jumpDuration = 0.8f;
        
        public void Throw(Transform targetTf)
        {
            transform.DOJump(targetTf.position, jumpPower, 1, jumpDuration);
        }
    }
}