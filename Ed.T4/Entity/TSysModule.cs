using System;
using System.Collections.Generic;
using System.Linq;
using NLite.Data;
namespace Ed.Entity
{
	[Table("T_sys_module")]
	public partial class TSysModule 
	{
	
		[Id("id",IsDbGenerated=false)]
		public Int32 Id { get;set; }
 
		[Column("m_code")]
		public String MCode { get;set; }
		[Column("m_parent")]
		public Int32? MParent { get;set; }
		[Column("m_name")]
		public String MName { get;set; }
		[Column("m_link")]
		public String MLink { get;set; }
		[Column("m_note")]
		public String MNote { get;set; }
 
 
 
	}
  
}




