using System;

namespace Scripts.Utilities.EventBus
{
    /// <summary>
    /// Defines the binding interface for handling events in the event bus system.
    /// </summary>
    /// <typeparam name="T">The type of the event that implements the <see cref="IEvent"/> interface.</typeparam>
    internal interface IEventBinding<T>
    {
        /// <summary>
        /// Gets or sets the event handler that is triggered when the event occurs.
        /// </summary>
        Action<T> OnEvent { get; set; }

        /// <summary>
        /// Gets or sets the event handler that is triggered when the event occurs without any arguments.
        /// </summary>
        Action OnEventNoArgs { get; set; }
    }
}