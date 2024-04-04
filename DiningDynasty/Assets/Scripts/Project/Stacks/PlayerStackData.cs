using Structure.Player.Stack;
using UnityEngine;

namespace Project.Stacks
{
    [CreateAssetMenu(fileName = "PlayerStackData", menuName = "Stacks/PlayerStackData", order = 0)]
    public class PlayerStackData : ScriptableObject
    {
        [SerializeField] private PlayerStackType stackType;
        [SerializeField] private Sprite icon;
        
        public PlayerStackType StackType => stackType;
        public Sprite Icon => icon;
    }
}