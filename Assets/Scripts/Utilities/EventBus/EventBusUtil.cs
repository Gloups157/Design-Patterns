using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace Scripts.Utilities.EventBus
{
    /// <summary>
    /// A utility class for managing and interacting with event buses within the Unity environment.
    /// </summary>
    public static class EventBusUtil
    {
        /// <summary>
        /// Gets or sets a read-only list of all event types implementing the <see cref="IEvent"/> interface.
        /// </summary>
        public static IReadOnlyList<Type> EventTypes { get; set; }

        /// <summary>
        /// Gets or sets a read-only list of all event bus types.
        /// </summary>
        public static IReadOnlyList<Type> EventBusTypes { get; set; }

#if UNITY_EDITOR
        /// <summary>
        /// Gets or sets the current play mode state in the Unity Editor.
        /// </summary>
        public static PlayModeStateChange PlayModeState { get; set; }

        /// <summary>
        /// Initializes the event bus utility when the Unity Editor loads.
        /// </summary>
        [InitializeOnLoadMethod]
        public static void InitializeEditor()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        /// <summary>
        /// Handles changes in the Unity Editor's play mode state.
        /// </summary>
        /// <param name="state">The current play mode state.</param>
        static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            PlayModeState = state;
            if (PlayModeState == PlayModeStateChange.ExitingPlayMode) ClearAllBuses();
        }
#endif

        /// <summary>
        /// Initializes the event bus utility before any scene loads in a runtime environment.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            EventTypes = PredefinedAssemblyUtil.GetTypes(typeof(IEvent));
            EventBusTypes = InitializeAllBuses();
        }

        /// <summary>
        /// Initializes all event buses for each event type.
        /// </summary>
        /// <returns>A list of event bus types initialized for each event type.</returns>
        static List<Type> InitializeAllBuses()
        {
            List<Type> eventBusTypeList = new();
            var typedef = typeof(EventBus<>);
            foreach (var eventType in EventTypes)
            {
                var busType = typedef.MakeGenericType(eventType);
                eventBusTypeList.Add(busType);
            }
            return eventBusTypeList;
        }

        /// <summary>
        /// Clears all registered event buses by invoking their internal clear methods.
        /// </summary>
        public static void ClearAllBuses()
        {
            const string CLEAR = "Clear";
            const BindingFlags BINDINGFLAGS = BindingFlags.Static | BindingFlags.NonPublic;
            for (var i = 0; i < EventTypes.Count; i++)
            {
                var busType = EventBusTypes[i];
                var clearMethod = busType.GetMethod(CLEAR, BINDINGFLAGS);
                clearMethod.Invoke(null, null);
            }
        }
    }
}