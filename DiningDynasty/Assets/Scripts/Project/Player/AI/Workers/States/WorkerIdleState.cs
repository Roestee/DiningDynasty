namespace Project.Player.AI.Workers.States
{
    public class WorkerIdleState : WorkerStateBase
    {
        public WorkerIdleState(Worker worker) : base(worker)
        {
        }

        protected override void DoOnStateStart()
        {
            Worker.StopMovement();
        }

        protected override void DoOnStateEnd()
        {
            
        }

        public override void DoState()
        {
            
        }
    }
}