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
}
