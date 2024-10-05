using System;

namespace Scripts.Utilities.EventBus
{
    /// <summary>
    /// Represents a binding for events that allows for the registration and deregistration of event handlers.
    /// </summary>
    /// <typeparam name="T">The type of the event that implements the <see cref="IEvent"/> interface.</typeparam>
    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        /// <summary>
        /// Event handler that is triggered when the event occurs.
        /// </summary>
        public Action<T> onEvent = _ => { };

        /// <summary>
        /// Event handler that is triggered when the event occurs without any arguments.
        /// </summary>
        public Action onEventNoArgs = () => { };

        /// <summary>
        /// Gets or sets the event handler that is triggered when the event occurs.
        /// </summary>
        Action<T> IEventBinding<T>.OnEvent
        {
            get => onEvent;
            set => onEvent = value;
        }

        /// <summary>
        /// Gets or sets the event handler that is triggered when the event occurs without any arguments.
        /// </summary>
        Action IEventBinding<T>.OnEventNoArgs
        {
            get => onEventNoArgs;
            set => onEventNoArgs = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventBinding{T}"/> class with the specified event handler.
        /// </summary>
        /// <param name="onEvent">The event handler to be triggered when the event occurs.</param>
        public EventBinding(Action<T> onEvent) => this.onEvent = onEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventBinding{T}"/> class with the specified event handler that takes no arguments.
        /// </summary>
        /// <param name="onEventNoArgs">The event handler to be triggered when the event occurs without any arguments.</param>
        public EventBinding(Action onEventNoArgs) => this.onEventNoArgs = onEventNoArgs;

        /// <summary>
        /// Adds a new event handler to be triggered when the event occurs.
        /// </summary>
        /// <param name="onEvent">The event handler to be added.</param>
        public void Add(Action<T> onEvent) => this.onEvent += onEvent;

        /// <summary>
        /// Removes an existing event handler.
        /// </summary>
        /// <param name="onEvent">The event handler to be removed.</param>
        public void Remove(Action<T> onEvent) => this.onEvent -= onEvent;

        /// <summary>
        /// Adds a new event handler to be triggered when the event occurs without any arguments.
        /// </summary>
        /// <param name="onEventNoArgs">The event handler to be added.</param>
        public void Add(Action onEventNoArgs) => this.onEventNoArgs += onEventNoArgs;

        /// <summary>
        /// Removes an existing event handler that takes no arguments.
        /// </summary>
        /// <param name="onEventNoArgs">The event handler to be removed.</param>
        public void Remove(Action onEventNoArgs) => this.onEventNoArgs -= onEventNoArgs;
    }
}