using System.Collections;
using System.Collections.Generic;
using StandWorld.Entities;
using StandWorld.Game;
using UnityEngine;

public class TileProperty
{
    public Vector2Int position;
    public float pathCost;
    public float fertility;

    public bool blockPath;
    public bool blockPlant;
    public bool blockStackable;
    public bool blockBuilding;
    public bool supportRoof;

    public TileProperty(Vector2Int position)
    {
        this.position = position;
        pathCost = 1f;
        fertility = 1f;

        blockPath = false;
        blockBuilding = false;
        blockPlant = false;
        blockStackable = false;
        supportRoof = false;
    }

    public void Update()
    {
        fertility = 1f;
        pathCost = 1f;
        blockPath = false;
        blockBuilding = false;
        blockPlant = false;
        blockStackable = false;
        supportRoof = false;
        
        foreach (Tilable tilable in ToolBox.map.GetAllTilablesAt(position))
        {
            if (fertility != 0f)
            {
                fertility *= tilable.def.fertility;
            }
            
            if (!blockPath && pathCost != 0)
            {
                pathCost *= tilable.def.pathCost;
            }

            if (blockPath == false)
            {
                blockPath = tilable.def.blockPath;
            }
            
            if (blockStackable == false)
            {
                blockBuilding = tilable.def.blockStackable;
            }
            
            if (blockPlant == false)
            {
                blockPlant = tilable.def.blockPlant;
            }
            
            if (blockBuilding == false)
            {
                blockBuilding = tilable.def.blockBuilding;
            }
            
            if (supportRoof == false)
            {
                supportRoof = tilable.def.supportRoof;
            }
        }

    }
}
