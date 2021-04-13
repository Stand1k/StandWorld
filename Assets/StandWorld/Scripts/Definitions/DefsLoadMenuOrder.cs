using System.Collections.Generic;
using StandWorld.Entities;
using StandWorld.Game;
using StandWorld.Helpers;
using UnityEngine;

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
                    actionArea = (rect) =>
                    {
                        foreach (Vector2Int position in rect)
                        {
                            Plant plant = (Plant) ToolBox.map.grids[Layer.Plant].GetTilableAt(position);
                            if (plant != null && plant.def.cuttable && plant.def.type == TilableType.Tree)
                            {
                                plant.AddOrder(orders["cut_wood"]);
                            }
                        }
                    },
                    graphicDef = new GraphicDef
                    {
                        textureName = "order_to_cut_tree"
                    },
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
                    actionArea = (rect) =>
                    {
                        foreach (Vector2Int position in rect)
                        {
                            Plant plant = (Plant) ToolBox.map.grids[Layer.Plant].GetTilableAt(position);
                            if (plant != null && plant.def.cuttable && plant.def.type == TilableType.Grass)
                            {
                                plant.AddOrder(orders["cut_plants"]);
                            }
                        }
                    },
                    graphicDef = new GraphicDef
                    {
                        textureName = "order_to_cut_plant"
                    },
                }
            );

            AddMenuOrder(
                new MenuOrderDef
                {
                    uID = "harvest_plants",
                    name = "Зібрати врожай",
                    layer = Layer.Orders,
                    shortDesc = "Збирає врожай в зазначеній області.",
                    selector = SelectorType.AreaTile,
                    sprite = Res.sprites["order_harvest"],
                    actionArea = (rect) =>
                    {
                        foreach (Vector2Int position in rect)
                        {
                            Plant plant = (Plant) ToolBox.map.grids[Layer.Plant].GetTilableAt(position);
                            if (plant != null && plant.def.cuttable && plant.def.type == TilableType.Plant)
                            {
                                plant.AddOrder(orders["harvest_plants"]);
                            }
                        }
                    },
                    graphicDef = new GraphicDef
                    {
                        textureName = "order_harvest"
                    }
                }
            );
        }
    }
}