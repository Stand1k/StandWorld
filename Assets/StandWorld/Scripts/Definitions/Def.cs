using StandWorld.Entities;
using UnityEngine.Serialization;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public class Def 
    {
        // Унікальний ідентифікатор
        [FormerlySerializedAs("uID")] public string uId;

        public override int GetHashCode()
        {
            return uId.GetHashCode();
        }
    }
}
