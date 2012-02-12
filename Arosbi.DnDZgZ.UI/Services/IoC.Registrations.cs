namespace Arosbi.DnDZgZ.UI.Services
{
    using Arosbi.DnDZgZ.UI.ViewModel;

    public static partial class IoC
    {
        static partial void RegisterAll()
        {
            // Registramos los view models
            Register<MainViewModel>();
            Register<BusesViewModel>();

            // Registramos servicios
            Register<INavigationService, NavigationService>();
            Register<JsonSerializer>();

            // Registramos repositorio
            Register<IRepository, Fakes.FakeRepository>();
        }
    }
}