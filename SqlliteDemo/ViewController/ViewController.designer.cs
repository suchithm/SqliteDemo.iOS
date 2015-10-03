// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SqliteDemo
{
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton btnAddContact { get; set; }

		[Outlet]
		UIKit.UIButton btnConnect { get; set; }

		[Outlet]
		UIKit.UIButton btnFetchRecord { get; set; }

		[Outlet]
		UIKit.UIButton btnInsert { get; set; }

		[Outlet]
		UIKit.UIButton btnRefreshContactList { get; set; }

		[Outlet]
		UIKit.UITableView tableViewContactsList { get; set; }

		[Outlet]
		UIKit.UITextView txtDisplayRec { get; set; }

		[Outlet]
		UIKit.UISearchBar txtSearchBar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAddContact != null) {
				btnAddContact.Dispose ();
				btnAddContact = null;
			}

			if (btnRefreshContactList != null) {
				btnRefreshContactList.Dispose ();
				btnRefreshContactList = null;
			}

			if (btnConnect != null) {
				btnConnect.Dispose ();
				btnConnect = null;
			}

			if (btnFetchRecord != null) {
				btnFetchRecord.Dispose ();
				btnFetchRecord = null;
			}

			if (btnInsert != null) {
				btnInsert.Dispose ();
				btnInsert = null;
			}

			if (tableViewContactsList != null) {
				tableViewContactsList.Dispose ();
				tableViewContactsList = null;
			}

			if (txtDisplayRec != null) {
				txtDisplayRec.Dispose ();
				txtDisplayRec = null;
			}

			if (txtSearchBar != null) {
				txtSearchBar.Dispose ();
				txtSearchBar = null;
			}
		}
	}
}
