using System;
using System.Collections.Generic;
using System.Linq;
using NLite.Data;
namespace Ed.Entity
{
    [Table("T_order_finance")]
    public partial class TOrderFinance
    {

        [Column("id")]
        public Int32 Id { get; set; }
        [Column("order_code")]
        public String OrderCode { get; set; }
        [Column("fin_type")]
        public Int32 FinType { get; set; }
        [Column("fin_payment")]
        public Double? FinPayment { get; set; }
        [Column("fin_addtime")]
        public DateTime FinAddtime { get; set; }
        [Column("fin_creater")]
        public Int32 FinCreater { get; set; }
        [Column("fin_note")]
        public String FinNote { get; set; }



    }

}




