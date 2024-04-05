using Structure.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Player.Workers
{
    public class WorkerMovementController : PlayerMovementBase
    {
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.isStopped = true;
        }

        public override void Move(Vector3 input, float delta)
        {
            _navMeshAgent.SetDestination(input);
            _navMeshAgent.isStopped = false;
        }
    }
}