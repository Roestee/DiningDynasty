using System;
using System.Collections.Generic;
using UnityEngine;

namespace Structure.MeshTypes
{
    public abstract class SimpleMeshTypeController<T, TType>: MeshTypeController<T, TType> where T : IMeshType<TType> where TType : IComparable
    {
        [SerializeField] private List<T> meshes;

        public override void GetMeshes()
        {
            MeshTypes = new List<T>(meshes);
        }
    }
}