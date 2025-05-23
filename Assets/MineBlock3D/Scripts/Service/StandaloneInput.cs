using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MineBlock3D.Scripts.Service
{
    public class StandaloneInput : IInput
    {
        public InputState Fire
        {
            get
            {
                var eventData = new PointerEventData(EventSystem.current)
                {
                    position = Input.mousePosition
                };

                var results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);

                if (results.Count > 0) return InputState.None;
                
                if (Input.GetButtonDown("Fire1")) return InputState.Down;
                if (Input.GetButton("Fire1")) return InputState.Pressed;
                if (Input.GetButtonUp("Fire1")) return InputState.Up;

                return InputState.None;
            }
        }

        public float Horizontal
        {
            get
            {
                
                var inputPositionX = Input.mousePosition.x;
#if UNITY_EDITOR
                var isGame = UnityEditor.EditorWindow.focusedWindow && UnityEditor.EditorWindow.focusedWindow.titleContent.text == "Game";
                return isGame ? Mathf.Clamp(inputPositionX / UnityEditor.EditorWindow.focusedWindow.position.width * 2f - 1f, -1f, 1f) : 0;
#else
                var screenWidth = Screen.currentResolution.width;
                return Mathf.Clamp(inputPositionX / screenWidth * 2f - 1f, -1f, 1f);
#endif                
            }
        }
    }
}