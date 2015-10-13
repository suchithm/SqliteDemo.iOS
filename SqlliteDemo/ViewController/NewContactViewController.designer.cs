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
	[Register ("NewContactViewController")]
	partial class NewContactViewController
	{
		[Outlet]
		UIKit.UIButton btnCancel { get; set; }

		[Outlet]
		UIKit.UIButton btnDeleteContact { get; set; }

		[Outlet]
		UIKit.UIButton btnDone { get; set; }

		[Outlet]
		UIKit.UITextField txtContactName { get; set; }

		[Outlet]
		UIKit.UITextField txtContactNumber { get; set; }

		[Outlet]
		UIKit.UIView viewFieldContainer { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnCancel != null) {
				btnCancel.Dispose ();
				btnCancel = null;
			}

			if (btnDone != null) {
				btnDone.Dispose ();
				btnDone = null;
			}

			if (txtContactName != null) {
				txtContactName.Dispose ();
				txtContactName = null;
			}

			if (txtContactNumber != null) {
				txtContactNumber.Dispose ();
				txtContactNumber = null;
			}

			if (viewFieldContainer != null) {
				viewFieldContainer.Dispose ();
				viewFieldContainer = null;
			}

			if (btnDeleteContact != null) {
				btnDeleteContact.Dispose ();
				btnDeleteContact = null;
			}
		}
	}
}
