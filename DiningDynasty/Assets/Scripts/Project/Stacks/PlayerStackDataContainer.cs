using System.Linq;
using Structure.Player.Stack;
using UnityEngine;

namespace Project.Stacks
{
    [CreateAssetMenu(fileName = "PlayerStackDataContainer", menuName = "Stacks/PlayerStackDataContainer", order = 0)]
    public class PlayerStackDataContainer : ScriptableObject
    {
        [SerializeField] private PlayerStackData[] datas;

        public PlayerStackData GetData(PlayerStackType type)
        {
            return datas.FirstOrDefault(p => p.StackType == type);
        }
    }
}