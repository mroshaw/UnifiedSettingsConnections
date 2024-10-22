using UnityEngine;
using UnityEngine.Audio;

namespace Kamgam.SettingsGenerator
{
    [CreateAssetMenu(fileName = "GpuInstancerConnection", menuName = "SettingsGenerator/Connection/UnifiedSettingsConnections/GpuInstancerConnection", order = 1)]
    public class GPUInstancerConnectionSO : OptionConnectionSO
    {
        protected GPUInstancerConnection _connection;

        public override IConnectionWithOptions<string> GetConnection()
        {
            if (_connection == null)
                Create();

            return _connection;
        }

        public void Create()
        {
            _connection = new GPUInstancerConnection();
        }

        public override void DestroyConnection()
        {
            if (_connection != null)
                _connection.Destroy();

            _connection = null;
        }
    }
}