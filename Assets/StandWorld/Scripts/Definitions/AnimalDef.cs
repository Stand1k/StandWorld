using System.Collections.Generic;
using StandWorld.Entities;
using UnityEngine;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public class LivingDef : Def
    {
        public GraphicDef graphics;
    }
    
    [System.Serializable]
    public class AnimalDef : LivingDef
    {
    }
}
