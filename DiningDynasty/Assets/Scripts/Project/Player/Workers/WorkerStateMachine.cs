using Project.Player.Workers.States;
using Structure.Player.AI;

namespace Project.Player.Workers
{
    public class WorkerStateMachine : StateMachineBase
    {
        private Worker _worker;

        private void Awake()
        {
            _worker = GetComponent<Worker>();
        }

        public override void SetDefaultState()
        {
            SetState(new LookForJobState(_worker));
        }

        public override void SetStartState()
        {
            
        }
    }
}