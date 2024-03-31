using Structure.Player.Stack.StackTakers;
using UnityEngine;

namespace Project.Fields.Shelfs
{
    public class Shelf : MonoBehaviour
    {
        private ShelfUIController _uiController;
        private StackTakerBase _stackTaker;

        private void Start()
        {
            _uiController = GetComponent<ShelfUIController>();
            _stackTaker = GetComponentInChildren<StackTakerBase>(true);
            _stackTaker.OnCountChange += OnStackCountChange;

            OnStackCountChange(_stackTaker.GetStackCount(), _stackTaker.GetCapacity());
        }

        private void OnStackCountChange(int count, int capacity)
        {
            _uiController.UpdateCountText(count, capacity);
        }

        private void OnDestroy()
        {
            _stackTaker.OnCountChange -= OnStackCountChange;
        }
    }
}