using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
        public static void AddAnimal(AnimalDef def)
        {
            animals.Add(def.uID, def);
        }

        public static void LoadAnimalsFromCode()
        {
            animals = new Dictionary<string, AnimalDef>();

            AddAnimal(
                new AnimalDef
                {
                    uID = "chiken",
                    graphics = new GraphicDef
                    {
                        textureName = "chicken_front"
                    }
                }
            );

        }

    }
}