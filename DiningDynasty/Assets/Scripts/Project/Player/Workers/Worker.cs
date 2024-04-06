using System.Collections.Generic;
using System.Linq;
using Project.Areas;
using Project.Player.Workers.States;
using Structure.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Player.Workers
{
    public class Worker : PlayerBase
    {
        [SerializeField] private LayerMask areaMask;
        [SerializeField] private JobEnum[] jobOrder;
        
        public NavMeshAgent NavMeshAgent { get; private set; }
        public WorkerStateMachine StateMachine { get; private set; }
        public WorkerMovementController MovementController { get; private set; }
        public WorkerAnimationController AnimationController { get; private set; }
        public AreaBase CurrentArea { get; private set; }
        public IEnumerable<JobEnum> JobOrder => jobOrder;

        protected override void Awake()
        {
            base.Awake();

            NavMeshAgent = GetComponent<NavMeshAgent>();
            StateMachine = GetComponent<WorkerStateMachine>();
            MovementController = GetComponent<WorkerMovementController>();
            AnimationController = GetComponent<WorkerAnimationController>();
            
            CheckForArea();
        }

        private void CheckForArea()
        {
            var results = Physics.OverlapSphere(transform.position, 0.5f, areaMask);
            if(results.Length == 0)
                return;
            
            CurrentArea = results.First().GetComponent<AreaBase>();
        }

        public void HandleMovement(Vector3 position)
        {
            MovementController.Move(position, Time.deltaTime);
            AnimationController.SetMovementSpeed();
        }

        public void StopMovement()
        {
            MovementController.Stop();
            AnimationController.SetMovementSpeed(0f);
        }
    }
}