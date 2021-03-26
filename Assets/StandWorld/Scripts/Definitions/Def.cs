using StandWorld.Entities;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public class Def 
    {
        // Унікальний ідентифікатор
        public string uID;

        public override int GetHashCode()
        {
            return uID.GetHashCode();
        }
    }
}
