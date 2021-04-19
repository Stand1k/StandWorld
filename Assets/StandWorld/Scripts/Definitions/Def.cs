using StandWorld.Entities;
using UnityEngine.Serialization;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public class Def 
    {
        // Унікальний ідентифікатор
        public string uId;

        public override int GetHashCode()
        {
            return uId.GetHashCode();
        }
    }
}
