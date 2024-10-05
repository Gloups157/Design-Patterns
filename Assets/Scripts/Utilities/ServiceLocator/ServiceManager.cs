using UnityEngine;
using System.Collections.Generic;
using System;

namespace Scripts.Utilities.ServiceLocator
{
    public class ServiceManager : MonoBehaviour
    {
        readonly Dictionary<Type, object> serviceMap = new();
        public IEnumerable<object> RegisteredServices => serviceMap.Values;

        public bool TryGet<T>(out T service) where T : class
        {
            Type type = typeof(T);
            if (serviceMap.TryGetValue(type, out object serviceObject))
            {
                service = serviceObject as T;
                return true;
            }
            service = null;
            return false;
        }

        public T Get<T>() where T : class
        {
            Type type = typeof(T);
            if (serviceMap.TryGetValue(type, out object service)) return service as T;
            throw new ArgumentException($"ServiceManager.Get() : Service of type {type.FullName} not registered");
        }

        public ServiceManager Register<T>(T service)
        {
            Type type = typeof(T);
            if (!serviceMap.TryAdd(type, service)) Debug.LogError($"ServiceManage.Register() : Service of type {type.FullName} already registered");
            return this;
        }

        public ServiceManager Register(Type type, object service)
        {
            if (!type.IsInstanceOfType(service)) throw new ArgumentException("Type of service does not match type of service interface", nameof(service));
            if (!serviceMap.TryAdd(type, service)) Debug.LogError($"ServiceManage.Register() : Service of type {type.FullName} already registered");
            return this;
        }
    }
}