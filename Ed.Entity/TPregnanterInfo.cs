using System;
using System.Collections.Generic;
using System.Linq;
using NLite.Data;
namespace Ed.Entity
{
	[Table("T_pregnanter_info")]
	public partial class TPregnanterInfo 
	{
	
		[Id("id",IsDbGenerated=true)]
		public Int32 Id { get;set; }
 
		[Column("p_info_code")]
		public String PInfoCode { get;set; }
		[Column("p_info_name")]
		public String PInfoName { get;set; }
		[Column("p_info_photo")]
		public String PInfoPhoto { get;set; }
		[Column("p_info_idcard")]
		public String PInfoIdcard { get;set; }
		[Column("p_info_national")]
		public String PInfoNational { get;set; }
		[Column("p_info_birthday")]
		public DateTime? PInfoBirthday { get;set; }
		[Column("p_info_age")]
		public Int32? PInfoAge { get;set; }
		[Column("p_info_xingzuo")]
		public String PInfoXingzuo { get;set; }
		[Column("p_info_shuxiang")]
		public String PInfoShuxiang { get;set; }
		[Column("p_info_edage")]
		public Int32? PInfoEdage { get;set; }
		[Column("p_info_type")]
		public Int32? PInfoType { get;set; }
		[Column("p_info_level")]
		public Int32? PInfoLevel { get;set; }
        [Column("p_info_price_type")]
        public String PInfoPriceType { get; set; }
		[Column("p_info_price")]
		public Double? PInfoPrice { get;set; }
        [Column("p_info_price_12")]
		public Double? PInfoPrice12 { get;set; }
        [Column("p_info_Yprice")]
        public Double? PInfoYPrice { get; set; }
        [Column("p_info_Yprice_12")]
        public Double? PInfoYPrice12 { get; set; }
        [Column("p_info_fenlei")]
        public Int32? PInfoFenlei { get; set; }
        [Column("p_info_state")]
		public Int32? PInfoState { get;set; }
		[Column("p_info_education")]
		public Int32? PInfoEducation { get;set; }
		[Column("p_info_marriage")]
		public Int32? PInfoMarriage { get;set; }
		[Column("p_info_hobby")]
		public String PInfoHobby { get;set; }
        [Column("p_info_class")]
        public String PInfoClass { get; set; }
        [Column("p_info_city")]
		public String PInfoCity { get;set; }
		[Column("p_info_address")]
		public String PInfoAddress { get;set; }
		[Column("p_info_cert")]
		public String PInfoCert { get;set; }
		[Column("p_info_train")]
		public String PInfoTrain { get;set; }
		[Column("p_info_edu")]
		public String PInfoEdu { get;set; }
		[Column("p_info_permanent")]
		public String PInfoPermanent { get;set; }
		[Column("p_info_reward")]
		public String PInfoReward { get;set; }
		[Column("p_info_lifeimg")]
		public String PInfoLifeimg { get;set; }
		[Column("p_info_certimg")]
		public String PInfoCertimg { get;set; }
		[Column("p_info_note")]
		public String PInfoNote { get;set; }
		[Column("p_info_tel")]
		public String PInfoTel { get;set; }
		[Column("p_info_contacter1")]
		public String PInfoContacter1 { get;set; }
		[Column("p_info_relation1")]
		public String PInfoRelation1 { get;set; }
		[Column("p_info_workunit1")]
		public String PInfoWorkunit1 { get;set; }
		[Column("p_info_reltel1")]
		public String PInfoReltel1 { get;set; }
		[Column("p_info_contact2")]
		public String PInfoContact2 { get;set; }
		[Column("p_info_relation2")]
		public String PInfoRelation2 { get;set; }
		[Column("p_info_workunit2")]
		public String PInfoWorkunit2 { get;set; }
		[Column("p_info_relTel2")]
		public String PInfoRelTel2 { get;set; }
		[Column("p_info_createperson")]
		public Int32? PInfoCreateperson { get;set; }
		[Column("p_info_lastdate")]
		public DateTime? PInfoLastdate { get;set; }
		[Column("p_info_createdate")]
		public DateTime? PInfoCreatedate { get;set; }
		[Column("p_info_quality")]
		public String PInfoQuality { get;set; }
		[Column("p_info_knowledge")]
		public String PInfoKnowledge { get;set; }
		[Column("p_info_service")]
		public String PInfoService { get;set; }
        [Column("P_info_spicture")]
        public Int32? PInfoSpicture { get; set; }
        [Column("P_info_scomment")]
        public Int32? PInfoScomment { get; set; }
        [Column("p_info_stotal")]
        public Int32? PInfoStotal { get; set; }
        [Column("P_info_start")]
        public DateTime? PInfoStart { get; set; }
        [Column("P_info_end")]
        public DateTime? PInfoEnd { get; set; }
	}
  
}
