using System.Collections.Generic;
using System.Dynamic;
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

        public static void AddMenuOrderBuilding(MenuOrderDef def)
        {
            buildingOrders.Add(def.uId, def);
        }
        
        public static void AddMenuOrderZone(MenuOrderDef def)
        {
            zonesOrders.Add(def.uId, def);
        }

        public static void LoadMenuOrdersFromCode()
        {
            orders = new Dictionary<string, MenuOrderDef>();
            buildingOrders = new Dictionary<string, MenuOrderDef>();
            zonesOrders = new Dictionary<string, MenuOrderDef>();
            
            AddMenuOrder(
                new MenuOrderDef
                {
                    id = 0,
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
            
            AddMenuOrder(
                new MenuOrderDef
                {
                    id = 0,
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
                    id = 0,
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
                    id = 0,
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
            
             AddMenuOrderZone(
                new MenuOrderDef
                {   id = 1,
                    uId = "grow_zone",
                    selector = SelectorType.AreaTile,
                    layer = Layer.Orders,
                    sprite = Res.sprites["icon_grow_zone"],
                    actionArea = (rect) =>
                    {
                        GrowArea area = new GrowArea(plants["carrot"]);
                        area.Add(new RectI(new Vector2Int(rect.min.x, rect.min.y), rect.width, rect.height));
                    },
                    keyCode = KeyCode.Q,
                }
            );
            
            AddMenuOrderZone(
                new MenuOrderDef
                {   id = 1,
                    uId = "stock_zone",
                    selector = SelectorType.AreaTile,
                    layer = Layer.Orders,
                    sprite = Res.sprites["icon_stock_zone"],
                    actionArea = (rect) =>
                    {
                        StockArea stockarea = new StockArea(empty);
                        stockarea.Add(new RectI(new Vector2Int(rect.min.x, rect.min.y), rect.width, rect.height));
                    },
                    keyCode = KeyCode.W,
                }
            );
            
            AddMenuOrderZone(
                new MenuOrderDef
                {
                    id = 1,
                    uId = "del_zone",
                    selector = SelectorType.AreaTile,
                    layer = Layer.Orders,
                    sprite = Res.sprites["icon_del_zone"],
                    actionArea = (rect) =>
                    {
                        foreach (Vector2Int position in rect)
                        {
                            var tilable = ToolBox.map.GetTilableAt(position, Layer.Helpers);
                            
                            if (tilable != null)
                            {
                                tilable.Destroy();
                            }
                        }
                    },
                    keyCode = KeyCode.E,
                }
            );

            AddMenuOrderBuilding(
                new MenuOrderDef
                {
                    id = 2,
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
                    keyCode = KeyCode.Q,
                }
            );
            
            AddMenuOrderBuilding(
                new MenuOrderDef
                {
                    id = 2,
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
                    keyCode = KeyCode.W,
                }
            );
            
           
        }
    }
}