using MineBlock3D.Scripts.Service;
using UnityEngine;

namespace MineBlock3D.Scripts.States
{
    public class BootstrapState : IState
    {
        public readonly StateMachine stateMachine;
        public readonly IFactory factory;
        
        public BootstrapState(StateMachine stateMachine, IFactory factory)
        {
            this.stateMachine = stateMachine;
            this.factory = factory;
        }
        
        public void Entry()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}