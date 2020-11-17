using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ExpensesApp.Models;
using Xamarin.Forms;

namespace ExpensesApp.ViewModels
{
    public class NewExpenseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string expenseName;
        public string ExpenseName
        {
            get => expenseName;
            set
            {
                expenseName = value;
                OnPropertyChanged("ExpenseName");
            }
        }
        private string expenseDescription;
        public string ExpenseDescription
        {
            get => expenseDescription;
            set
            {
                expenseDescription = value;
                OnPropertyChanged("ExpenseDescription");
            }
        }

        private float expenseAmount;
        public float ExpenseAmount
        {
            get => expenseAmount;
            set
            {
                expenseAmount = value;
                OnPropertyChanged("ExpenseAmount");
            }
        }

        private DateTime expenseDate;
        public DateTime ExpenseDate
        {
            get => expenseDate;
            set
            {
                expenseDate = value;
                OnPropertyChanged("ExpenseDate");
            }
        }

        private string expenseCategory;
        public string ExpenseCategory
        {
            get => expenseCategory;
            set
            {
                expenseCategory = value;
                OnPropertyChanged("ExpenseCategory");
            }
        }

        public Command SaveExpenseCommand { get; set; }

        public ObservableCollection<string> Categories { get; set; }

        public NewExpenseViewModel()
        {
            Categories = new ObservableCollection<string>();
            ExpenseDate = DateTime.Today;
            SaveExpenseCommand = new Command(InsertExpense);
            GetCategories();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InsertExpense()
        {
            Expense expense = new Expense()
            {
                Name = ExpenseName,
                Amount = ExpenseAmount,
                Description = ExpenseDescription,
                Date = ExpenseDate,
                Category = ExpenseCategory
            };

            int response = Expense.InsertExpense(expense);

            if (response > 0)
                Application.Current.MainPage.Navigation.PopAsync();
            else
                Application.Current.MainPage.DisplayAlert("Error", "No items were inserted", "Ok");
        }

        private void GetCategories()
        {
            Categories.Clear();

            Categories.Add("Housing");
            Categories.Add("Debt");
            Categories.Add("Health");
            Categories.Add("Food");
            Categories.Add("Personal");
            Categories.Add("Travel");
            Categories.Add("Other");
        }
    }
}
