using System;
using System.Threading.Tasks;
using ExpensesApp.Interfaces;
using ExpensesApp.iOS.Dependencies;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Share))]
namespace ExpensesApp.iOS.Dependencies
{
    public class Share : IShare
    {
        public Share()
        {
        }

        public async Task Show(string title, string message, string filePath)
        {
            var viewController = GetVisibleController();
            var items = new NSObject[] { NSObject.FromObject(title), NSUrl.FromFilename(filePath) };
            var activityController = new UIActivityViewController(items, null);

            if (activityController.PopoverPresentationController != null)
                activityController.PopoverPresentationController.SourceView = viewController.View;

            await viewController.PresentViewControllerAsync(activityController, true);
        }

        // UIView Controller is like a Page / View in Xamarin.Forms.
        private UIViewController GetVisibleController()
        {
            var rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            /* checking the type of page we get back;
            
             * Check if it is null
             * Check if it's a navigation page and get the top page in the stack
             * check if it is a Tab page and change to the selected one
             
            */
            if (rootViewController.PresentedViewController == null)
                return rootViewController;

            if (rootViewController.PresentedViewController is UINavigationController)
                return ((UINavigationController)rootViewController.PresentedViewController).TopViewController;

            if (rootViewController.PresentedViewController is UITabBarController)
                return ((UITabBarController)rootViewController.PresentedViewController).SelectedViewController;


            return rootViewController.PresentedViewController;
        }
    }
}
