using StandWorld.UI;
using UnityEngine;

namespace StandWorld.Controllers
{
    public class WindowController : MonoBehaviour
    {
        private Vector2 scale = new Vector2(1f, 1f);
        private Vector2 resolution;
        private Vector2 pivotPoint;

        private void Start()
        {
            WindowComponents.LoadComponents();

            resolution = new Vector2(Screen.width, Screen.height);
            scale = new Vector2(scale.x * (resolution.x / 3840f), scale.y * (resolution.y / 2160f));
        }

        private void OnGUI()
        {
            if (resolution.x != Screen.width || resolution.y != Screen.height)
            {
                scale = new Vector2(scale.x * (Screen.width / resolution.x), scale.y * (Screen.height / resolution.y));
                resolution.x = Screen.width;
                resolution.y = Screen.height;
            }

            pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            GUIUtility.ScaleAroundPivot(scale, pivotPoint);
            
            foreach (Window window in Window.windows)
            {
                window.OnGUI();
            }
        }
    }
}