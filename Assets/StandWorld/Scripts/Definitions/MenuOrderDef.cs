using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.Definitions
{
    public enum SelectorType
    {
        Tile,
        Area,
        AreaTile,
        Line,
    }

    [System.Serializable]
    public class MenuOrderDef : Def
    {
        public string name;
        public string shortDesc;
        public TilableDef tilableDef;
        public Sprite sprite;

        public delegate void ActionDelegate(Vector2Int position);
        public delegate void ActionAreaDelegate(RectI position);

        public ActionDelegate action;
        public ActionAreaDelegate actionArea;
        public SelectorType selector;
    }
}