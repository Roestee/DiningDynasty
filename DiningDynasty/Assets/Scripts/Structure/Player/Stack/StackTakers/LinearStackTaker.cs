using System.Collections.Generic;
using UnityEngine;

namespace Structure.Player.Stack.StackTakers
{
    public class LinearStackTaker : StackTakerBase
    {
        [Space]
        [SerializeField] private int horizontalCount = 3;
        [SerializeField] private int verticalCount = 2;
        [Space]
        [SerializeField] private float distanceBetweenStack = 1f;
        [Space]
        [SerializeField] private float horizontalOffset, verticalOffset;

        [Header("Gizmos")] 
        [SerializeField] private float cellRadius = 0.1f;

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
                        cell.transform.localPosition = new Vector3(
                            xPos + horizontalOffset, 
                            yPos, 
                            zPos + verticalOffset);
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            for (var i = 0; i < columnCount; i++)
            {
                var yPos = i * distanceBetweenColumn;
                for (var j = 0; j < verticalCount; j++)
                {
                    var zPos = (j - verticalCount * 0.5f) * distanceBetweenStack;
                    for (var k = 0; k < horizontalCount; k++)
                    {
                        var xPos = (k - horizontalCount * 0.5f) * distanceBetweenStack;
                        var currentPos = transform.position;
                        Gizmos.color = Color.red;
                        Gizmos.DrawSphere( new Vector3(
                            currentPos.x + xPos + horizontalOffset, 
                            currentPos.y + yPos, 
                            currentPos.z + zPos + verticalOffset), cellRadius);
                        Gizmos.color = Color.white;
                    }
                }
            }
        }
    }
}