using Structure.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Player.AI
{
    public class AIBase : PlayerBase
    {
        public NavMeshAgent NavMeshAgent { get; private set; }
        public AIMovementController MovementController { get; private set; }
        public PlayerAnimationControllerBase AnimationController { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            NavMeshAgent = GetComponent<NavMeshAgent>();
            MovementController = GetComponent<AIMovementController>();
            AnimationController = GetComponent<PlayerAnimationControllerBase>();
        }
        
                
        public virtual void HandleMovement(Vector3 position)
        {
            MovementController.Move(position, Time.deltaTime);
            AnimationController.SetMovementSpeed();
        }

        public virtual void StopMovement()
        {
            MovementController.Stop();
            AnimationController.SetMovementSpeed(0f);
        }
    }
}