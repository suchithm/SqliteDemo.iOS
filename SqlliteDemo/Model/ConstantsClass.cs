using System;

namespace SqliteDemo
{
	public static class ConstantsClass
	{ 
		internal readonly static string strDbFolderPath = Environment.GetFolderPath ( Environment.SpecialFolder.Personal );
		internal const string strAppName="SqliteAsyncDemo";
		internal const string strDatabaseName="dbContactList.db";
		internal const string strLoadingMessage="Loading..."; 
		internal readonly static string strExceptionMessage="Opps! Unable to process, Please recheck previous input";
		internal const string strOkButtonText="Ok";
		internal const string strDeleteConfirmationText="Are you sure you want delete?";
		internal const string strPositiveBtnText="Proceed";
		internal const string strNegativeBtnText="Cancel";
		internal const string strMandatoryFields="Please Fill the details";
		internal const string strValidMobileNubmber="Please Enter valid mobile number";
	}
}

