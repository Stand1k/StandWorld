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
        Building,
        Orders,
        FX,
        Plant,
        Stackable,
        Count // Трік який дає нам кількість наших леєрів
    }

    [System.Serializable]
    public enum TilableType : ushort
    {
        Undefined,
        Grass,
        Tree,
        Plant,
        Building,
        BuildingConnected,
        Recipe,
    }

    public static class LayerUtils
    {
        public static float Height(Layer layer)
        {
            return (int) layer * -1f;
        }
    }
}
