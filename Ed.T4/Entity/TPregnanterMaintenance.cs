using System;
using System.Collections.Generic;
using System.Linq;
using NLite.Data;
namespace Ed.Entity
{
	[Table("T_pregnanter_maintenance")]
	public partial class TPregnanterMaintenance 
	{
	
		[Id("id",IsDbGenerated=true)]
		public Int32 Id { get;set; }
 
		[Column("p_info_code")]
		public Int32 PInfoCode { get;set; }
		[Column("p_main_type")]
		public Int32 PMainType { get;set; }
		[Column("p_main_content")]
		public String PMainContent { get;set; }
		[Column("p_main_creater")]
		public Int32 PMainCreater { get;set; }
		[Column("p_main_creatTime")]
		public DateTime PMainCreatTime { get;set; }
 
 
 
	}
  
}




