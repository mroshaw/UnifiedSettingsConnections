#if KAMGAM_RENDER_PIPELINE_HDRP && !KAMGAM_RENDER_PIPELINE_URP

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.NVIDIA;
using UnityEngine.Rendering.HighDefinition;

namespace Kamgam.SettingsGenerator
{
    // This settings is camera based. Thus we need to keep track of active cameras.

    public partial class UnityDLSSConnection : ConnectionWithOptions<string>
    {
        protected List<string> _labels;

        public UnityDLSSConnection()
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
                _labels.Add("Maximum Quality");
                _labels.Add("Balanced");
                _labels.Add("Maximum Performance");
                _labels.Add("Ultra Performance");
            }
            return _labels;
        }

        public override void SetOptionLabels(List<string> optionLabels)
        {
            if (optionLabels == null || optionLabels.Count != 5)
            {
                Debug.LogError("Invalid new labels. Need to be five.");
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
            if (Camera.main == null || !HDDynamicResolutionPlatformCapabilities.DLSSDetected)
                return 0;

            // Fetch from current camera
            HDAdditionalCameraData settings = Camera.main.GetComponent<HDAdditionalCameraData>();
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
                settings.allowDeepLearningSuperSampling = false;
                return;
            }

            settings.allowDeepLearningSuperSampling = true;
            settings.deepLearningSuperSamplingUseCustomQualitySettings = true;

            switch(index)
            {
                case 1:
                    settings.deepLearningSuperSamplingQuality = 2;
                    break;
                case 2:
                    settings.deepLearningSuperSamplingQuality = 1;
                    break;

                case 3:
                    settings.deepLearningSuperSamplingQuality = 0;
                    break;
                case 4:
                    settings.deepLearningSuperSamplingQuality = 3;
                    break;

            }
        }
    }
}

#endif