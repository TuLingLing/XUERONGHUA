using System;
using System.Collections.Generic;
using System.Linq;
using NLite.Data;
namespace Ed.Entity
{
	[Table("T_client_maintenance")]
	public partial class TClientMaintenance 
	{
	
		[Id("id",IsDbGenerated=true)]
		public Int32 Id { get;set; }
 
		[Column("client_id")]
		public Int32 ClientId { get;set; }
		[Column("c_main_type")]
		public String CMainType { get;set; }
		[Column("c_main_content")]
		public String CMainContent { get;set; }
		[Column("c_main_time")]
		public DateTime CMainTime { get;set; }
		[Column("c_main_creater")]
		public String CMainCreater { get;set; }
 
 
 
	}
  
}




