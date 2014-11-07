using System;
using System.Collections.Generic;
using System.Linq;
using NLite.Data;
namespace Ed.Entity
{
	[Table("T_client_info")]
	public partial class TClientInfo 
	{
	
		[Id("id",IsDbGenerated=true)]
		public Int32 Id { get;set; }
 
		[Column("client_code")]
		public String ClientCode { get;set; }
		[Column("client_name")]
		public String ClientName { get;set; }
		[Column("client_sex")]
		public String ClientSex { get;set; }
		[Column("client_idcard")]
		public String ClientIdcard { get;set; }
		[Column("client_birthday")]
		public DateTime? ClientBirthday { get;set; }
		[Column("client_onote")]
		public String ClientOnote { get;set; }
		[Column("client_age")]
		public Int32? ClientAge { get;set; }
		[Column("client_childbirth")]
		public DateTime? ClientChildbirth { get;set; }
		[Column("client_national")]
		public String ClientNational { get;set; }
		[Column("client_permanent")]
		public String ClientPermanent { get;set; }
		[Column("client_city")]
		public String ClientCity { get;set; }
		[Column("client_address")]
		public String ClientAddress { get;set; }
		[Column("client_tel1")]
		public String ClientTel1 { get;set; }
		[Column("client_tel2")]
		public String ClientTel2 { get;set; }
		[Column("client_hobby")]
		public String ClientHobby { get;set; }
		[Column("client_note")]
		public String ClientNote { get;set; }
		[Column("client_creater")]
		public Int32 ClientCreater { get;set; }
		[Column("client_createTime")]
		public DateTime ClientCreateTime { get;set; }
		[Column("client_weihu")]
		public Int32 ClientWeihu { get;set; }
		[Column("client_lasttime")]
		public DateTime ClientLasttime { get;set; }
		[Column("client_status")]
		public Int32? ClientStatus { get;set; }
 
 
 
	}
  
}




