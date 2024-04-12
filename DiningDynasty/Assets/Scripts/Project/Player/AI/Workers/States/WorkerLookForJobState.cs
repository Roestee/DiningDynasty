namespace Project.Player.AI.Workers.States
{
    public enum JobEnum
    {
        Farm,
        Delivery,
    }
    
    public class WorkerLookForJobState : WorkerStateBase
    {
        public WorkerLookForJobState(Worker worker) : base(worker)
        {
            
        }
        
        protected override void DoOnStateStart()
        {
            
        }

        protected override void DoOnStateEnd()
        {
            
        }

        public override void DoState()
        {
            CheckForJob();
        }

        private void CheckForJob()
        {
            foreach (var job in Worker.JobOrder)
            {
                switch (job)
                {
                    case JobEnum.Farm:
                        Worker.StateMachine.SetState(new GoToFieldState(Worker));
                        return;
                    case JobEnum.Delivery:
                        return;
                    default:
                        return;
                }
            }
        }
    }
}