using UnityEngine;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public enum Layer : ushort
    {
        Undefined,
        Ground,
        Grass,
        Count // Трік який дає нам кількість наших лейерів
    }

    public static class LayerUtils
    {
        public static float Height(Layer layer)
        {
            return (int) layer * -1f;
        }
    }
}
