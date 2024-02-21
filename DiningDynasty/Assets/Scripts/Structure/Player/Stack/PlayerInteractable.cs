using System;
using Structure.Interact;

namespace Structure.Player.Stack
{
    public class PlayerInteractable : SingleInteractable<PlayerBase>
    {
        public event Action<PlayerBase, bool> OnPlayerInteract;
        
        protected override void OnTriggerInteract(PlayerBase player)
        {
            OnPlayerInteract?.Invoke(player, true);
        }

        protected override void OnTriggerInteractExit(PlayerBase player)
        {
            OnPlayerInteract?.Invoke(player, false);
        }
    }
}