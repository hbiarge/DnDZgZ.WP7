// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.Registrations.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the IoC type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Arosbi.DnDZgZ.UI.Infrastructure
{
    using Arosbi.DnDZgZ.UI.Services;
    using Arosbi.DnDZgZ.UI.Services.Fakes;
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
            Register<IRepository, FakeRepository>();
        }
    }
}