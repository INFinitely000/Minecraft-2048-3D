using System;
using System.Collections.Generic;
using MineBlock3D.Scripts.Service;

namespace MineBlock3D.Scripts.States
{
    public class StateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        
        public IExitableState Current { get; private set; }

        public readonly Services services;
        
        public StateMachine(Services services)
        {
            this.services = services;
            
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services.Single<IFactory>())
            };
        }


        public void Entry<TState>() where TState : IState
        {
            var state = ChangeState<TState>() as IState;
                state.Entry();
        }

        public void Entry<TState, TPayload>(TPayload payload) where TState : IPayloadState<TPayload>
        {
            var state = ChangeState<TState>() as IPayloadState<TPayload>;
                state.Entry(payload);
        }

        private IExitableState ChangeState<TState>() where TState : IExitableState
        {
            Current?.Exit();
            Current = _states[typeof(TState)] as IExitableState;
            return Current;
        }
    }
}