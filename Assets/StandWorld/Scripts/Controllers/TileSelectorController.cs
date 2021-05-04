using System.Collections.Generic;
using StandWorld.Characters;
using StandWorld.Game;
using StandWorld.UI;
using UnityEngine;

namespace StandWorld.Controllers
{
    public class TileSelectorController : MonoBehaviour
    {
        private Vector2Int currentTilePosition;
        private int currentIndex;
        private Dictionary<BaseCharacter, WindowCharacter> _characterWindows;

        private void Awake()
        {
            _characterWindows = new Dictionary<BaseCharacter, WindowCharacter>();
        }

        private void Update()
        {
            if (ToolBox.contoller.ready)
            {
                int i = 0;
                if (Input.GetMouseButtonDown(0))
                {
                    foreach (BaseCharacter character in ToolBox.map[ToolBox.cameraController.tileMapMousePosition].characters)
                    {
                        if (i == currentIndex)
                        {
                            DisplayCharacterData(ToolBox.cameraController.tileMapMousePosition, character);
                        }

                        i++;
                    }
                }
            }
        }

        private void DisplayCharacterData(Vector2Int position, BaseCharacter character)
        {
            if (currentTilePosition == position)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }

            if (_characterWindows.ContainsKey(character))
            {
                _characterWindows[character].Show();
            }
            else
            {
                _characterWindows.Add(character, new WindowCharacter(character));
            }
        }
    }
}