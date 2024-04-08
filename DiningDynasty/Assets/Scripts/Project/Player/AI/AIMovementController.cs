using Structure.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Player.AI
{
    public class AIMovementController : PlayerMovementBase
    {
        private NavMeshAgent _navMeshAgent;

        protected virtual void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = movementSpeed;
            _navMeshAgent.isStopped = true;
        }

        public override void Move(Vector3 input, float delta)
        {
            _navMeshAgent.SetDestination(input);
            _navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = true;
        }
    }
}