using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class GlobalUtilities : MonoBehaviour
    {
        #region Events
        
        public static event Action UpdateEvent;

        #endregion

        #region Messages
        
        void Update()
        {
            UnityEngine.Profiling.Profiler.BeginSample("Global Update");
            
            UpdateEvent?.Invoke();
            
            UnityEngine.Profiling.Profiler.EndSample();
        }

        #endregion Messages
    }
}
