using System; 
using SQLite;

namespace SqliteDemo
{
	public static class DbConnectionClass
	{
		static SQLiteAsyncConnection sqliteAsyncConnection;  
		public static SQLiteAsyncConnection FnGetConnection()
		{
			
			if ( sqliteAsyncConnection == null )
				sqliteAsyncConnection = new SQLiteAsyncConnection (System.IO.Path.Combine(ConstantsClass.strDbFolderPath,ConstantsClass.strDatabaseName ));
		
			return sqliteAsyncConnection; 
		} 

		static SQLiteConnection sqliteConnection;
		public static SQLiteConnection FnGetNonAsyncConnection()
		{

			if ( sqliteConnection == null )
				sqliteConnection = new SQLiteConnection (System.IO.Path.Combine(ConstantsClass.strDbFolderPath,ConstantsClass.strDatabaseName ));

			return sqliteConnection; 
		} 
	}
}

