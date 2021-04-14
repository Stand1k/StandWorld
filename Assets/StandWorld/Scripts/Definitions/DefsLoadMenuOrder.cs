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
            orders.Add(def.uId, def);
        }

        public static void LoadMenuOrdersFromCode()
        {
            orders = new Dictionary<string, MenuOrderDef>();

            AddMenuOrder(
                new MenuOrderDef
                {
                    uId = "cancel",
                    name = "Відмінити накази",
                    shortDesc = "Відмініє всі накази в зазначеній обсласті.",
                    selector = SelectorType.AreaTile,
                    layer = Layer.Orders,
                    sprite = Res.sprites["order_to_cancel"],
                    actionArea = (rect) =>
                    {
                        foreach (Vector2Int position in rect)
                        {
                            foreach (Tilable tilable in ToolBox.map.GetAllTilablesAt(position))
                            {
                                tilable.ClearOrder();
                            }
                        }
                    },
                    keyCode = KeyCode.R,
                }
            );

            AddMenuOrder(
                new MenuOrderDef
                {
                    uId = "cut_wood",
                    name = "Зрубати дерева",
                    shortDesc = "Зрубує дерева в зазначеній області.",
                    selector = SelectorType.AreaTile,
                    sprite = Res.sprites["order_to_cut_tree"],
                    actionArea = (rect) =>
                    {
                        foreach (Vector2Int position in rect)
                        {
                            Plant plant = (Plant) ToolBox.map.grids[Layer.Plant].GetTilableAt(position);
                            if (plant != null && plant.tilableDef.cuttable && plant.tilableDef.type == TilableType.Tree)
                            {
                                plant.AddOrder(orders["cut_wood"]);
                            }
                        }
                    },
                    graphicDef = new GraphicDef
                    {
                        textureName = "order_to_cut_tree"
                    },
                    keyCode = KeyCode.E,
                }
            );

            AddMenuOrder(
                new MenuOrderDef
                {
                    uId = "cut_plants",
                    name = "Зрізати рослини",
                    shortDesc = "Зрізає рослини в зазначеній області.",
                    selector = SelectorType.AreaTile,
                    sprite = Res.sprites["order_to_cut_plant"],
                    actionArea = (rect) =>
                    {
                        foreach (Vector2Int position in rect)
                        {
                            Plant plant = (Plant) ToolBox.map.grids[Layer.Plant].GetTilableAt(position);
                            if (plant != null && plant.tilableDef.cuttable &&
                                plant.tilableDef.type == TilableType.Grass)
                            {
                                plant.AddOrder(orders["cut_plants"]);
                            }
                        }
                    },
                    graphicDef = new GraphicDef
                    {
                        textureName = "order_to_cut_plant"
                    },
                    keyCode = KeyCode.W,
                }
            );

            AddMenuOrder(
                new MenuOrderDef
                {
                    uId = "harvest_plants",
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
                            if (plant != null && plant.tilableDef.cuttable &&
                                plant.tilableDef.type == TilableType.Plant)
                            {
                                plant.AddOrder(orders["harvest_plants"]);
                            }
                        }
                    },
                    graphicDef = new GraphicDef
                    {
                        textureName = "order_harvest"
                    },
                    keyCode = KeyCode.Q,
                }
            );
        }
    }
}