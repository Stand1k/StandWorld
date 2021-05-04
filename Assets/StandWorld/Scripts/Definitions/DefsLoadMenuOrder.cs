using System.Collections.Generic;
using StandWorld.Entities;
using StandWorld.Game;
using StandWorld.Helpers;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
        public static void AddMenuOrder(MenuOrderDef def)
        {
            orders.Add(def.uId, def);
        }

        public static void AddMenuOrderBuildings(MenuOrderDef def)
        {
            buildingOrders.Add(def.uId, def);
        }

        public static void LoadMenuOrdersFromCode()
        {
            orders = new Dictionary<string, MenuOrderDef>();
            buildingOrders = new Dictionary<string, MenuOrderDef>();

            AddMenuOrder(
                new MenuOrderDef
                {
                    uId = "cancel",
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
                    layer = Layer.Orders,
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

            AddMenuOrderBuildings(
                new MenuOrderDef
                {
                    uId = "woodWall_build",
                    layer = Layer.Orders,
                    selector = SelectorType.AreaTile,
                    sprite = Res.sprites["wall_1"],
                    actionArea = (rect) =>
                    {
                        foreach (Vector2Int position in rect)
                        {
                            WorldUtils.SpawnBuilding(position);
                        }
                    },
                    keyCode = KeyCode.B,
                }
            );
            
            AddMenuOrderBuildings(
                new MenuOrderDef
                {
                    uId = "cancel_building",
                    selector = SelectorType.AreaTile,
                    layer = Layer.Orders,
                    sprite = Res.sprites["order_to_cancel"],
                    actionArea = (rect) =>
                    {
                        foreach (Vector2Int position in rect)
                        {
                            WorldUtils.DeleteBlueprint(position);
                        }
                    },
                    keyCode = KeyCode.N,
                }
            );
        }
    }
}