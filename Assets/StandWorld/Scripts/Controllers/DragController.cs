using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StandWorld.Controllers
{
    public class DragController : MonoBehaviour
    {
        public MenuController menuController;

        public bool isDragging;

        public Vector2 currFramePosition;
        public Vector2 lastFramePosition;
        Vector3 dragStartPosition;

        public Rect currentSelection;
        public RectI currentSelectionOnMap;

        public MenuOrderDef currentOrder => menuController.currentOrder;

        public void GetScreenRect(Vector2 origin, Vector2 mousePosition)
        {
            origin.y = Screen.height - origin.y;
            mousePosition.y = Screen.height - mousePosition.y;

            Vector2 topLeft = Vector2.Min(origin, mousePosition);
            Vector2 bottomRight = Vector2.Max(origin, mousePosition);
            currentSelection = Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }

        public void DrawScreenRect(Rect rect, Color color)
        {
            GUI.DrawTexture(rect, Res.TextureUnicolor(color));
        }

        public void DrawScreenRectBorder(Rect rect, Color color, float thickness)
        {
            DrawScreenRect(
                new Rect(
                    rect.xMin, rect.yMin, rect.width, thickness
                ),
                color
            );
            DrawScreenRect(
                new Rect(
                    rect.xMin, rect.yMin, thickness, rect.height
                ),
                color
            );
            DrawScreenRect(
                new Rect(
                    rect.xMax - thickness, rect.yMin, thickness, rect.height
                ),
                color
            );
            DrawScreenRect(
                new Rect(
                    rect.xMin, rect.yMax - thickness, rect.width, thickness
                ),
                color
            );
        }

        public void Start()
        {
            menuController = GetComponent<MenuController>();
            isDragging = false;
        }

        public void Reset()
        {
            currFramePosition = Vector2.zero;
            isDragging = false;
        }

        public void Update()
        {
            BeginSelection();
            UpdateSelection();

            lastFramePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        public void OnGUI()
        {
            UpdateDrawRect();
        }

        public void BeginSelection()
        {
            if (currentOrder == null)
            {
                return;
            }

            if (
                isDragging == false &&
                Input.GetMouseButtonDown(0) &&
                currentOrder.selector != SelectorType.Tile &&
                !EventSystem.current.IsPointerOverGameObject()
            )
            {
                currFramePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                isDragging = true;
            }
        }

        public void UpdateSelection()
        {
            if (!isDragging)
            {
                return;
            }

            GetScreenRect(currFramePosition, lastFramePosition);
            GetMapRect(currFramePosition, lastFramePosition);

            if (
                Input.GetMouseButtonUp(0) &&
                currentOrder.selector != SelectorType.Tile &&
                !EventSystem.current.IsPointerOverGameObject()
            )
            {
                CheckOrders();
            }
        }

        public void UpdateDrawRect()
        {
            if (currentOrder == null || !isDragging)
            {
                return;
            }

            if (currentOrder.selector == SelectorType.AreaTile)
            {
                DrawScreenRectBorder(currentSelection, new Color(0, 1, 0, .5f), 3f);
            }
        }

        public void GetMapRect(Vector2 start, Vector2 end)
        {
            Vector2 startInGame = Camera.main.ScreenToWorldPoint(start);
            Vector2 endInGame = Camera.main.ScreenToWorldPoint(end);

            if (endInGame.x < startInGame.x)
            {
                float tmp = endInGame.x;
                endInGame.x = startInGame.x;
                startInGame.x = tmp;
            }

            if (endInGame.y < startInGame.y)
            {
                float tmp = endInGame.y;
                endInGame.y = startInGame.y;
                startInGame.y = tmp;
            }

            if (currentOrder.selector == SelectorType.AreaTile)
            {
                currentSelectionOnMap = new RectI(
                    new Vector2Int(Mathf.FloorToInt(startInGame.x + 0.5f), Mathf.FloorToInt(startInGame.y + 0.5f)),
                    new Vector2Int(Mathf.FloorToInt(endInGame.x + 0.5f), Mathf.FloorToInt(endInGame.y + 0.5f))
                );
            }
            else if (currentOrder.selector == SelectorType.Line)
            {
                currentSelectionOnMap = new RectI(
                    new Vector2Int(Mathf.FloorToInt(startInGame.x + 0.5f), Mathf.FloorToInt(startInGame.y + 0.5f)),
                    new Vector2Int(Mathf.FloorToInt(startInGame.x + endInGame.y + 0.5f), Mathf.FloorToInt(startInGame.y + 0.5f))
                );
            }
        }


        public void CheckOrders()
        {
            if (currentOrder == null)
            {
                return;
            }

            if (currentOrder.selector == SelectorType.AreaTile)
            {
                currentOrder.actionArea(currentSelectionOnMap);
            }

            Reset();
        }
    }
}