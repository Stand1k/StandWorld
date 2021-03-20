using System.Collections;
using System.Collections.Generic;
using Priority_Queue;
using StandWorld.Entities;
using StandWorld.Game;
using UnityEngine;

/// <summary>
/// Дані проходимості тайла
/// </summary>
public class TileProperty   
{
    public Vector2Int position { get; protected set; }
    public float pathCost { get; protected set; }
    public float fertility { get; protected set; }

    public bool blockPath { get; protected set; }
    public bool blockPlant { get; protected set; }
    public bool blockStackable { get; protected set; }
    public bool blockBuilding { get; protected set; }
    public bool supportRoof { get; protected set; }

    public float gCost;
    public float hCost;
    public float fCost
    {
        get { return gCost + hCost; }
    }
    public TileProperty parent;

    public TileProperty(Vector2Int position)
    {
        this.position = position;
        Reset();
    }

    public void Reset()
    {
        fertility = 1f;
        pathCost = 1f;
        blockPath = false;
        blockBuilding = false;
        blockPlant = false;
        blockStackable = false;
        supportRoof = false;
        gCost = 0;
        hCost = 0;
        parent = null;
    }

    public void Update()
    {
        Reset();

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
                blockStackable = tilable.def.blockStackable;
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

        if (blockPath)
        {
            pathCost = 0f;
        }
        else if (pathCost <= 0)
        {
            blockPath = true;
        }

    }
}
