using MineBlock3D.Scripts.Gameplay;
using UnityEngine;

namespace MineBlock3D.Scripts.States
{
    public class GameplayState : IState
    {
        public void Entry()
        {
            Game.Launch();
        }

        public void Exit()
        {
            
        }
    }
}