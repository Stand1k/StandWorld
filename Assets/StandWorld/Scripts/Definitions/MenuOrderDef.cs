using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public class MenuOrderDef : Def
    {
        public string name;
        public string shortDesc;
        public TilableDef tilableDef;
        public Sprite sprite;

        public delegate void ActionDelegate(Vector2Int position);
        public delegate void ActionAreaDelegate(RectI rect);

        public ActionDelegate action;
        public ActionAreaDelegate actionArea;
        public SelectorType selector;
        public GraphicDef graphicDef;
        public Layer layer;
        public KeyCode keyCode = KeyCode.Escape;
    }
}