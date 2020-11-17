using System;
using System.Collections.ObjectModel;
using ExpensesApp.Models;
using ExpensesApp.Views;
using Xamarin.Forms;

namespace ExpensesApp.ViewModels
{
    public class ExpensesViewModel
    {
        public ObservableCollection<Expense> Expenses
        {
            get;
            set;
        }

        public Command AddExpenseCommand
        {
            get;
            set;
        }

        public ExpensesViewModel()
        {
            Expenses = new ObservableCollection<Expense>();
            GetExpenses();
            AddExpenseCommand = new Command(AddExpense);
        }

        public void GetExpenses()
        {
            var expenses = Expense.GetExpenses();

            Expenses.Clear();

            foreach (var expense in expenses)
            {
                Expenses.Add(expense);
            }
        }

        public void AddExpense()
        {
            Application.Current.MainPage.Navigation.PushAsync(new NewExpensePage());
        }
    }
}
