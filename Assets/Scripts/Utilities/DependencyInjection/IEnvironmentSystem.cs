namespace Scripts.Utilities.DependencyInjection
{
    /// <summary>
    /// Interface representing an environment system that can be provided and initialized.
    /// </summary>
    public interface IEnvironmentSystem
    {
        /// <summary>
        /// Provides an instance of the environment system.
        /// </summary>
        /// <returns>An instance of the <see cref="IEnvironmentSystem"/>.</returns>
        IEnvironmentSystem ProvideEnvironmentSystem();

        /// <summary>
        /// Initializes the environment system.
        /// </summary>
        void Initialize();
    }
}