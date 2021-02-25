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

}
