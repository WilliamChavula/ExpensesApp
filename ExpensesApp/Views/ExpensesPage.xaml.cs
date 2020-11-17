using System;
using System.Collections.Generic;
using ExpensesApp.ViewModels;
using Xamarin.Forms;

namespace ExpensesApp.Views
{
    public partial class ExpensesPage : ContentPage
    {
        ExpensesViewModel viewModel;
        public ExpensesPage()
        {
            InitializeComponent();
            viewModel = Resources["vm"] as ExpensesViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.GetExpenses();
        }
    }
}
