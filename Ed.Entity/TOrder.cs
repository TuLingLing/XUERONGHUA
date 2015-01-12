using System;
using System.Collections.Generic;
using System.Linq;
using NLite.Data;
namespace Ed.Entity
{
	[Table("T_order")]
	public partial class TOrder 
	{
	
		[Id("id",IsDbGenerated=true)]
		public Int32 Id { get;set; }
 
		[Column("order_code")]
		public String OrderCode { get;set; }
		[Column("client_id")]
		public Int32? ClientId { get;set; }
		[Column("order_type")]
		public Int32 OrderType { get;set; }
		[Column("order_begin")]
		public DateTime? OrderBegin { get;set; }
		[Column("order_end")]
		public DateTime? OrderEnd { get;set; }
		[Column("order_status")]
		public Int32? OrderStatus { get;set; }
		[Column("order_sumMoney")]
		public Double? OrderSumMoney { get;set; }
		[Column("order_prepaid")]
		public Double? OrderPrepaid { get;set; }
		[Column("order_creater")]
		public Int32? OrderCreater { get;set; }
		[Column("order_weihu")]
		public Int32? OrderWeihu { get;set; }
		[Column("order_date")]
		public DateTime OrderDate { get;set; }
		[Column("order_processtime")]
		public DateTime? OrderProcesstime { get;set; }
        [Column("order_note")]
        public String OrderNote { get; set; }
 
 
 
	}
  
}




