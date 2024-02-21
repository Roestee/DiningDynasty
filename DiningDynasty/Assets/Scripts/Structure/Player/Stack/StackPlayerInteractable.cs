using System;
using Structure.Interact;

namespace Structure.Player.Stack
{
    public class StackPlayerInteractable : SingleInteractable<PlayerBase>
    {
        public event Action<PlayerBase, bool> OnStackInteract;
        
        protected override void OnTriggerInteract(PlayerBase player)
        {
            OnStackInteract?.Invoke(player, true);
        }

        protected override void OnTriggerInteractExit(PlayerBase player)
        {
            OnStackInteract?.Invoke(player, false);
        }
    }
}