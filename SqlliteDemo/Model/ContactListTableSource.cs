using System;
using UIKit;
using System.Collections.Generic;

namespace SqliteDemo
{
	public class ContactListTableSource : UITableViewSource
	{
		const string strCellIdentifier="Cell";
		readonly List<PhoneContactClass> lstContacts; 
		internal event Action<PhoneContactClass> ConatctRowSelectedEventAction; 

		public ContactListTableSource(List<PhoneContactClass> lstContact)
		{ 
			lstContacts=lstContact; 
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return lstContacts.Count;

		}

		public override UIView GetViewForFooter (UITableView tableView, nint section)
		{
			return new UIView();
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell ( strCellIdentifier ) ?? new UITableViewCell ( UITableViewCellStyle.Default , strCellIdentifier );
			cell.TextLabel.Text = lstContacts [indexPath.Row].strContactName;
			cell.TextLabel.Font = UIFont.SystemFontOfSize (12);
			return cell;
		}
		public override void RowSelected (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			if ( ConatctRowSelectedEventAction != null )
			{
				ConatctRowSelectedEventAction ( lstContacts[indexPath.Row] );
			}
			tableView.DeselectRow ( indexPath , true );
		}
	}

}

