using System.Linq;
using Project.Machines;

namespace Project.Player.AI.Workers.States
{
    public class GoToMachineState : WorkerStateBase
    {
        private MachineBase _target;
        private bool _isArrive;
        
        private const float StopDistance = 0.1f;

        public GoToMachineState(Worker worker) : base(worker)
        {
        }

        protected override void DoOnStateStart()
        {
            _target = Worker.CurrentArea.Machines.First(p => p.CanTakeStack());
            Worker.HandleMovement(_target.GetAiCollectPoint());
        }

        protected override void DoOnStateEnd()
        {
            
        }

        public override void DoState()
        {
            if (_isArrive)
                return;
            
            CheckIsArrive();
        }

        private void CheckIsArrive()
        {
            var distanceSqr = (_target.GetAiCollectPoint() - Worker.transform.position).sqrMagnitude;
            if(distanceSqr > StopDistance)
                return;

            _isArrive = true;
            Worker.StopMovement();
        }
    }
}