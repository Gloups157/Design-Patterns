using UnityEngine;

namespace Scripts.Utilities.ServiceLocator
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ServiceLocator))]
    public abstract class ServiceLocatorBootstrap : MonoBehaviour
    {
        ServiceLocator container;
        internal ServiceLocator Container => container.OrNull() ?? (container = GetComponent<ServiceLocator>());

        bool hasBeenBoostrapped;

        void Awake() => BootstrapOnDemand();

        public void BootstrapOnDemand()
        {
            if (hasBeenBoostrapped) return;
            hasBeenBoostrapped = true;
            Bootstrap();
        }

        protected abstract void Bootstrap();
    }
}