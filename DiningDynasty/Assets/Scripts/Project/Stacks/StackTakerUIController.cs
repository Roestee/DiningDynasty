using Structure.Player.Stack.StackTakers;
using TMPro;
using UnityEngine;

namespace Project.Stacks
{
    public class StackTakerUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stackCountText;

        private StackTakerBase _stackTaker;

        private void Awake()
        {
            _stackTaker = GetComponentInChildren<StackTakerBase>(true);
            if (_stackTaker == null)
                return;

            _stackTaker.OnCountChange += OnCountChange;
            OnCountChange(0, _stackTaker.GetCapacity());
        }

        private void OnCountChange(int count, int capacity)
        {
            UpdateStackCountText(count, capacity);
        }

        private void UpdateStackCountText(int count, int capacity)
        {
            stackCountText.text = $"{count}/{capacity}";
        }

        private void OnDestroy()
        {
            if (_stackTaker == null)
                return;
            
            _stackTaker.OnCountChange -= OnCountChange;
        }
    }
}