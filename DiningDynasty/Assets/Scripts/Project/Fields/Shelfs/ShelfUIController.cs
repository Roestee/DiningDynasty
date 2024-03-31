using TMPro;
using UnityEngine;

namespace Project.Fields.Shelfs
{
    public class ShelfUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countText;

        public void UpdateCountText(int count, int capacity) => countText.text = $"{count}/{capacity}";
        
    }
}