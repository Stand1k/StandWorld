﻿using System.Collections.Generic;
using StandWorld.Entities;
using StandWorld.Game;
using StandWorld.Helpers;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
        public static void AddMenuOrder(MenuOrderDef def)
        {
            orders.Add(def.uID, def);
        }

        public static void LoadMenuOrdersFromCode()
        {
            orders = new Dictionary<string, MenuOrderDef>();

            AddMenuOrder(
                new MenuOrderDef
                {
                    uID = "cut_wood",
                    name = "Зрубати дерева",
                    shortDesc = "Зрубує дерева в зазначеній області.",
                    selector = SelectorType.AreaTile,
                    sprite = Res.sprites["order_to_cut_tree"],
                    action = position =>
                    {
                        Plant plant = (Plant) ToolBox.map.grids[Layer.Plant].GetTilableAt(position);
                        if (plant != null && plant.def.cuttable && plant.def.type == TilableType.Tree)
                        {
                            plant.OrderToCut();
                        }
                    }
                }
            );
            AddMenuOrder(
                new MenuOrderDef
                {
                    uID = "cut_plants",
                    name = "Зрізати рослини",
                    shortDesc = "Зрізає рослини в зазначеній області.",
                    selector = SelectorType.AreaTile,
                    sprite = Res.sprites["order_to_cut_plant"],
                    action = position =>
                    {
                        Plant plant = (Plant) ToolBox.map.grids[Layer.Plant].GetTilableAt(position);
                        if (plant != null && plant.def.cuttable)
                        {
                            plant.OrderToCut();
                        }
                    }
                }
            );
            AddMenuOrder(
                new MenuOrderDef
                {
                    uID = "harvest_plants",
                    name = "Зібрати врожай",
                    shortDesc = "Збирає врожай в зазначеній області.",
                    selector = SelectorType.AreaTile,
                    sprite = Res.sprites["order_harvest"],
                    action = position =>
                    {
                        Plant plant = (Plant) ToolBox.map.grids[Layer.Plant].GetTilableAt(position);
                        if (plant != null && plant.def.cuttable && plant.def.type == TilableType.Plant)
                        {
                            plant.OrderToCut();
                        }
                    }
                }
            );
        }
    }
}