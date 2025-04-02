using System;
using MineBlock3D.Scripts.Service;
using MineBlock3D.Scripts.States;
using UnityEngine;

namespace MineBlock3D.Scripts
{
    [DefaultExecutionOrder(-128)]
    public class Bootstrap : MonoBehaviour
    {
        [field: SerializeField] public GameData GameData { get; private set; }
        
        public StateMachine StateMachine { get; private set; }
        public Services Services { get; private set; }
        
        private void Awake() => Initialize();
    
        private void Initialize()
        {
            Services = new Services();

            RegisterServices();
            
            StateMachine = new StateMachine(Services);
            StateMachine.Entry<BootstrapState>();
        }

        private void RegisterServices()
        {
            Services.Register<IAssets>(GameData.Assets);
            Services.Register<IInput>(new StandaloneInput());
            Services.Register<IFactory>( new Factory( Services.Single<IAssets>() ));
        }
    }
}
