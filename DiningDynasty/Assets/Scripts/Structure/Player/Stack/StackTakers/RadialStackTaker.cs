using System.Collections.Generic;
using UnityEngine;

namespace Structure.Player.Stack.StackTakers
{
    public class RadialStackTaker : StackTakerBase
    {
        [SerializeField] private Transform cellDesignHelper;
        [SerializeField][Range(4, 100)] private int stackCountInTheRow = 4;
        [SerializeField] private float distanceBetweenStack = 1f;
        [SerializeField] private bool isThereStackInTheMiddle;
        
        protected override void Init()
        {
            Cells = new List<StackCell>();
            var cellDesignHelperChild = cellDesignHelper.GetChild(0);
            var a = 2f * Mathf.PI / stackCountInTheRow;
            cellDesignHelperChild.localPosition = new Vector3(
                distanceBetweenStack * 0.5f * Mathf.Sin(a),
                0,
                distanceBetweenStack * 0.5f * Mathf.Cos(a));

            var rowCount = stackCountInTheRow - (isThereStackInTheMiddle ? 1 : 0);
            for (var i = 0; i < columnCount; i++)
            {
                for (var j = 0; j < rowCount; j++)
                {
                    var cell = Instantiate(cellPrefab, cellsParent);
                    Cells.Add(cell);
                    cellDesignHelper.localEulerAngles = (360f / rowCount) * j * Vector3.up;
                    
                    var cellDesignHelperChildPos = Vector3.zero;
                    if (stackCountInTheRow - 2 != j || isThereStackInTheMiddle)
                        cellDesignHelperChildPos = cellDesignHelperChild.position;
                    
                    cellDesignHelperChildPos.y = i * distanceBetweenColumn;
                    cell.transform.position = cellDesignHelperChildPos;
                }
            }
        }
    }
}