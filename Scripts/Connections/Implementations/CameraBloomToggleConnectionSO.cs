using UnityEngine;

namespace Kamgam.SettingsGenerator
{
    [CreateAssetMenu(fileName = "CameraBloomToggleConnection", menuName = "SettingsGenerator/Connection/UnifiedSettingsConnections/CameraBloomToggleConnection", order = 1)]
    public class CameraBloomToggleConnectionSO : BoolConnectionSO
    {
        protected CameraBloomToggleConnection ToggleConnection;

        public override IConnection<bool> GetConnection()
        {
            if(ToggleConnection == null)
                Create();

            return ToggleConnection;
        }

        public void Create()
        {
            ToggleConnection = new CameraBloomToggleConnection();
        }

        public override void DestroyConnection()
        {
            if (ToggleConnection != null)
                ToggleConnection.Destroy();

            ToggleConnection = null;
        }
    }
}
