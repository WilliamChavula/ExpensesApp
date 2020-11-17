using System;
using System.Collections.ObjectModel;
using ExpensesApp.Models;
using System.Linq;
using Xamarin.Forms;
using ExpensesApp.Interfaces;
using PCLStorage;
using System.IO;

namespace ExpensesApp.ViewModels
{
    public class CategoriesViewModel
    {
        public ObservableCollection<string> Categories
        {
            get;
            set;
        }

        public Command ExportCommand { get; set; }

        public ObservableCollection<CategoryExpenses> CategoryExpensesCollection { get; set; }

        public CategoriesViewModel()
        {

            CategoryExpensesCollection = new ObservableCollection<CategoryExpenses>();
            Categories = new ObservableCollection<string>();
            ExportCommand = new Command(ShareReport);

            GetCategories();
            GetExpensesPerCategory();
        }

        public void GetExpensesPerCategory()
        {
            CategoryExpensesCollection.Clear();
            float totalAmount = Expense.TotalExpensesAmount();
            foreach (string cat in Categories)
            {
                var expenses = Expense.GetExpenses(cat);
                float expensesAmountInCategory = expenses.Sum(e => e.Amount);

                CategoryExpenses ce = new CategoryExpenses()
                {
                    Category = cat,
                    ExpensesPercentage = (expensesAmountInCategory / totalAmount)
                };

                CategoryExpensesCollection.Add(ce);
            }
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

        public class CategoryExpenses
        {
            public string Category { get; set; }
            public float ExpensesPercentage { get; set; }


        }

        public async void ShareReport()
        {
            IFileSystem fileSystem = FileSystem.Current;
            IFolder rootFolder = fileSystem.LocalStorage;
            IFolder reportsFolder = await rootFolder.CreateFolderAsync("reports", CreationCollisionOption.OpenIfExists);

            var txtFile = await reportsFolder.CreateFileAsync("report.txt", CreationCollisionOption.ReplaceExisting);

            using (StreamWriter streamWriter = new StreamWriter(txtFile.Path))
            {
                foreach (var ce in CategoryExpensesCollection)
                {
                    streamWriter.WriteLine($"{ce.Category} - {ce.ExpensesPercentage:p}");
                }
            }

            IShare shareDependency = DependencyService.Get<IShare>();
            await shareDependency.Show("Expense Report", "Here is your expense report", txtFile.Path);
        }
    }
}
