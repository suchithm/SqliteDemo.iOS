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
	}
}

