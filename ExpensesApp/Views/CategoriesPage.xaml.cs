using System;
using System.Collections.Generic;
using ExpensesApp.ViewModels;
using Xamarin.Forms;

namespace ExpensesApp.Views
{
    public partial class CategoriesPage : ContentPage
    {

        CategoriesViewModel viewModel;
        public CategoriesPage()
        {
            InitializeComponent();
            viewModel = Resources["vm"] as CategoriesViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.GetExpensesPerCategory();
        }
    }
}
