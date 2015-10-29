using System; 
using SQLite; 
using System.Threading.Tasks;
using UIKit;  

namespace SqliteDemo
{
	public partial class NewContactViewController : UIViewController
	{ 
		SQLiteAsyncConnection sqlAsyncConnection; 
		PhoneContactClass objPhoneContactLocalClass;
		BusyIndicatorClass objBusyIndicator;
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
			FnViewCustomization ();
		}

		void FnTapEvents()
		{
			btnDone.TouchUpInside += async delegate(object sender , EventArgs e )
			{
				try
				{
					var strValidationMsg=FnFieldValidation();

					if(!string.IsNullOrEmpty( strValidationMsg))
					{
						AlertDialogClass.FnShowAlertDialog ( ConstantsClass.strAppName , strValidationMsg , ConstantsClass.strOkButtonText );
						return;
					} 
					if ( objPhoneContactClass == null )
					{
						FnStartActivityIndicator (); 
						int intRow=await FnInsertRecord ();
						FnStopActivityIndicator ();

						if ( intRow != 0 )
							FnCancel ();
						else
							AlertDialogClass.FnShowAlertDialog ( ConstantsClass.strAppName , ConstantsClass.strExceptionMessage , ConstantsClass.strOkButtonText ); 	 
					}
					else
					{    
						FnStartActivityIndicator ();  
						await FnUpdateRecord ();
						FnStopActivityIndicator (); 
						FnCancel ();  
					}
				}
				catch
				{
					FnStopActivityIndicator ();
					AlertDialogClass.FnShowAlertDialog ( ConstantsClass.strAppName , ConstantsClass.strExceptionMessage , ConstantsClass.strOkButtonText );
				}
			};

			btnCancel.TouchUpInside += delegate(object sender , EventArgs e )
			{
				DismissViewController(true,null);
			}; 

			btnDeleteContact.TouchUpInside += async delegate(object sender , EventArgs e )
			{
				try
				{
					ButtonedAlertClass objButtonedAlert =null;
					objButtonedAlert=new ButtonedAlertClass (); 
					int intButtonIndex = await objButtonedAlert.FnTwoButtonedAlertDialog ( ConstantsClass.strAppName ,ConstantsClass.strDeleteConfirmationText , ConstantsClass.strNegativeBtnText ,ConstantsClass.strPositiveBtnText );
					if ( intButtonIndex == 1 )
					{ 
						FnStartActivityIndicator(); 
						int intRows = await sqlAsyncConnection.DeleteAsync ( objPhoneContactClass );
						FnStopActivityIndicator();
						if ( intRows != 0 )
							FnCancel ();
						else
							AlertDialogClass.FnShowAlertDialog ( ConstantsClass.strAppName , ConstantsClass.strExceptionMessage , ConstantsClass.strOkButtonText ); 	
					}
				}
				catch(Exception e2)
				{
					FnStopActivityIndicator();
					Console.WriteLine(e2.Message);
					AlertDialogClass.FnShowAlertDialog ( ConstantsClass.strAppName , ConstantsClass.strExceptionMessage , ConstantsClass.strOkButtonText );
				}
			};
		}
	 
		async Task<int>FnInsertRecord()
		{
			objPhoneContactLocalClass = new PhoneContactClass ();
			objPhoneContactLocalClass.strContactName = txtContactName.Text;
			objPhoneContactLocalClass.strContactNumber =Convert.ToInt64( txtContactNumber.Text);
			int intRow=await sqlAsyncConnection.InsertAsync ( objPhoneContactLocalClass ); 

			objPhoneContactLocalClass=null;
			return intRow;
		}

		async Task<int> FnUpdateRecord()
		{ 
			objPhoneContactClass.strContactName = txtContactName.Text;
			objPhoneContactClass.strContactNumber = Convert.ToInt64 ( txtContactNumber.Text ); 
			string  strQry=string.Format( "update PhoneContactClass set strContactName='{0}',strContactNumber={1} where Id={2}",objPhoneContactClass.strContactName,objPhoneContactClass.strContactNumber,objPhoneContactClass.Id); 
			await sqlAsyncConnection.QueryAsync<PhoneContactClass> ( strQry );  
			return 0;
		}

		void FnCancel()
		{
			txtContactName.Text=string.Empty;
			txtContactNumber.Text=string.Empty;
			DismissViewController (true,null);
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
	 
		void FnViewCustomization()
		{
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

		string FnFieldValidation()
		{
			if ( string.IsNullOrEmpty ( txtContactName.Text ) || string.IsNullOrEmpty ( txtContactNumber.Text ) )
			{
				return ConstantsClass.strMandatoryFields;
			}
			else if (!(txtContactNumber.Text.Length > 8 && txtContactNumber.Text.Length < 12) )
			{
				return ConstantsClass.strValidMobileNubmber;
			}
			else
			{
				return string.Empty;
			}
		}
	}
}