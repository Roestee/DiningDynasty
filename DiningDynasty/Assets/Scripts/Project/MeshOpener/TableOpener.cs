using UnityEngine;

namespace Project.MeshOpener
{
    public class TableOpener : MeshOpenerBase<TableType>
    {
        [SerializeField] private TableType tableType;
        
        protected override TableType GetSpecialType() => tableType;
    }
}