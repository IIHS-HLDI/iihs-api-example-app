using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

namespace IIHSApiApp.Framework
{
    public static class DialogHelper
    {

        public static ProgressDialog ShowDownloadingMessage(Context context)
        {
            ProgressDialog progressBar = new ProgressDialog(context);
            progressBar.SetCancelable(false);
            progressBar.SetMessage("Downloading Resources...");
            progressBar.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressBar.Show();
            return progressBar;
        }

        public static void ShowDownloadingMessage(Context context, Task task)
        {
            ProgressDialog progressBar = new ProgressDialog(context);
            progressBar.SetCancelable(false);
            progressBar.SetMessage("Downloading Resources...");
            progressBar.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressBar.Show();
            task.ContinueWith((t) => {
                progressBar.Hide();
            });
        }

        public static void ShowErrorMesage(Context context, Exception e)
        {
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(context);
            alertDialog.SetTitle("Error");
            alertDialog.SetMessage(e.Message ?? "Unhelpful error message");
            alertDialog.SetNeutralButton("OK", delegate
            {
                alertDialog.Dispose();
            });
            alertDialog.Show();
        }

        public static void ShowErrorMesage(Context context, string message)
        {
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(context);
            alertDialog.SetTitle("Error");
            alertDialog.SetMessage(message);
            alertDialog.SetNeutralButton("OK", delegate
            {
                alertDialog.Dispose();
            });
            alertDialog.Show();
        }
    }
}