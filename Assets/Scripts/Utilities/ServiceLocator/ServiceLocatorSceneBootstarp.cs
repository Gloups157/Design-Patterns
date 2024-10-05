using UnityEngine;

namespace Scripts.Utilities.ServiceLocator
{
    [AddComponentMenu("Service Locator/Scene")]
    public class ServiceLocatorSceneBootstarp : ServiceLocatorBootstrap
    {
        protected override void Bootstrap()
        {
            Container.ConfigureForScene();
        }
    }
}