namespace StandWorld.Definitions
{
    [System.Serializable]
    public class TilableDef : Def
    {
        public string name;
        public Layer layer;
        public TilableType type = TilableType.Undefined;
        
        public GraphicDef graphics; //Graphic data(size, texture, shader/material)
        public GroundDef groundDef;
        public PlantDef plantDef;
        public BuildingDef buildingDef;
        public RecipeDef recipeDef;

        public float fertility;
        public float nutriments;
        public int maxStack;
        public float pathCost = 1f;
        
        public bool blockPath;
        public bool blockBuilding;
        public bool blockPlant;
        public bool blockStackable;
        public bool supportRoof;
        public bool cuttable;
    }
}