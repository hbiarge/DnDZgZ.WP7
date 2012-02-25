namespace Arosbi.DnDZgZ.UI
{
    using Arosbi.DnDZgZ.UI.ViewModel;

    using Microsoft.Phone.Controls;

    /// <summary>
    /// Description for BusesPage.
    /// </summary>
    public partial class BusesPage : PhoneApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the BusesPage class.
        /// </summary>
        public BusesPage()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButtonClick(object sender, System.EventArgs e)
        {
            var vm = DataContext as BusesViewModel;

            if (vm == null)
            {
                return;
            }

            vm.CurrentLocationCommand.Execute(null);
        }
    }
}