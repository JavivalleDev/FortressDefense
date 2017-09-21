using System;
using Object = UnityEngine.Object;

namespace EnhancedHierarchy {
    /// <summary>
    /// Prevents wrong profiler samples count.
    /// Very useful for things other than Enhanced Hierarchy, Unity could implement this on its API, just saying :).
    /// Commented because this is for debug purposes only.
    /// </summary>
    internal class ProfilerSample : IDisposable {

        public ProfilerSample(string name) {
            //Profiler.BeginSample(_buildingName);
        }

        public ProfilerSample(string name, Object targetObject) {
            //Profiler.BeginSample(_buildingName, targetObject);
        }

        public void Dispose() {
            //Profiler.EndSample();
        }

    }
}