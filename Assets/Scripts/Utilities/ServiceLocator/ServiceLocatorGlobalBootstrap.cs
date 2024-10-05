using UnityEngine;

namespace Scripts.Utilities.ServiceLocator
{
    [AddComponentMenu("Service Locator/Global")]
    public class ServiceLocatorGlobalBootstrap : ServiceLocatorBootstrap
    {
        [SerializeField] bool dontDestroyOnLoad = true;

        protected override void Bootstrap()
        {
            Container.ConfigureAsGlobal(dontDestroyOnLoad);
        }
    }
}