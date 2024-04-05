﻿using Structure.Player.AI;

namespace Project.Player.Workers.States
{
    public abstract class WorkerStateBase : StateBase
    {
        protected readonly Worker Worker;

        protected WorkerStateBase(Worker worker)
        {
            Worker = worker;
        }
    }
}