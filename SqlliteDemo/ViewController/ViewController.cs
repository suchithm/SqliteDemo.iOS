using System; 
using SQLite; 
using System.Collections.Generic; 
using System.Threading.Tasks;
using Foundation;
using UIKit; 

namespace SqliteDemo
{
	[Foundation.Register ("ViewController")]
	public partial class ViewController : UIViewController
	{
		SQLiteAsyncConnection sqlAsyncConnection;
		PhoneContactClass objPhoneContactClass; 
		ContactListTableSource objContactListTableSource; 
		BusyIndicatorClass objBusyIndicator;
		public ViewController ( IntPtr handle ) : base ( handle )
		{
			 
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			FnTapEvents (); 
			FnInitializeView ();
		
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated); 
		}
		void FnTapEvents()
		{  
			txtSearchBar.SearchButtonClicked +=async delegate(object sender , EventArgs e )
			{
				if(!string.IsNullOrEmpty(txtSearchBar.Text))
				{
					FnStartActivityIndicator();
					var lstContactList= await FnGetContactList(txtSearchBar.Text);
					FnStopActivityIndicator();
					FnBindContactList(lstContactList);
				}
			};
			txtSearchBar.TextChanged +=async  delegate(object sender , UISearchBarTextChangedEventArgs e )
			{
				if(!string.IsNullOrEmpty(txtSearchBar.Text))
				{
					FnStartActivityIndicator();
				    var lstContactList= await FnGetContactList(txtSearchBar.Text);
					FnStopActivityIndicator();
					FnBindContactList(lstContactList);
				}
			};

			btnRefreshContactList.TouchUpInside +=async delegate(object sender , EventArgs e )
			{
				FnStartActivityIndicator(); 
				var contactList=await FnGetAllContactList (); 
				FnStopActivityIndicator();
				FnBindContactList (contactList);  
			};

		}
		async void FnInitializeView()
		{
			FnStartActivityIndicator ();
			sqlAsyncConnection =	DbConnectionClass.FnGetConnection (); 
			tableViewContactsList.Hidden=true; 
			await  sqlAsyncConnection.CreateTableAsync<PhoneContactClass> (); 
			var contactList=await FnGetAllContactList (); 
			FnStopActivityIndicator ();
			FnBindContactList (contactList);  
		
			btnAddContact.SetBackgroundImage ( UIImage.FromBundle ( "Images/iconAdd" ) , UIControlState.Normal ); 
			btnRefreshContactList.SetBackgroundImage ( UIImage.FromBundle ( "Images/iconRefreshImg" ) , UIControlState.Normal );
			tableViewContactsList.Layer.CornerRadius = 10; 
		}
	
		async Task<List<PhoneContactClass>> FnGetAllContactList()
		{     
			var	lstAllContact=await sqlAsyncConnection.QueryAsync<PhoneContactClass> ( "select Id,strContactName,strContactNumber from PhoneContactClass order by strContactName COLLATE NOCASE ASC"  );
			FnStopActivityIndicator ();
			return lstAllContact;
		}
		async Task<List<PhoneContactClass>>FnGetContactList(string str)
		{ 
			var lstContact =await  sqlAsyncConnection.Table<PhoneContactClass> ().Where ( v => v.strContactName.Contains ( str ) ).ToListAsync(); 
			return lstContact; 
		} 
		void FnBindContactList(List<PhoneContactClass> lstContactList)
		{ 
			if ( lstContactList != null )
			{
				if(lstContactList.Count>0)
				{
					if ( objContactListTableSource != null )
					{
						objContactListTableSource.ConatctRowSelectedEventAction -= FnContactSelected;
						objContactListTableSource = null;
					} 
					tableViewContactsList.Hidden=false;
					objContactListTableSource = new ContactListTableSource (lstContactList);
					objContactListTableSource.ConatctRowSelectedEventAction += FnContactSelected;

					tableViewContactsList.Source = objContactListTableSource;
					tableViewContactsList.ReloadData ();  
				}

			} 
		}
	
		void FnContactSelected(PhoneContactClass _objPhoneContactClass)
		{
			objPhoneContactClass = _objPhoneContactClass;
			PerformSegue ("EditContact",this);

		}
	
		void FnStartActivityIndicator()
		{
			objBusyIndicator =new BusyIndicatorClass(UIScreen.MainScreen.Bounds,ConstantsClass.strLoadingMessage);
			Add ( objBusyIndicator ); 
		}
		void FnStopActivityIndicator()
		{
			if ( objBusyIndicator != null )
			{
				objBusyIndicator.Hide();
				objBusyIndicator.RemoveFromSuperview();
				objBusyIndicator=null;
			} 
		}
		//adding new comment 
		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if ( segue.Identifier.Equals ( "EditContact" ) )
			{
				var ContatctEditViewController = segue.DestinationViewController as NewContactViewController;
				if ( ContatctEditViewController != null )
				{
					ContatctEditViewController.objPhoneContactClass = objPhoneContactClass;
				}
			}
		}
	}
}