namespace StandWorld.Definitions
{
    [System.Serializable]
    public class PlantDef : Def
    {
        public float probability = 0f;
        public float minFertility = 0f;
        public int states = 5;
        public float lifetime = 2f; // В днях
    }
}