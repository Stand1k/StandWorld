using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Game;
using UnityEngine;

namespace StandWorld.Entities
{
    public class GrowArea : Area
    {
        public static List<GrowArea> growAreas = new List<GrowArea>();
        public TilableDef def { get; protected set; }

        public GrowArea(TilableDef def)
        {
            this.def = def;
            color = new Color(0.76f, 1f, 0f, 0.5f);
            growAreas.Add(this);
        }

        protected override void AddTilable(Vector2Int position)
        {
            ToolBox.map.Spawn(
                position,
                new Field(position, Defs.empty, this)
            );
            base.AddTilable(position);
        }

        protected override void DelTilable(Vector2Int position)
        {
            base.DelTilable(position);
        }
    }
}