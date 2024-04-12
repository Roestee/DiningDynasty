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
            var sitDuration = AnimationController.GetSitDuration();
            transform.DORotate(chair.AiTargetTf.localEulerAngles, sitDuration * 0.5f)
                .OnComplete(()=>
                {
                    AnimationController.SitToChair();
                    chair.MoveChairOnLocalX(true, sitDuration);
                });
        }
    }
}