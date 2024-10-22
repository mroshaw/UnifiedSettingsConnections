#if KAMGAM_RENDER_PIPELINE_HDRP && !KAMGAM_RENDER_PIPELINE_URP && GPU_INSTANCER

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using GPUInstancer;

namespace Kamgam.SettingsGenerator
{
    // This settings is camera based. Thus we need to keep track of active cameras.

    internal enum GPUManagerType { None, DetailOnly, TreesOnly, Both }

    public partial class GPUInstancerConnection : ConnectionWithOptions<string>
    {
        protected List<string> _labels;

        public GPUInstancerConnection()
        {
        }

        public override List<string> GetOptionLabels()
        {
            return _labels ??= new List<string>
            {
                "Disabled",
                "Terrain and Trees",
                "Terrain Only",
                "Trees Only"
            };
        }

        public override void SetOptionLabels(List<string> optionLabels)
        {
            if (optionLabels == null || optionLabels.Count != 4)
            {
                Debug.LogError("Invalid new labels. Need to be four.");
                return;
            }

            _labels = optionLabels;
        }

        public override void RefreshOptionLabels()
        {
            _labels = null;
            GetOptionLabels();
        }

        /// <summary>
        /// Returns 0 if no camera is active.
        /// </summary>
        /// <returns></returns>
        public override int Get()
        {
            if (Camera.main == null)
                return 0;

            // Fetch from current camera
            var settings = Camera.main.GetComponent<HDAdditionalCameraData>();
            if (settings == null || !settings.allowDeepLearningSuperSampling)
                return 0;

            switch (settings.deepLearningSuperSamplingQuality)
            {
                case 2:
                    return 1;
                case 1:
                    return 2;
                case 0:
                    return 3;
                default:
                    return 4;
            }
        }

        public override void Set(int index)
        {
            switch (index)
            {
                case 0:
                    SetAllManagersState(GPUManagerType.None);
                    break;
                case 1:
                    SetAllManagersState(GPUManagerType.Both);
                    break;
                case 2:
                    SetAllManagersState(GPUManagerType.DetailOnly);
                    break;
                case 3:
                    SetAllManagersState(GPUManagerType.TreesOnly);
                    break;
            }
            NotifyListenersIfChanged(index);
        }

        private GPUManagerType GetAllManagersState()
        {
            List<GPUInstancerManager> allManagers = GPUInstancerAPI.GetActiveManagers();
            if (allManagers == null || allManagers.Count == 0)
            {
                return GPUManagerType.None;
            }

            bool isDetailActive = allManagers
                .OfType<GPUInstancerDetailManager>()
                .All(manager => manager.enabled);

            bool isTreesActive = allManagers
                .OfType<GPUInstancerTreeManager>()
                .All(manager => manager.enabled);

            return (isDetailActive, isTreesActive) switch
            {
                (true, true) => GPUManagerType.Both,
                (true, false) => GPUManagerType.DetailOnly,
                (false, true) => GPUManagerType.TreesOnly,
                _ => GPUManagerType.None
            };
        }

        private void SetAllManagersState(GPUManagerType managerType)
        {
            List<GPUInstancerManager> allManagers = GPUInstancerAPI.GetActiveManagers();
            if (allManagers == null || allManagers.Count == 0)
            {
                return;
            }
            foreach (GPUInstancerManager manager in allManagers)
            {
                if (manager is GPUInstancerDetailManager)
                {
                    manager.enabled = managerType == GPUManagerType.Both || managerType == GPUManagerType.DetailOnly;
                }

                if (manager is GPUInstancerTreeManager)
                {
                    manager.enabled = managerType == GPUManagerType.Both || managerType == GPUManagerType.TreesOnly;
                }
            }
        }
    }
}

#endif