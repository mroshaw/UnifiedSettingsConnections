using UnityEngine;

namespace Kamgam.SettingsGenerator
{
    [CreateAssetMenu(fileName = "CameraSSGIToggleConnection", menuName = "SettingsGenerator/Connection/UnifiedSettingsConnections/CameraSSGIToggleConnection", order = 1)]
    public class CameraSSGIToggleConnectionSO : BoolConnectionSO
    {
        protected CameraSSGIToggleConnection _connection;

        public override IConnection<bool> GetConnection()
        {
            if(_connection == null)
                Create();

            return _connection;
        }

        public void Create()
        {
            _connection = new CameraSSGIToggleConnection();
        }

        public override void DestroyConnection()
        {
            if (_connection != null)
                _connection.Destroy();

            _connection = null;
        }
    }
}
