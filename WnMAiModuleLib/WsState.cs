using System.Collections.Generic;

namespace WnMAiModuleLib
{
    public class WsState
    {
        /// <summary>
        /// How important this state is.
        /// </summary>
        public float Weight { get; set; } = 0;
        /// <summary>
        /// How to init the state
        /// </summary>
        public DelInitState Init { get; set; }
        /// <summary>
        /// How to collect information
        /// </summary>
        public DelCollectInfo CollectInfo { get; set; }
        /// <summary>
        /// What to do at this state
        /// </summary>
        public DelAction Action { get; set; }
        /// <summary>
        /// Judgment of every frame to decide if it's needed to change state
        /// </summary>
        public Dictionary<string, DelJudge> Judges { get; set; } = new Dictionary<string, DelJudge>();
        
        public WsState SetWeight(float weight)
        {
            Weight = weight;
            return this;
        }

        public WsState SetInit(DelInitState init)
        {
            Init = init;
            return this;
        }

        public WsState SetCollector(DelCollectInfo collector)
        {
            CollectInfo = collector;
            return this;
        }

        public WsState SetAction(DelAction action)
        {
            Action = action;
            return this;
        }

        public WsState AddJudge(string toState, DelJudge judge)
        {
            if (Judges.ContainsKey(toState))
            {
                Judges[toState] = judge;
            }
            else
            {
                Judges.Add(toState, judge);
            }
            return this;
        }
    }
}