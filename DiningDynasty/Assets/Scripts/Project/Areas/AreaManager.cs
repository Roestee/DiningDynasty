using System.Linq;
using Project.Tables;
using Structure.Singleton;

namespace Project.Areas
{
    public class AreaManager : SingletonMonoBehaviour<AreaManager>
    {
        private AreaBase[] _areas;
        
        protected override void Awake()
        {
            base.Awake();

            _areas = FindObjectsOfType<AreaBase>(true);
        }
        
        public CustomerTable GetAvailableTable()
        {
            var table = _areas.Where(p => p.IsUnlock)
                .Select(p => p.IsAnyTableAvailable())
                .FirstOrDefault();
            
            return table;
        }
    }
} 