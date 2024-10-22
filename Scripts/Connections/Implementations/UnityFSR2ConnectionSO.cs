using UnityEngine;
using UnityEngine.Audio;

namespace Kamgam.SettingsGenerator
{
    [CreateAssetMenu(fileName = "UnityFSR2Connection", menuName = "SettingsGenerator/Connection/UnifiedSettingsConnections/UnityFSR2Connection", order = 1)]
    public class UnityFSR2ConnectionSO : OptionConnectionSO
    {
        protected UnityFSR2Connection _connection;

        public override IConnectionWithOptions<string> GetConnection()
        {
            if (_connection == null)
                Create();

            return _connection;
        }

        public void Create()
        {
            _connection = new UnityFSR2Connection();
        }

        public override void DestroyConnection()
        {
            if (_connection != null)
                _connection.Destroy();

            _connection = null;
        }
    }
}