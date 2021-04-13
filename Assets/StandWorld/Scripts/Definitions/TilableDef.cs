namespace StandWorld.Definitions
{
    [System.Serializable]
    public class TilableDef : Def
    {
        public Layer layer;

        public TilableType type = TilableType.Undefined;
        
        //Graphic data(size, texture, shader/material)
        public GraphicDef graphics;

        public GroundDef groundDef;

        public PlantDef plantDef;

        public float fertility = 0f;

        public float nutriments = 0f;

        public int maxStack = 0;

        public float pathCost = 1f;
        
        public bool blockPath = false;
        public bool blockBuilding = false;
        public bool blockPlant = false;
        public bool blockStackable = false;
        public bool supportRoof = false;
        public bool cuttable = false;
    }
}