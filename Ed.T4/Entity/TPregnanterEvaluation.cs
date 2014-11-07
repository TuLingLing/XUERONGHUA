using System;
using System.Collections.Generic;
using System.Linq;
using NLite.Data;
namespace Ed.Entity
{
	[Table("T_pregnanter_evaluation")]
	public partial class TPregnanterEvaluation 
	{
	
		[Id("id",IsDbGenerated=true)]
		public Int32 Id { get;set; }
 
		[Column("order_code")]
		public String OrderCode { get;set; }
		[Column("p_info_code")]
		public Int32 PInfoCode { get;set; }
		[Column("p_eva_level")]
		public Int32? PEvaLevel { get;set; }
		[Column("p_eva_content")]
		public String PEvaContent { get;set; }
		[Column("p_eva_time")]
		public DateTime? PEvaTime { get;set; }
		[Column("p_eva_person")]
		public String PEvaPerson { get;set; }
		[Column("p_eva_creater")]
		public Int32 PEvaCreater { get;set; }
		[Column("p_eva_creattime")]
		public DateTime PEvaCreattime { get;set; }
		[Column("p_eva_img")]
		public String PEvaImg { get;set; }
 
 
 
	}
  
}




