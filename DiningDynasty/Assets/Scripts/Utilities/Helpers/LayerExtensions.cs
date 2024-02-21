using UnityEngine;

namespace Utilities.Helpers
{
    public static class LayerExtensions
    {
        public static bool LayerMaskLayerCompare(this int layer, LayerMask layerMask)
        {
            return ((1 << layer) & layerMask) != 0;
        }
    }
}