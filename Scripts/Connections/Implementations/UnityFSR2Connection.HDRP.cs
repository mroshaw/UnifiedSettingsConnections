#if KAMGAM_RENDER_PIPELINE_HDRP && !KAMGAM_RENDER_PIPELINE_URP

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.NVIDIA;
using UnityEngine.Rendering.HighDefinition;

namespace Kamgam.SettingsGenerator
{
    // This settings is camera based. Thus we need to keep track of active cameras.

    public partial class UnityFSR2Connection : ConnectionWithOptions<string>
    {
        protected List<string> _labels;

        public UnityFSR2Connection()
        {
            CameraDetector.Instance.OnNewCameraFound += onNewCameraFound;
        }

        protected void onNewCameraFound(Camera cam)
        {
            setOnCamera(cam, lastKnownValue);
        }

        public override List<string> GetOptionLabels()
        {
            if (_labels == null)
            {
                _labels = new List<string>();
                _labels.Add("Disabled");
                _labels.Add("Quality");
                _labels.Add("Balanced");
                _labels.Add("Performance");
                _labels.Add("Ultra Performance");
            }
            return _labels;
        }

        public override void SetOptionLabels(List<string> optionLabels)
        {
            if (optionLabels == null || optionLabels.Count != 6)
            {
                Debug.LogError("Invalid new labels. Need to be six.");
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
            if (Camera.main == null || !HDDynamicResolutionPlatformCapabilities.FSR2Detected)
                return 0;

            // Fetch from current camera
            HDAdditionalCameraData settings = Camera.main.GetComponent<HDAdditionalCameraData>();
            if (settings == null || !settings.allowFidelityFX2SuperResolution)
                return 0;

            switch (settings.fidelityFX2SuperResolutionQuality)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 3;
                default:
                    return 4;
            }
        }

        public override void Set(int index)
        {
            var cameras = Camera.allCameras;
            foreach (var cam in cameras)
            {
                if (cam.gameObject.activeInHierarchy && cam.isActiveAndEnabled)
                {
                    setOnCamera(cam, index);
                }
            }

            NotifyListenersIfChanged(index);
        }

        private static void setOnCamera(Camera cam, int index)
        {
            var settings = cam.GetComponent<HDAdditionalCameraData>();
            if (settings == null)
                return;

            if (index == 0)
            {
                settings.allowFidelityFX2SuperResolution = false;
                return;
            }

            settings.allowFidelityFX2SuperResolution = true;
            settings.fidelityFX2SuperResolutionUseOptimalSettings = true;

            switch(index)
            {
                case 1:
                    settings.fidelityFX2SuperResolutionQuality = 0;
                    break;
                case 2:
                    settings.fidelityFX2SuperResolutionQuality = 1;
                    break;

                case 3:
                    settings.fidelityFX2SuperResolutionQuality = 2;
                    break;
                case 4:
                    settings.fidelityFX2SuperResolutionQuality = 3;
                    break;

            }
        }
    }
}

#endif