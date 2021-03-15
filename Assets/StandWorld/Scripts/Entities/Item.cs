using System.Collections;
using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Entities;
using UnityEngine;

public class Item : Tilable
{

    private InventoryTilable _inventory;

    public Item(TilableDef def)
    {
        this.def = def;
    }

}
