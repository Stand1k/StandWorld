using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.Entities
{
    public class Plant : Tilable
    {
        private Color32 _leafColor;
        private Color32 _woodColor;
        
        public Plant(Vector2Int position, TilableDef def)
        {
            this.position = position;
            this.def = def;
            
            if (def.type == TilableType.Grass)
            {
                _leafColor = Defs.colorPallets["cols_leafsGreen"].GetRandom();
                mainGraphic = GraphicInstance.GetNew(def.graphics, _leafColor);
            }
            else if (def.type == TilableType.Tree)
            {
                addGraphics = new Dictionary<string, GraphicInstance>();
                _leafColor = Defs.colorPallets["cols_leafsGreen"].GetRandom();
                _woodColor = Defs.colorPallets["cols_wood"].colors[0];
                mainGraphic = GraphicInstance.GetNew(
                    def.graphics,
                    _woodColor, 
                    Res.textures[def.graphics.textureName + "_base"],
                    0
                    );
                
                addGraphics.Add("leafs", GraphicInstance.GetNew(
                        def.graphics,
                        _leafColor,
                        Res.textures[def.graphics.textureName + "_leafs"],
                        1
                        )
                );
            }
            else
            {
                mainGraphic = GraphicInstance.GetNew(def.graphics);
            }
        }
    }
}
