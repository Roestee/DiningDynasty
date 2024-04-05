using Project.Fields;
using Project.Fields.Shelfs;
using Project.Machines;
using UnityEngine;

namespace Project.Areas
{
    public class AreaBase : MonoBehaviour
    {
        private MachineBase[] _machines;
        private FieldBase[] _fields;
        private Shelf[] _shelves;

        public MachineBase[] Machines => _machines;
        public FieldBase[] Fields => _fields;
        public Shelf[] Shelves => _shelves;

        private void Awake()
        {
            _machines = GetComponentsInChildren<MachineBase>(true);
            _fields = GetComponentsInChildren<FieldBase>(true);
            _shelves = GetComponentsInChildren<Shelf>(true);
        }
    }
}