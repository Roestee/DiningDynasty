using DG.Tweening;
using Project.Tables;
using UnityEngine;

namespace Project.Player.AI.Customers
{
    public class Customer : AIBase
    {
        public CustomerStateMachine StateMachine { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            StateMachine = GetComponent<CustomerStateMachine>();
        }

        public void SitToChair(Chair chair)
        {
            var sequence = DOTween.Sequence();
            var rotation = chair.transform.eulerAngles;
            rotation.y += 90f;
            var sitDuration = AnimationController.GetSitDuration();
            var chairDirection = chair.transform.localPosition.x > 0 ? 1: -1;
            sequence.Append(chair.transform.DOLocalMoveX(0.7f * chairDirection, sitDuration * 0.5f));
            sequence.Join(transform.DORotate(rotation.y * Vector3.up, sitDuration * 0.5f));
            sequence.Append(chair.transform.DOLocalMoveX(0.5f * chairDirection, sitDuration).OnStart(()=> AnimationController.SitToChair()));
        }
    }
}