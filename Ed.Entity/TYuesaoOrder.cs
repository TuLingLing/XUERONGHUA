using System;
using System.Collections.Generic;
using System.Linq;
using NLite.Data;
namespace Ed.Entity
{
    [Table("T_yuesao_order")]
    public partial class TYuesaoOrder
    {

        [Id("id", IsDbGenerated = true)]
        public Int32 Id { get; set; }

        [Column("order_code")]
        public String OrderCode { get; set; }
        [Column("yorder_code")]
        public Int32? YorderCode { get; set; }
        [Column("yorder_childbirth")]
        public DateTime? YorderChildbirth { get; set; }
        [Column("yorder_hospital")]
        public String YorderHospital { get; set; }
        [Column("yorder_haddress")]
        public String YorderHaddress { get; set; }
        [Column("yorder_require")]
        public String YorderRequire { get; set; }
        [Column("yorder_note")]
        public String YorderNote { get; set; }
        [Column("yorder_age")]
        public String YorderAge { get; set; }
        [Column("yorder_level")]
        public Int32? YorderLevel { get; set; }
        [Column("yorder_price")]
        public Int32? YorderPrice { get; set; }
        [Column("yorder_cons")]
        public String YorderCons { get; set; }
        [Column("yorder_zodiac")]
        public Int32? YorderZodiac { get; set; }
        [Column("yorder_alter")]
        public String YorderAlter { get; set; }
        [Column("yorder_yuesao")]
        public String YorderYuesao { get; set; }
        [Column("yorder_ptype")]
        public String YorderPtype { get; set; }
    }

}




