using GalaSoft.MvvmLight;

namespace Arosbi.DnDZgZ.UI.ViewModel
{
    using System;

    using Arosbi.DnDZgZ.UI.Services;

    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class BusesViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        private readonly IRepository repository;

        /// <summary>
        /// Initializes a new instance of the BusesViewModel class.
        /// </summary>
        public BusesViewModel(INavigationService navigationService, IRepository repository)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }

            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.navigationService = navigationService;
            this.repository = repository;

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real": Connect to service, etc...
            ////}
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}
    }
}