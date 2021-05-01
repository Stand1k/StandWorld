using System;
using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.UI
{
    public class WindowBuildMenu : Window
    {
        public WindowBuildMenu()
        {
            _hasTitle = true;
            title = "Build Menu";
            closeButton = false;
            draggable = false;
            _centered = false;
            resizeable = false;
            _show = true;
            tabs = new List<string>();
            padding = new RectOffset(0, 0, 0, 0);
            initialSize = new Vector2(Screen.width, 100f);
            rect = new Rect(0, Screen.height-100, Screen.width, 100);
            size = initialSize;
        }

        public override void Content()
        {
            throw new NotImplementedException();
        }
    }
}