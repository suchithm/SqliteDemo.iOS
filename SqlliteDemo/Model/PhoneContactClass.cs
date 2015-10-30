using SQLite;
namespace SqliteDemo
{
	public class PhoneContactClass
	{
		[PrimaryKey, AutoIncrement]
		public int Id{ get; set;}
		[NotNull]
		public string strContactName{get;set;} 
		public long strContactNumber{ get; set;}
	}
}

