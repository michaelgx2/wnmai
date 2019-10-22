using System.Collections.Generic;

namespace WnMAiModuleLib
{
    public interface IStateTemplate
    {
        /// <summary>
        /// All States
        /// </summary>
        Dictionary<string, WsState> States { get; set; }
    }
}