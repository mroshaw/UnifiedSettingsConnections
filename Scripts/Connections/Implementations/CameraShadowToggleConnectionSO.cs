using UnityEngine;

namespace Kamgam.SettingsGenerator
{
    [CreateAssetMenu(fileName = "CameraShadowToggleConnection", menuName = "SettingsGenerator/Connection/UnifiedSettingsConnections/CameraShadowToggleConnection", order = 1)]
    public class CameraShadowToggleConnectionSO : BoolConnectionSO
    {
        protected CameraShadowToggleConnection _connection;

        public override IConnection<bool> GetConnection()
        {
            if(_connection == null)
                Create();

            return _connection;
        }

        public void Create()
        {
            _connection = new CameraShadowToggleConnection();
        }

        public override void DestroyConnection()
        {
            if (_connection != null)
                _connection.Destroy();

            _connection = null;
        }
    }
}
