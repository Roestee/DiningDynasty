using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Helpers;

namespace Structure.MeshTypes
{
    public abstract class MeshTypeController<T, TType> : MonoBehaviour where T : IMeshType<TType> where TType: IComparable
    {
        protected List<T> MeshTypes = new List<T>();
        
        public T CurrentMesh { get; private set; }

        public abstract void GetMeshes();
        public abstract void SetMeshAttribute();

        protected virtual void Awake()
        {
            GetMeshes();
        }
        
        public void ChooseRandomMesh()
        {
            SetMesh( MeshTypes.Rand().GetSpecialType());
        }
        
        public void SetNextMesh()
        {
            var indexOf = MeshTypes.IndexOf(CurrentMesh);
            indexOf++;
            if (indexOf >= MeshTypes.Count)
                indexOf = 0;
            
            SetMesh(MeshTypes[indexOf].GetSpecialType());
        }
        
        public void SetMesh(TType type)
        {
            if (MeshTypes == null || MeshTypes.Count == 0)
            {
                Debug.LogError("Mesh Types does not have any member!");
                return;
            }
            
            MeshTypes.ForEach(t =>
            {
                var isCorrect = Equals(type, t.GetSpecialType());
                t.OnMeshSetActive(isCorrect);
                if (isCorrect)
                {
                    CurrentMesh = t;
                    SetMeshAttribute();
                }
            });
        }
    }
}