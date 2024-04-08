using System.Collections.Generic;
using System.Linq;
using Project.Areas;
using Project.Player.AI.Workers.States;
using UnityEngine;

namespace Project.Player.AI.Workers
{
    public class Worker : AIBase
    {
        [SerializeField] private LayerMask areaMask;
        [SerializeField] private JobEnum[] jobOrder;
        
        public WorkerStateMachine StateMachine { get; private set; }
        public AreaBase CurrentArea { get; private set; }
        public IEnumerable<JobEnum> JobOrder => jobOrder;

        protected override void Awake()
        {
            base.Awake();

            StateMachine = GetComponent<WorkerStateMachine>();
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