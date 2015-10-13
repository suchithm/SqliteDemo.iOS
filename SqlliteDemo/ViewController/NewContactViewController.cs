using System; 
using SQLite; 
using System.Threading.Tasks;
using UIKit; 

namespace SqliteDemo
{
	public partial class NewContactViewController : UIViewController
	{ 
		SQLiteAsyncConnection sqlAsyncConnection;
		SQLiteConnection sqliteConnection;
		PhoneContactClass objPhoneContactLocalClass;
		internal PhoneContactClass objPhoneContactClass{ get; set;}
		public NewContactViewController (IntPtr handle) : base (handle)
		{
			
		} 
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			FnViewInitialize();
			FnTapEvents();
		}

		void FnViewInitialize()
		{
			sqlAsyncConnection =	DbConnectionClass.FnGetConnection (); 
			sqliteConnection = DbConnectionClass.FnGetNonAsyncConnection ();
		    sqlAsyncConnection.CreateTableAsync<PhoneContactClass> ();
			btnDeleteContact.Hidden = true;

			if ( objPhoneContactClass != null )
			{
				txtContactName.Text = objPhoneContactClass.strContactName;
				txtContactNumber.Text = objPhoneContactClass.strContactNumber.ToString();
				btnDeleteContact.Hidden = false;
			}

			txtContactName.ShouldReturn += ( (textField ) => textField.ResignFirstResponder () );
			txtContactNumber.ShouldReturn += ( (textField ) => textField.ResignFirstResponder () );

			viewFieldContainer.Layer.MasksToBounds = false;
			viewFieldContainer.Layer.CornerRadius = 10;
			viewFieldContainer.Layer.ShadowColor = UIColor.DarkGray.CGColor;
			viewFieldContainer.Layer.ShadowOpacity = 1.0f;
			viewFieldContainer.Layer.ShadowRadius = 6.0f;
			viewFieldContainer.Layer.ShadowOffset = new System.Drawing.SizeF(0f, 3f);

			txtContactName.Layer.CornerRadius=5;
			txtContactNumber.Layer.CornerRadius = 5;
			btnDeleteContact.Layer.CornerRadius = 5;
				
		}

		void FnTapEvents()
		{
			btnDone.TouchUpInside +=async delegate(object sender , EventArgs e )
			{
				if ( objPhoneContactClass == null )
				{
					await FnInsertRecord();
				}
				else
				{ 
					objPhoneContactClass.strContactName = txtContactName.Text;
					objPhoneContactClass.strContactNumber = Convert.ToInt64 ( txtContactNumber.Text );
					int intRows = sqliteConnection.Update ( objPhoneContactClass );
					if ( intRows != 0 ) 
						FnCancel (); 
					objPhoneContactClass=null;
				}
			};

			btnCancel.TouchUpInside += delegate(object sender , EventArgs e )
			{
				DismissViewController(true,null);
			}; 

			btnDeleteContact.TouchUpInside +=async delegate(object sender , EventArgs e )
			{
				int intRows= await sqlAsyncConnection.DeleteAsync (objPhoneContactClass);
				if(intRows!=0)
					FnCancel(); 
			};
		}
	
		void FnFetchRecord()
		{
			var tableContact = sqlAsyncConnection.Table<PhoneContactClass> ();
			tableContact.ToListAsync ().ContinueWith (t=>{
				foreach(var contact in t.Result)
				{
					Console.WriteLine("contact name"+ contact.strContactName+  " number : "+ contact.strContactNumber); 
				} 
			}); 
		}
		async Task<int>FnInsertRecord()
		{
			objPhoneContactLocalClass = new PhoneContactClass ();
			objPhoneContactLocalClass.strContactName = txtContactName.Text;
			objPhoneContactLocalClass.strContactNumber =Convert.ToInt64( txtContactNumber.Text);
			int intRow=await sqlAsyncConnection.InsertAsync ( objPhoneContactLocalClass ); 
			FnFetchRecord();
			FnCancel ();
			objPhoneContactLocalClass=null;
			return intRow;
		}
		void FnCancel()
		{
			txtContactName.Text=string.Empty;
			txtContactNumber.Text=string.Empty;
			DismissViewController (true,null);
		}
		async Task<int> FnUpdateRecord()
		{
			int intRow= await sqlAsyncConnection.ExecuteAsync (string.Format( "update PhoneContactClass set strContactName={0},strContactNumber={1} where Id={2}",objPhoneContactClass.strContactName,objPhoneContactClass.strContactNumber, objPhoneContactClass.Id),null);
			return intRow;

		}
	}
}
