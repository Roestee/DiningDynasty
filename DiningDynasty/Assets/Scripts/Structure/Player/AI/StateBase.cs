namespace Structure.Player.AI
{
    public abstract class StateBase
    {
        private bool _startCalledOnce;

        protected abstract void DoOnStateStart();
        protected abstract void DoOnStateEnd();

        public abstract void DoState();
        
        public void OnStateStart()
        {
            if (_startCalledOnce)
            {
                return;
            }
            
            _startCalledOnce = true;
            DoOnStateStart();
        }

        public void OnStateEnd()
        {
            DoOnStateEnd();
        }
    }
}