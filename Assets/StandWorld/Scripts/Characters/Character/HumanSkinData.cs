using System;
using StandWorld.Definitions;
using StandWorld.Game;

namespace StandWorld.Characters
{
    [Serializable]
    public struct HumanSkinData
    {
        public int hairID;
        public int eyeID;
        public int bodyID;
        public int headID;
        public int clothesID;
        public int hairColorID;
        public int bodyColorID;
        public int clothesColorID;

        public HumanSkinData(bool randomize = false)
        {
            if (!randomize)
            {
                eyeID = 0;
                bodyID = 0;
                hairColorID = 0;
                bodyColorID = 0;
                clothesColorID = 0;
                hairID = 0;
                headID = 0;
                clothesID = 0;
            }
            else
            {
                bodyID = 0;
                hairColorID = Defs.colorPallets["human_hair"].GetRandomID();
                bodyColorID = Defs.colorPallets["human_body"].GetRandomID();
                clothesColorID = Defs.colorPallets["human_clothes"].GetRandomID();
                clothesID = UnityEngine.Random.Range(0, Settings.CLOUTHES_COUNT);
                eyeID = UnityEngine.Random.Range(0, Settings.EYE_COUNT);
                hairID = UnityEngine.Random.Range(0, Settings.HAIR_COUNT);
                headID = 0;
            }
        }
    }
}