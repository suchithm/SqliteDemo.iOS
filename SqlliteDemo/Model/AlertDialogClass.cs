using System;
using UIKit;
using System.Threading.Tasks;

namespace SqliteDemo
{
	public static class AlertDialogClass
	{
		internal static void FnShowAlertDialog(string strTitlt,string strMessage,string strOkButtonText)
		{ 
			var alertView = new UIAlertView ();
			alertView.Title = strTitlt;
			alertView.Message = strMessage;
			alertView.AddButton ( strOkButtonText );
			alertView.Show ();
		} 
	}

	public class ButtonedAlertClass
	{
		UIAlertView alertView;
		internal  Task<int> FnTwoButtonedAlertDialog(string strTitle,string strMessage,string strNegativeButton,string strPositiveButton)
		{
			var taskCompletionSource = new TaskCompletionSource<int> ();
			alertView =alertView ?? new UIAlertView ();
			alertView.Title = strTitle;
			alertView.Message = strMessage;
			alertView.AddButton ( strNegativeButton);
			alertView.AddButton (strPositiveButton );

			alertView.Clicked += ( (sender , e ) => taskCompletionSource.TrySetResult ( Convert.ToInt32 ( e.ButtonIndex.ToString () ) ) );
			alertView.Show ();

			Console.WriteLine ( taskCompletionSource.Task.ToString() );
			return taskCompletionSource.Task;
 
		}
	}
}

