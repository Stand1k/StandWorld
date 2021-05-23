using System;
using System.Collections.Generic;
using StandWorld.Game;
using UnityEngine;

namespace StandWorld.Controllers
{
    public class TickController : MonoBehaviour
    {
        private Dictionary<KeyCode, int> tabShortcuts = new Dictionary<KeyCode, int>();

        private void Start()
        {
            tabShortcuts.Add(KeyCode.Space, 0);
            tabShortcuts.Add(KeyCode.Alpha1, 1);
            tabShortcuts.Add(KeyCode.Alpha2, 2);
            tabShortcuts.Add(KeyCode.Alpha3, 3);
        }

        void LateUpdate()
        {
            foreach (KeyValuePair<KeyCode, int> kv in tabShortcuts)
            {
                if (Input.GetKeyDown(kv.Key))
                {
                    CheckButton(kv.Key);
                }
            }
        }

        void CheckButton(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.Space:
                    if (ToolBox.Instance.tick.isStop)
                    {
                        GameProceed();
                    }
                    else
                    {
                        GamePause();
                    }
                    break;
                
                case KeyCode.Alpha1:
                    GameProceed();
                    break;
                
                case KeyCode.Alpha2:
                    GameSpeedx2();
                    break;
                
                case KeyCode.Alpha3:
                    GameSpeedx3();
                    break;
            }
        }

        public void GamePause()
        {
            ToolBox.Instance.tick.isStop = true;
            Shader.SetGlobalFloat(ToolBox.Instance.Speed, 0.0f);
            Shader.SetGlobalFloat(ToolBox.Instance.ScrollSpeed, 0.0f);
        }

        public void GameProceed()
        {
            ToolBox.Instance.tick.speed = 1f;
            ToolBox.Instance.tick.isStop = false;
            Shader.SetGlobalFloat(ToolBox.Instance.Speed, 1f);
            Shader.SetGlobalFloat(ToolBox.Instance.ScrollSpeed, 0.2f);
        }

        public void GameSpeedx2()
        {
            ToolBox.Instance.tick.speed = 2f;
            ToolBox.Instance.tick.isStop = false;
            Shader.SetGlobalFloat(ToolBox.Instance.Speed, 2f);
            Shader.SetGlobalFloat(ToolBox.Instance.ScrollSpeed, 0.4f);
        }

        public void GameSpeedx3()
        {
            ToolBox.Instance.tick.speed = 3f;
            ToolBox.Instance.tick.isStop = false;
            Shader.SetGlobalFloat(ToolBox.Instance.Speed, 3f);
            Shader.SetGlobalFloat(ToolBox.Instance.ScrollSpeed, 0.6f);
        }
    }
}