using StandWorld.UI;
using UnityEngine;

namespace StandWorld.Controllers
{
    public class WindowController : MonoBehaviour
    {
        private void Start()
        {
            WindowComponents.LoadComponents();
        }

        private void OnGUI()
        {
            foreach (Window window in Window.windows)
            {
                window.OnGUI();
            }
        }
    }
}