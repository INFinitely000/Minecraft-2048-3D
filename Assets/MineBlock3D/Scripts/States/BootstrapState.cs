using MineBlock3D.Scripts.Gameplay;
using MineBlock3D.Scripts.Service;
using UnityEngine;

namespace MineBlock3D.Scripts.States
{
    public class BootstrapState : IState
    {
        public const string BlockSpawnerAssetName = "BlockSpawner";
        
        public readonly StateMachine stateMachine;
        public readonly IFactory factory;
        public readonly IAssets assets;
        public readonly IInput input;
        
        public BootstrapState(StateMachine stateMachine, IFactory factory, IAssets assets, IInput input)
        {
            this.stateMachine = stateMachine;
            this.factory = factory;
            this.assets = assets;
            this.input = input;
        }
        
        public void Entry()
        {
            var createdFactory = factory.Create<BlockSpawner>(BlockSpawnerAssetName);
                createdFactory.Construct(assets, factory, input);

            stateMachine.Entry<GameplayState>();
        }

        public void Exit()
        {
            
        }
    }
}