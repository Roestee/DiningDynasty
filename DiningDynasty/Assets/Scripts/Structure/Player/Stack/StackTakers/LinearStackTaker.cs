using System.Collections.Generic;
using UnityEngine;

namespace Structure.Player.Stack.StackTakers
{
    public class LinearStackTaker : StackTakerBase
    {
        [SerializeField] private int horizontalCount = 3;
        [SerializeField] private int verticalCount = 2;
        [SerializeField] private float distanceBetweenStack = 1f;

        protected override void Init()
        {
            Cells = new List<StackCell>();
            
            for (var i = 0; i < columnCount; i++)
            {
                var yPos = i * distanceBetweenColumn;
                for (var j = 0; j < verticalCount; j++)
                {
                    var zPos = (j - verticalCount * 0.5f) * distanceBetweenStack;
                    for (var k = 0; k < horizontalCount; k++)
                    {
                        var xPos = (k - horizontalCount * 0.5f) * distanceBetweenStack;
                        var cell = Instantiate(cellPrefab, cellsParent);
                        Cells.Add(cell);
                        cell.transform.localPosition = new Vector3(xPos, yPos, zPos);
                    }
                }
            }
        }
    }
}