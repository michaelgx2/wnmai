using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace WnMAiModuleLib
{
    /// <summary>
    /// AI State Machine
    /// </summary>
    public class WsMachine
    {
        /// <summary>
        /// Current information
        /// </summary>
        public dynamic Infos { get; set; } = new ExpandoObject();
        /// <summary>
        /// Current state
        /// </summary>
        public string CurrentState { get; private set; } = "";

        /// <summary>
        /// Current WsState instance
        /// </summary>
        public WsState Current
        {
            get
            {
                if (States.ContainsKey(CurrentState))
                {
                    return States[CurrentState];
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// All States
        /// </summary>
        public Dictionary<string, WsState> States { get; set; } = new Dictionary<string, WsState>();

        public WsMachine SetStates(IStateTemplate stateTemplate)
        {
            States = stateTemplate.States;
            return this;
        }

        public WsMachine ChangeToState(string toState, object data)
        {
            CurrentState = toState;
            Current.Init?.Invoke(Infos, data);
            return this;
        }

        public WsMachine ChangeToState(WsState state, object data)
        {
            try
            {
                var kv = States.FirstOrDefault(k => k.Value == state);
                CurrentState = kv.Key;
                Current.Init?.Invoke(Infos, data);
            }
            catch
            {
                //Do nothing
            }

            return this;
        }

        /// <summary>
        /// Run every frame
        /// </summary>
        /// <param name="timeStep"></param>
        /// <returns></returns>
        public WsMachine UpdatePerFrame(float timeStep, object data)
        {
            if (Current == null) return this;
            Current.CollectInfo?.Invoke(Infos, data);
            Current.Action?.Invoke(Infos, timeStep, data);
            //Judgment of changing states
            List<WsState> changeTos = new List<WsState>();
            foreach (var judge in Current.Judges)
            {
                if (judge.Value(Infos, this, data))
                {
                    changeTos.Add(States[judge.Key]);
                }
            }
            //Sort and change
            if (changeTos.Count > 0)
            {
                changeTos.Sort(((left, right) =>
                {
                    if (left.Weight > right.Weight) return -1;
                    else if (left.Weight < right.Weight) return 1;
                    else return 0;
                }));
                ChangeToState(changeTos[0], data);
            }
            return this;
        }
    }
}