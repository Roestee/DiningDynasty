using System.Linq;

namespace Project.Player.Workers.States
{
    public class GoToFieldState : WorkerStateBase
    {
        
        public GoToFieldState(Worker worker) : base(worker)
        {
        }

        protected override void DoOnStateStart()
        {
            var availableField = Worker.CurrentArea.Fields.FirstOrDefault(p => p.IsThereAvailableStack());
            if (availableField == null)
                availableField = Worker.CurrentArea.Fields.First();

            Worker.NavMeshAgent.SetDestination(availableField.GetAiCollectPoint());
            Worker.NavMeshAgent.isStopped = false;
        }

        protected override void DoOnStateEnd()
        {
            
        }

        public override void DoState()
        {
            
        }
    }
}