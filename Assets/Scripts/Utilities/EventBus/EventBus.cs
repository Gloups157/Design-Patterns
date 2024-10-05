using System.Collections.Generic;

namespace Scripts.Utilities.EventBus
{
    /// <summary>
    /// A static class that manages event bindings and allows events to be raised and handled.
    /// </summary>
    /// <typeparam name="T">The type of the event that implements the <see cref="IEvent"/> interface.</typeparam>
    public static class EventBus<T> where T : IEvent
    {
        /// <summary>
        /// A set containing all registered event bindings.
        /// </summary>
        static readonly HashSet<IEventBinding<T>> bindingSet = new();

        /// <summary>
        /// Registers an event binding to the event bus.
        /// </summary>
        /// <param name="eventBinding">The event binding to be registered.</param>
        public static void Register(EventBinding<T> eventBinding) => bindingSet.Add(eventBinding);

        /// <summary>
        /// Deregisters an event binding from the event bus.
        /// </summary>
        /// <param name="eventBinding">The event binding to be deregistered.</param>
        public static void Deregister(EventBinding<T> eventBinding) => bindingSet.Remove(eventBinding);

        /// <summary>
        /// Raises an event, invoking all registered event handlers.
        /// </summary>
        /// <param name="event">The event to be raised.</param>
        public static void Raise(T @event)
        {
            foreach (var binding in bindingSet)
            {
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
        }

        /// <summary>
        /// Clears all registered event bindings from the event bus.
        /// </summary>
        static void Clear() => bindingSet.Clear();
    }
}