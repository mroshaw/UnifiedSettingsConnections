using UnityEngine;
using UnityEngine.Audio;

namespace Kamgam.SettingsGenerator
{
    [CreateAssetMenu(fileName = "UnityDLSSConnection", menuName = "SettingsGenerator/Connection/UnifiedSettingsPlus/UnityDLSSConnection", order = 1)]
    public class UnityDLSSConnectionSO : OptionConnectionSO
    {
        protected UnityDLSSConnection _connection;

        public override IConnectionWithOptions<string> GetConnection()
        {
            if (_connection == null)
                Create();

            return _connection;
        }

        public void Create()
        {
            _connection = new UnityDLSSConnection();
        }

        public override void DestroyConnection()
        {
            if (_connection != null)
                _connection.Destroy();

            _connection = null;
        }
    }
}