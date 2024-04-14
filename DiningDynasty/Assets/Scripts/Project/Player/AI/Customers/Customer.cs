using System;
using DG.Tweening;
using Project.Tables;
using Structure.GenericObjectPooling.Abstracts;

namespace Project.Player.AI.Customers
{
    public class Customer : AIBase, IPoolMember
    {
        public event Action<IPoolMember> OnDeath;

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

        #region Pool

        public void OnCreate()
        {
            
        }

        public void OnEnterPool()
        {
            
        }

        public void OnExitPool()
        {
            
        }
        
        public void PushToPool()
        {
            OnDeath?.Invoke(this);
        }

        #endregion Pool
    }
}