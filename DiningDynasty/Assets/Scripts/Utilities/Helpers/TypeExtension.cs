using System;

namespace Utilities.Helpers
{
    public static class TypeExtension
    {
        public static bool CheckIsParentWithName(this Type thisType, string parent)
        {
            if (thisType.BaseType.Name == "MonoBehaviour")
                return false;
            
            return thisType.BaseType.Name == parent || CheckIsParentWithName(thisType.BaseType, parent);
        }
    }
}