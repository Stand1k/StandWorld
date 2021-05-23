using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
        public static void AddAnimal(AnimalDef def)
        {
            animals.Add(def.uId, def);
        }

        public static void LoadAnimalsFromCode()
        {
            animals = new Dictionary<string, AnimalDef>();

            AddAnimal(
                new AnimalDef
                {
                    uId = "chiken",
                    shortDescription = "Курица — самая многочисленная и распространённая домашняя птица.",
                    graphics = new GraphicDef
                    {
                        textureName = "chicken_front",
                        size = new Vector2(0.7f, 0.7f)
                    }
                }
            );
            
            AddAnimal(
                new AnimalDef
                {
                    uId = "human",
                    shortDescription = "Чел це чел",
                    graphics = new GraphicDef()
                }
            );
        }

    }
}