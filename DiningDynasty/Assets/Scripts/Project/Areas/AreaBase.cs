using System.Linq;
using Project.Fields;
using Project.Fields.Shelfs;
using Project.Machines;
using Project.Tables;
using UnityEngine;

namespace Project.Areas
{
    public class AreaBase : MonoBehaviour
    {
        private MachineBase[] _machines;
        private FieldBase[] _fields;
        private Shelf[] _shelves;
        private CustomerTable[] _tables;

        public MachineBase[] Machines => _machines;
        public FieldBase[] Fields => _fields;
        public Shelf[] Shelves => _shelves;
        public CustomerTable[] Tables => _tables;
        
        public bool IsUnlock { get; set; }

        private void Awake()
        {
            _machines = GetComponentsInChildren<MachineBase>(true);
            _fields = GetComponentsInChildren<FieldBase>(true);
            _shelves = GetComponentsInChildren<Shelf>(true);
            _tables = GetComponentsInChildren<CustomerTable>(true);
        }

        public CustomerTable IsAnyTableAvailable() => _tables.FirstOrDefault(p => p.IsThereAvailableChair());
    }
}