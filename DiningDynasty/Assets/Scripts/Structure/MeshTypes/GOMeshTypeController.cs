using System;
using System.Linq;

namespace Structure.MeshTypes
{
    public abstract class GOMeshTypeController<T, TType>: MeshTypeController<T, TType>  
        where T : IMeshType<TType> where TType : IComparable
    {
        public sealed override void GetMeshes()
        {
            MeshTypes = GetComponentsInChildren<T>(true).ToList();
        }
    }
}