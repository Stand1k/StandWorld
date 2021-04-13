﻿using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Game;
using UnityEngine;

namespace StandWorld.Entities
{
    public class StockArea : Area
    {
        public static List<StockArea> areas = new List<StockArea>();
        public TilableDef zoneConfig { get; protected set; }
        public HashSet<Stackable> stackables { get; protected set; }

        public StockArea(TilableDef zoneConfig)
        {
            stackables = new HashSet<Stackable>();
            this.zoneConfig = zoneConfig;
            color = new Color(0f, 0.3f, 1f, 0.4f);
            areas.Add(this);
        }

        protected override void AddTilable(Vector2Int position)
        {
            Stackable stackable = new Stackable(position, this);
            ToolBox.map.Spawn(position, stackable);
            stackables.Add(stackable);
            base.AddTilable(position);
        }

        protected override void DelTilable(Vector2Int position)
        {
            base.DelTilable(position);
        }
    }
}