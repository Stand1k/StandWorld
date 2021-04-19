using System.Collections;
using System.Collections.Generic;
using Priority_Queue;
using StandWorld.Characters;
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
    public bool reserved { get; set; }

    public float gCost { get; set; }
    public float hCost { get; set; }
    public float fCost => gCost + hCost;
    public TileProperty parent { get; set; }

    public List<BaseCharacter> characters { get; protected set; }

    public TileProperty(Vector2Int position)
    {
        this.position = position;
        characters = new List<BaseCharacter>();
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
        reserved = false;
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
                fertility *= tilable.tilableDef.fertility;
            }
            
            if (!blockPath && pathCost != 0)
            {
                pathCost *= tilable.tilableDef.pathCost;
            }

            if (blockPath == false)
            {
                blockPath = tilable.tilableDef.blockPath;
            }
            
            if (blockStackable == false)
            {
                blockStackable = tilable.tilableDef.blockStackable;
            }
            
            if (blockPlant == false)
            {
                blockPlant = tilable.tilableDef.blockPlant;
            }
            
            if (blockBuilding == false)
            {
                blockBuilding = tilable.tilableDef.blockBuilding;
            }
            
            if (supportRoof == false)
            {
                supportRoof = tilable.tilableDef.supportRoof;
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
