#if KAMGAM_RENDER_PIPELINE_HDRP && !KAMGAM_RENDER_PIPELINE_URP
using UnityEngine.Rendering.HighDefinition;

namespace Kamgam.SettingsGenerator
{
    public partial class CameraShadowToggleConnection : Connection<bool>
    {

        public CameraShadowToggleConnection()
        {
            HDRenderPipelineCameraUtils.EnableCustomFrameProperty(FrameSettingsField.ShadowMaps);
            HDRenderPipelineCameraUtils.EnableCustomFrameProperty(FrameSettingsField.ContactShadows);
            HDRenderPipelineCameraUtils.EnableCustomFrameProperty(FrameSettingsField.ScreenSpaceShadows);
            HDRenderPipelineCameraUtils.EnableCustomFrameProperty(FrameSettingsField.Shadowmask);
        }

        public override bool Get()
        {
            return HDRenderPipelineCameraUtils.GetCameraCustomFramePropertyState(FrameSettingsField.ShadowMaps) &&
                   HDRenderPipelineCameraUtils.GetCameraCustomFramePropertyState(FrameSettingsField.ContactShadows) &&
                   HDRenderPipelineCameraUtils.GetCameraCustomFramePropertyState(FrameSettingsField.ScreenSpaceShadows) &&
                   HDRenderPipelineCameraUtils.GetCameraCustomFramePropertyState(FrameSettingsField.Shadowmask);
        }

        public override void Set(bool enable)
        {
            HDRenderPipelineCameraUtils.SetCameraCustomFramePropertyState(FrameSettingsField.ShadowMaps, enable);
            HDRenderPipelineCameraUtils.SetCameraCustomFramePropertyState(FrameSettingsField.ContactShadows, enable);
            HDRenderPipelineCameraUtils.SetCameraCustomFramePropertyState(FrameSettingsField.ScreenSpaceShadows, enable);
            HDRenderPipelineCameraUtils.SetCameraCustomFramePropertyState(FrameSettingsField.Shadowmask, enable);
            NotifyListenersIfChanged(enable);
        }
    }
}
#endif