using UnityEngine;

namespace Core
{
    public static class LayerMaskUtils
    {
        public static bool Contains(this LayerMask mask, int layer) => 
            mask == (mask | (1 << layer));
    }
}