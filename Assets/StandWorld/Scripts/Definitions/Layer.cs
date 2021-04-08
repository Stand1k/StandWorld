using UnityEngine;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public enum Layer : ushort
    {
        Undefined,
        Ground,
        Helpers,
        Mountain,
        Stackable,
        Plant,
        Count // Трік який дає нам кількість наших лейерів
    }

    [System.Serializable]
    public enum TilableType : ushort
    {
        Undefined,
        Grass,
        Tree,
        Plant
    }

    public static class LayerUtils
    {
        public static float Height(Layer layer)
        {
            return (int) layer * -1f;
        }
    }
}
