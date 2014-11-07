using System;
using System.Collections.Generic;
using System.Linq;
using NLite.Data;
namespace Ed.Entity
{
	[Table("T_sys_user")]
	public partial class TSysUser 
	{
	
		[Id("id",IsDbGenerated=true)]
		public Int32 Id { get;set; }
 
		[Column("user_lname")]
		public String UserLname { get;set; }
		[Column("user_password")]
		public String UserPassword { get;set; }
		[Column("user_tname")]
		public String UserTname { get;set; }
		[Column("user_sex")]
		public Int32 UserSex { get;set; }
		[Column("user_phone")]
		public String UserPhone { get;set; }
		[Column("user_email")]
		public String UserEmail { get;set; }
		[Column("user_tel")]
		public String UserTel { get;set; }
		[Column("user_qq")]
		public String UserQq { get;set; }
		[Column("dep_id")]
		public Int32 DepId { get;set; }
		[Column("role_id")]
		public Int32 RoleId { get;set; }
		[Column("user_enable")]
		public Int32 UserEnable { get;set; }
		[Column("user_regDate")]
		public DateTime? UserRegDate { get;set; }
 
 
 
	}
  
}




