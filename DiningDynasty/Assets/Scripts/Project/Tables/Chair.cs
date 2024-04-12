using DG.Tweening;
using Project.Player.AI.Customers;
using UnityEngine;

namespace Project.Tables
{
    public class Chair : MonoBehaviour
    {
        [SerializeField] private Transform aiTargetTf;
        
        [Header("Movement")]
        [SerializeField] private float moveForwardPosition = 0.5f;
        [SerializeField] private float moveBackwardPosition = 0.7f;
        
        public Customer CurrentCustomer { get; set; }
        public Transform AiTargetTf => aiTargetTf;

        public void MoveChairOnLocalX(bool isForward, float duration)
        {
            transform.DOLocalMoveX(isForward ? moveForwardPosition: moveBackwardPosition, duration);
        }
    }
}