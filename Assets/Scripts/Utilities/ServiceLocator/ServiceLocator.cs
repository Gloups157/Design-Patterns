using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Scripts.Utilities.ServiceLocator
{
    public class ServiceLocator : MonoBehaviour
    {
        static ServiceLocator global;
        static Dictionary<Scene, ServiceLocator> sceneContainerMap;
        static List<GameObject> tmpSceneGameObjects;

        readonly ServiceManager services = new();

        const string GLOBALSERVICELOCATOR = "Service Locator [Global]";
        const string SCENESERVICELOCATOR = "Service Locator [Scene]";

        public static ServiceLocator Global
        {
            get
            {
                if (global != null) return global;
                if (FindFirstObjectByType<ServiceLocatorGlobalBootstrap>() is { } found)
                {
                    found.BootstrapOnDemand();
                    return global;
                }
                GameObject container = new(GLOBALSERVICELOCATOR, typeof(ServiceLocator));
                container.AddComponent<ServiceLocatorGlobalBootstrap>().BootstrapOnDemand();
                return global;
            }
        }

        internal void ConfigureAsGlobal(bool dontDestroyOnLoad)
        {
            if (global == this) Debug.LogWarning("ServiceLocator.ConfigureAsGlobal() : Already configured as global", this);
            else if (global != null) Debug.LogError("ServiceLocator.ConfigureAsGlobal() : Another ServiceLocator is already configured as global", this);
            else
            {
                global = this;
                if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
            }
        }

        internal void ConfigureForScene()
        {
            var scene = gameObject.scene;
            if (sceneContainerMap.ContainsKey(scene))
            {
                Debug.LogError("ServiceLocator.ConfigureForScne() : Another ServiceLocator is already configured for this scene", this);
                return;
            }
            sceneContainerMap.Add(scene, this);
        }

        public static ServiceLocator For(MonoBehaviour monoBehaviour) => monoBehaviour.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(monoBehaviour) ?? Global;

        public static ServiceLocator ForSceneOf(MonoBehaviour monoBehaviour)
        {
            Scene scene = monoBehaviour.gameObject.scene;
            if (sceneContainerMap.TryGetValue(scene, out ServiceLocator container) && container != monoBehaviour) return container;
            tmpSceneGameObjects.Clear();
            scene.GetRootGameObjects(tmpSceneGameObjects);
            foreach(var gameObject in tmpSceneGameObjects.Where(gameObject => gameObject.GetComponent<ServiceLocatorSceneBootstarp>() != null))
            {
                if (gameObject.TryGetComponent(out ServiceLocatorSceneBootstarp bootstrap) && bootstrap.Container != monoBehaviour)
                {
                    bootstrap.BootstrapOnDemand();
                    return bootstrap.Container;
                }
            }
            return Global;
        }

        public ServiceLocator Register<T>(T service)
        {
            services.Register(service);
            return this;
        }

        public ServiceLocator Register(Type type, object service)
        {
            services.Register(type, service);
            return this;
        }

        public ServiceLocator Get<T>(out T service) where T : class
        {
            if (TryGetService(out service)) return this;
            if (TryGetNextInHierarchy(out ServiceLocator container))
            {
                container.Get(out service);
                return this;
            }
            throw new ArgumentException($"ServiceLocator.Get() : Service of type {typeof(T).FullName} not registered");
        }

        bool TryGetService<T>(out T service) where T : class => services.TryGet(out service);

        bool TryGetNextInHierarchy(out ServiceLocator container)
        {
            if (this == global)
            {
                container = null;
                return false;
            }
            container = transform.parent.OrNull()?.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(this);
            return container != null;
        }

        void OnDestroy()
        {
            if (this == global) global = null;
            else if (sceneContainerMap.ContainsValue(this)) sceneContainerMap.Remove(gameObject.scene);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetStatics()
        {
            global = null;
            sceneContainerMap = new();
            tmpSceneGameObjects = new();
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/Service Locator/Add Global")]
        static void AddGlobal()
        {
            GameObject gameObject = new(GLOBALSERVICELOCATOR, typeof(ServiceLocatorGlobalBootstrap));
        }

        [MenuItem("GameObject/Service Locator/Add Scene")]
        static void AddScene()
        {
            GameObject gameObject = new(SCENESERVICELOCATOR, typeof(ServiceLocatorSceneBootstarp));
        }
#endif
    }
}