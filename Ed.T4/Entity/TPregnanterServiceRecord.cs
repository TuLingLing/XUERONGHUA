using System;
using System.Collections.Generic;
using System.Linq;
using NLite.Data;
namespace Ed.Entity
{
	[Table("T_pregnanter_serviceRecord")]
	public partial class TPregnanterServiceRecord 
	{
	
		[Id("id",IsDbGenerated=true)]
		public Int32 Id { get;set; }
 
		[Column("p_service_code")]
		public String PServiceCode { get;set; }
		[Column("p_info_code")]
		public Int32 PInfoCode { get;set; }
		[Column("p_service_type")]
		public Int32 PServiceType { get;set; }
		[Column("p_service_state")]
		public Int32 PServiceState { get;set; }
		[Column("p_service_begin")]
		public DateTime? PServiceBegin { get;set; }
		[Column("p_service_note")]
		public String PServiceNote { get;set; }
		[Column("p_service_end")]
		public DateTime PServiceEnd { get;set; }
		[Column("p_service_createTime")]
		public DateTime PServiceCreateTime { get;set; }
		[Column("p_service_creater")]
		public Int32? PServiceCreater { get;set; }
 
 
 
	}
  
}




