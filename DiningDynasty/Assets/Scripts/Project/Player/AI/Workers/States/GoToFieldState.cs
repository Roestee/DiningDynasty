using System.Linq;
using Project.Fields;
using Structure.Player;

namespace Project.Player.AI.Workers.States
{
    public class GoToFieldState : WorkerStateBase
    {
        private FieldBase _targetField;
        private int _stackCount;
        private bool _isArrive;

        private const float StopDistance = 2f;
        private const int TotalStackCount = 4;

        public GoToFieldState(Worker worker) : base(worker)
        {
        }

        protected override void DoOnStateStart()
        {
            var availableField = Worker.CurrentArea.Fields.FirstOrDefault(p => p.IsThereAvailableStack());
            if (availableField == null)
                availableField = Worker.CurrentArea.Fields.First();

            _targetField = availableField;
            Worker.HandleMovement(_targetField.GetAiCollectPoint());
        }

        protected override void DoOnStateEnd()
        {
            _targetField.OnCollect -= OnCollect;
        }

        public override void DoState()
        {
            if (_isArrive)
                return;
            
            CheckIsArrive();
        }

        private void CheckIsArrive()
        {
            var distanceSqr = (_targetField.GetAiCollectPoint() - Worker.transform.position).sqrMagnitude;
            if (distanceSqr > StopDistance)
                return;

            _targetField.OnCollect += OnCollect;
            _isArrive = true;
            Worker.StopMovement();
        }
        
        private void OnCollect(PlayerBase player)
        {
            if (Worker != player)
                return;
            
            _stackCount += 1;
            if (_stackCount < TotalStackCount)
                return;
            
            Worker.StateMachine.SetState(new GoToMachineState(Worker));
        }
    }
}