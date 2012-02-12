/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Arosbi.DnDZgZ.UI.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
  
  OR (WPF only):
  
  xmlns:vm="clr-namespace:Arosbi.DnDZgZ.UI.ViewModel"
  DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
*/

namespace Arosbi.DnDZgZ.UI.ViewModel
{
    using System;

    using Arosbi.DnDZgZ.UI.Services;

    using GalaSoft.MvvmLight;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
    /// to this locator.
    /// </para>
    /// <para>
    /// In Silverlight and WPF, place the ViewModelLocatorTemplate in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Arosbi.DnDZgZ.UI.ViewModel"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// You can also use Blend to do all this with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// <para>
    /// In <strong>*WPF only*</strong> (and if databinding in Blend is not relevant), you can delete
    /// the Main property and bind to the ViewModelNameStatic property instead:
    /// </para>
    /// <code>
    /// xmlns:vm="clr-namespace:Arosbi.DnDZgZ.UI.ViewModel"
    /// DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
    /// </code>
    /// </summary>
    public class ViewModelLocator
    {
        public static readonly Uri BusesPage = new Uri("/BusesPage.xaml", UriKind.Relative);
        public const string BizisPage = "";
        public const string WifiPage = "";

        private static MainViewModel main;
        private static BusesViewModel buses;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view models
            }
            else
            {
                // Create run time view models
            }

            CreateMain();
        }

        #region Main
        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static MainViewModel MainStatic
        {
            get
            {
                if (main == null)
                {
                    CreateMain();
                }

                return main;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return MainStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearMain()
        {
            main.Cleanup();
            main = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateMain()
        {
            if (main == null)
            {
                main = IoC.Resolve<MainViewModel>();
            }
        }

        #endregion

        #region Buses

        /// <summary>
        /// Gets the $ViewModelPropertyName$ property.
        /// </summary>
        public static BusesViewModel BusesStatic
        {
            get
            {
                if (buses == null)
                {
                    CreateBuses();
                }

                return buses;
            }
        }

        /// <summary>
        /// Gets the Buses property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public BusesViewModel Buses
        {
            get
            {
                return BusesStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the $ViewModelPropertyName$ property.
        /// </summary>
        public static void ClearBuses()
        {
            buses.Cleanup();
            buses = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the $ViewModelPropertyName$ property.
        /// </summary>
        public static void CreateBuses()
        {
            if (buses == null)
            {
                buses = IoC.Resolve<BusesViewModel>();
            }
        }

        #endregion

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            ClearMain();
            ClearBuses();
        }
    }
}