#if KAMGAM_RENDER_PIPELINE_HDRP && !KAMGAM_RENDER_PIPELINE_URP
using UnityEngine.Rendering.HighDefinition;

namespace Kamgam.SettingsGenerator
{
    public partial class CameraSSGIToggleConnection : Connection<bool>
    {

        public CameraSSGIToggleConnection()
        {
            HDRenderPipelineCameraUtils.EnableCustomFrameProperty(FrameSettingsField.SSGI);
        }

        public override bool Get()
        {
            return HDRenderPipelineCameraUtils.GetCameraCustomFramePropertyState(FrameSettingsField.SSGI);
        }

        public override void Set(bool enable)
        {
            HDRenderPipelineCameraUtils.SetCameraCustomFramePropertyState(FrameSettingsField.SSGI, enable);
            NotifyListenersIfChanged(enable);
        }
    }
}
#endif