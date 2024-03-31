using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Structure.Player.Stack
{
    public class PlayerStackController : MonoBehaviour
    {
        [SerializeField] private StackCell stackCellPrefab;
        [SerializeField] private Transform cellsParent;
        
        [Header("Init Options")]
        [SerializeField] private int maxColumnCount = 3;
        [SerializeField] private int columnCapacity = 4;
        [SerializeField] private float distanceBetweenStacks = 0.2f;
        [SerializeField] private float distanceBetweenColumns = 0.4f;

        private List<PlayerStack> _stackList;
        private List<StackCell> _cellList;

        public bool CanTakeStack() => _cellList.Any(p => p.CurrentStack == null);

        protected virtual void Start()
        {
            Init();
        }

        private void Init()
        {
            _stackList = new List<PlayerStack>();
            _cellList = new List<StackCell>();
            
            for (var i = 0; i < columnCapacity * maxColumnCount; i++)
            {
                var cell = Instantiate(stackCellPrefab, cellsParent);
                _cellList.Add(cell);
            }
        }

        public void AddStack(PlayerStack stack)
        {
            var cell = GetAvailableCell(stack.StackType);
            if (cell == null)
            {
                Debug.LogError("There is no available cell!");
                return;
            }

            var index = _cellList.IndexOf(cell);
            var slideStack = cell.CurrentStack;
            SetCellPosition(index);
            cell.CurrentStack = stack;
            stack.JumpToCell(cell);
            _stackList.Add(stack);
            
            if(slideStack != null)
                SlideStacksOnAdd(index, slideStack);
        }
        
        public PlayerStack RemoveStack(PlayerStackType type)
        {
            PlayerStack stack = null;
            StackCell cell = null;
            if (type == PlayerStackType.None)
            {
                cell = _cellList.LastOrDefault(p => p.CurrentStack != null);
                if (cell == null)
                    return null;

                stack = cell.CurrentStack;
                cell.CurrentStack = null;
                return stack;
            }
            
            cell = _cellList.LastOrDefault(p=>p.CurrentStack != null && p.CurrentStack.StackType == type);
            if (cell == null)
                return null;
            
            stack = cell.CurrentStack;
            cell.CurrentStack = null;
            SlideStackOnRemove(_cellList.IndexOf(cell) + 1);
            return stack;
        }

        private StackCell GetAvailableCell(PlayerStackType stackType)
        {
            StackCell cell = null;
            cell = _cellList.LastOrDefault(p=>p.CurrentStack != null && p.CurrentStack.StackType == stackType);
            if (cell != null)
            {
                var index = _cellList.IndexOf(cell);
                cell = _cellList[index + 1];
            }
            else
            {
                cell = _cellList.FirstOrDefault(p => p.CurrentStack == null);
            }

            return cell;
        }

        private void SlideStacksOnAdd(int index, PlayerStack stack)
        {
            PlayerStack nextStack = null;
            var slideStack = stack;
            for (var i = index; i < _cellList.Count - 1; i++)
            {
                if(slideStack == null)
                    return;

                slideStack = nextStack != null? nextStack: slideStack;
                nextStack = _cellList[i + 1].CurrentStack;

                var targetCell = _cellList[i + 1];
                targetCell.CurrentStack = slideStack;
                SetCellPosition(i + 1);
                slideStack.MoveToCell(targetCell);
                
                if (nextStack == null)
                    return;
            }
        }

        private void SlideStackOnRemove(int index)
        {
            for (var i = index; i < _cellList.Count; i++)
            {
                var slideStack = _cellList[i].CurrentStack;
                if(slideStack == null)
                    break;

                _cellList[i].CurrentStack = null;
                var targetCell = _cellList[i - 1];
                targetCell.CurrentStack = slideStack;
                slideStack.MoveToCell(targetCell);
                SetCellPosition(i);
            }
        }

        private void SetCellPosition(int index)
        {
            if (index == 0)
            {
                _cellList[index].transform.localPosition = Vector3.zero;
                return;
            }
            
            var oldColumnIndex = (index - 1) / columnCapacity;
            var cell = _cellList[index - 1];
            var newColumnIndex = index / columnCapacity;
            var isColumnChange = newColumnIndex != oldColumnIndex;
            
            _cellList[index].transform.localPosition = new Vector3(
                0,
                isColumnChange? 0: cell.transform.localPosition.y + cell.CurrentStack.GetHeight() + distanceBetweenStacks,
                -newColumnIndex * distanceBetweenColumns);
        }
    }
}