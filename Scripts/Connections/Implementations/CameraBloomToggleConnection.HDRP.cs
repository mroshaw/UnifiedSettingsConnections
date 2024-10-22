#if KAMGAM_RENDER_PIPELINE_HDRP && !KAMGAM_RENDER_PIPELINE_URP
using UnityEngine.Rendering.HighDefinition;

namespace Kamgam.SettingsGenerator
{
    public partial class CameraBloomToggleConnection : Connection<bool>
    {

        public CameraBloomToggleConnection()
        {
            HDRenderPipelineCameraUtils.EnableCustomFrameProperty(FrameSettingsField.Bloom);
        }

        public override bool Get()
        {
            return HDRenderPipelineCameraUtils.GetCameraCustomFramePropertyState(FrameSettingsField.Bloom);
        }

        public override void Set(bool enable)
        {
            HDRenderPipelineCameraUtils.SetCameraCustomFramePropertyState(FrameSettingsField.Bloom, enable);
            NotifyListenersIfChanged(enable);
        }
    }
}
#endif