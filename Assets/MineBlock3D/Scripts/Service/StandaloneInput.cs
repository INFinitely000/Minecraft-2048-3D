using UnityEngine;

namespace MineBlock3D.Scripts.Service
{
    public class StandaloneInput : IInput
    {
        public InputState Fire
        {
            get
            {
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
                return isGame ? inputPositionX / UnityEditor.EditorWindow.focusedWindow.position.width * 2f - 1f : 0;
#else
                var screenWidth = Screen.currentResolution.width;
                return inputPositionX / screenWidth * 2f - 1f;
#endif                
            }
        }
    }
}