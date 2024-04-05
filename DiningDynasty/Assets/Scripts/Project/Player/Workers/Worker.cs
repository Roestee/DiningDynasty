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
        public WorkerStateMachine WorkerStateMachine { get; private set; }
        public WorkerMovementController WorkerMovementController { get; private set; }
        public AreaBase CurrentArea { get; private set; }
        public IEnumerable<JobEnum> JobOrder => jobOrder;

        protected override void Awake()
        {
            base.Awake();

            NavMeshAgent = GetComponent<NavMeshAgent>();
            WorkerStateMachine = GetComponent<WorkerStateMachine>();
            WorkerMovementController = GetComponent<WorkerMovementController>();
            
            CheckForArea();
        }

        private void CheckForArea()
        {
            var results = Physics.OverlapSphere(transform.position, 0.5f, areaMask);
            if(results.Length == 0)
                return;
            
            CurrentArea = results.First().GetComponent<AreaBase>();
        }
    }
}