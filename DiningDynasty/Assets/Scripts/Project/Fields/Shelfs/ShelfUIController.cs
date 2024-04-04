using Project.Stacks;
using Structure.Player.Stack;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Fields.Shelfs
{
    public class ShelfUIController : MonoBehaviour
    {
        [SerializeField] private PlayerStackDataContainer stackDataContainer;
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Image identityImage;

        public void UpdateCountText(int count, int capacity) => countText.text = $"{count}/{capacity}";

        public void SetIdentityImage(PlayerStackType type)
        {
            var data = stackDataContainer.GetData(type);
            if (data == null)
            {
                Debug.LogError($"{type} type data not found!");
                return;
            }

            identityImage.sprite = data.Icon;
        }
    }
}