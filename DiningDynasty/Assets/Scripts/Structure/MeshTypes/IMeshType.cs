using System;

namespace Structure.MeshTypes
{
    public interface IMeshType<out TType> where TType: IComparable
    {
        TType GetSpecialType();
        void OnMeshSetActive(bool active = true);
    }
}