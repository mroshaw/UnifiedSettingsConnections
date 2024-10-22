#if KAMGAM_RENDER_PIPELINE_HDRP

using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace Kamgam.SettingsGenerator
{
    public static class HDRenderPipelineCameraUtils
    {
        private static FieldInfo RenderPipelineSettings_FieldInfo;

        public static void SetCameraCustomFramePropertyState(FrameSettingsField frameSettingField, bool enabled)
        {
            var cameras = Camera.allCameras;
            foreach (Camera cam in cameras)
            {
                if (cam.gameObject.activeInHierarchy && cam.isActiveAndEnabled)
                {
                    HDAdditionalCameraData settings = cam.GetComponent<HDAdditionalCameraData>();
                    settings.customRenderingSettings = true;
                    settings.renderingPathCustomFrameSettings.SetEnabled(frameSettingField, enabled);
                }
            }
        }

        public static void EnableCustomFrameProperty(FrameSettingsField frameSettingField)
        {
            Camera[] cameras = Camera.allCameras;
            foreach (Camera cam in cameras)
            {
                if (cam.gameObject.activeInHierarchy && cam.isActiveAndEnabled)
                {
                    HDAdditionalCameraData settings = cam.GetComponent<HDAdditionalCameraData>();
                    settings.customRenderingSettings = true;
                    settings.renderingPathCustomFrameSettingsOverrideMask.mask[(uint)frameSettingField] = true;
                }
            }
        }

        public static bool GetCameraCustomFramePropertyState(FrameSettingsField frameSettingField)
        {
            if (Camera.main == null)
                return false;

            // Fetch from current camera
            HDAdditionalCameraData settings = Camera.main.GetComponent<HDAdditionalCameraData>();
            if (settings == null)
            {
                return false;
            }

            return settings.renderingPathCustomFrameSettings.IsEnabled(frameSettingField);
        }
    }
}

#endif