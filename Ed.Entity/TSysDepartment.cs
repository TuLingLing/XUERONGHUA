﻿using System;
using System.Collections.Generic;
using System.Linq;
using NLite.Data;
namespace Ed.Entity
{
	[Table("T_sys_department")]
	public partial class TSysDepartment 
	{
	
		[Id("id",IsDbGenerated=true)]
		public Int32 Id { get;set; }
 
		[Column("parent_id")]
		public Int32 ParentId { get;set; }
		[Column("dep_code")]
		public String DepCode { get;set; }
		[Column("dep_name")]
		public String DepName { get;set; }
		[Column("dep_note")]
		public String DepNote { get;set; }
		[Column("dep_order")]
		public Int32? DepOrder { get;set; }
		[Column("create_date")]
		public DateTime? CreateDate { get;set; }
 
 
 
	}
  
}




