using System;
using System.Collections.Generic;
using Core;
using PathSystem;
using UnityEngine;

namespace GhostSystem
{
    public class MovementStateMachine: IStateMachine
    {
        private Dictionary<Type, AMovementState> _states;
        private AMovementState _currAState;

        public AMovementState CurrentState => _currAState;
        
        public MovementStateMachine(params AMovementState[] states)
        {
            SetupStates(states);
        }

        public void Move(PathWalker pathWalker)
        {
            _currAState.Move(pathWalker);
        }
        
        public bool ChangeState<T>()
        {
            _currAState?.Exit();
            if (_states.ContainsKey(typeof(T)))
            {
                _currAState = _states[typeof(T)];
                _currAState.Enter();
                return true;
            }

            return false;
        }
        
        private void SetupStates(params AMovementState[] states)
        {
            _states = new();
            foreach (var state in states)
            {
                _states.Add(state.GetType(),state);
            }
        }
    }
}
