using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class StateMachine : UnitySingleton<StateMachine>
    {
        private static readonly HashSet<string> States = new HashSet<string>(); 
        public enum Mode
        {
            Add, Remove
        }


        public static event StateChangedEventHandler StateChanged;

        public delegate void StateChangedEventHandler(string state, Mode mode);
        protected static void OnStateChanged(string state, Mode mode)
        {
            if (StateChanged != null)
                StateChanged(state, mode);
        }

        public void Add(string state)
        {
            States.Add(state);
            OnStateChanged(state, Mode.Add);
        }

        public void Remove(string state)
        {
            States.Remove(state);
            OnStateChanged(state, Mode.Remove);
        }

        public bool Contains(string state)
        {
            return States.Contains(state);
        }

        public bool ContainsAll(string[] states)
        {
            var match = true;
            foreach (var state in states)
            {
                if (Contains(state)) continue;
                match = false;
                break;
            }
            return match;
        }

        public void AddAll(string[] states)
        {
            foreach (var state in states)
            {
                Add(state);
            }
        }

        public void RemoveAll(string[] states)
        {
            foreach (var state in states)
            {
                Remove(state);
            }
        }
    }
}
