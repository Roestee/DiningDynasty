using Structure.Player.Stack;
using UnityEngine;

namespace Structure.Player
{
    public class PlayerBase : MonoBehaviour
    {
        public PlayerStackController PlayerStackController { get; private set; }

        protected virtual void Awake()
        {
            PlayerStackController = GetComponent<PlayerStackController>();
        }

        public void OnInteractWithStack(PlayerStack stack)
        {
            PlayerStackController.AddStack(stack);
        }

        public PlayerStack GetStack(PlayerStackType takeStackType)
        {
            return PlayerStackController.RemoveStack(takeStackType);
        }
    }
}