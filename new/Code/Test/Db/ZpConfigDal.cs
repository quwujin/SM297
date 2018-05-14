using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Db
{
   public  class ZpConfigDal
    {
       public string conn = SqlHelper.ConnectionString;



       public Model.ZpConfigModel GetModel(int id)
       {
           StringBuilder sql = new StringBuilder();
           sql.Append("select * from zp_config where id=" + id);
           Model.ZpConfigModel model = new Model.ZpConfigModel();
           SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
           if (dr.Read())
           {
               model.Id = Convert.ToInt32(dr["Id"].ToString());
               model.Zjl1 = Convert.ToInt32(dr["Zjl1"].ToString());
               model.Zjl2 = Convert.ToInt32(dr["Zjl2"].ToString());
               model.Zjl3 = Convert.ToInt32(dr["Zjl3"].ToString());
               model.Zjl4 = Convert.ToInt32(dr["Zjl4"].ToString());
               model.Zjl5 = Convert.ToInt32(dr["Zjl5"].ToString());
               model.Zjl6 = Convert.ToInt32(dr["Zjl6"].ToString());
               model.Zjl7 = Convert.ToInt32(dr["Zjl7"].ToString());
               model.Zjl8 = Convert.ToInt32(dr["Zjl8"].ToString());
               model.Zjl9 = Convert.ToInt32(dr["Zjl9"].ToString());
               model.Zjl10 = Convert.ToInt32(dr["Zjl10"].ToString());
               model.Zjl11 = Convert.ToInt32(dr["Zjl11"].ToString());
               model.Zjl12 = Convert.ToInt32(dr["Zjl12"].ToString());
               model.Zjl13 = Convert.ToInt32(dr["Zjl13"].ToString());
               model.Zjl14 = Convert.ToInt32(dr["Zjl14"].ToString());
               model.Zjl15 = Convert.ToInt32(dr["Zjl15"].ToString());
               model.Zjl16 = Convert.ToInt32(dr["Zjl16"].ToString());
               model.Zjl17 = Convert.ToInt32(dr["Zjl17"].ToString());
               model.Zjl18 = Convert.ToInt32(dr["Zjl18"].ToString());
           }
	        dr.Close();
           return model;

       }


       public int Update(Model.ZpConfigModel model)
       {


           int obj = 0;
 
           StringBuilder sql = new StringBuilder();

           sql.Append(" Update zp_config set Zjl1=@Zjl1,Zjl2=@Zjl2,Zjl3=@Zjl3,Zjl4=@Zjl4,Zjl5=@Zjl5,Zjl6=@Zjl6,Zjl7=@Zjl7,Zjl8=@Zjl8,Zjl9=@Zjl9,"+
               "Zjl10=@Zjl10,Zjl11=@Zjl11,Zjl12=@Zjl12,Zjl13=@Zjl13,Zjl14=@Zjl14,Zjl15=@Zjl15,Zjl16=@Zjl16,Zjl17=@Zjl17,Zjl18=@Zjl18  where Id=@Id");
 
           SqlParameter[] para = new SqlParameter[]
			{
                new SqlParameter("@Zjl1",model.Zjl1),
                new SqlParameter("@Zjl2",model.Zjl2),
                new SqlParameter("@Zjl3",model.Zjl3),
                new SqlParameter("@Zjl4",model.Zjl4),
                new SqlParameter("@Zjl5",model.Zjl5),
                new SqlParameter("@Zjl6",model.Zjl6),
                new SqlParameter("@Zjl7",model.Zjl7),
                new SqlParameter("@Zjl8",model.Zjl8),
                new SqlParameter("@Zjl9",model.Zjl9),
                new SqlParameter("@Zjl10",model.Zjl10),      
                new SqlParameter("@Zjl11",model.Zjl11),      
                new SqlParameter("@Zjl12",model.Zjl12),      
                new SqlParameter("@Zjl13",model.Zjl13),      
                new SqlParameter("@Zjl14",model.Zjl14),      
                new SqlParameter("@Zjl15",model.Zjl15),      
                new SqlParameter("@Zjl16",model.Zjl16),      
                new SqlParameter("@Zjl17",model.Zjl17),      
                new SqlParameter("@Zjl18",model.Zjl18),      
                new SqlParameter("@Id",model.Id)
			};

           try
           {
               SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString(), para);
               obj = model.Id;

           }
           catch (Exception)
           {
               obj = 0;
           }
           return obj;
       }

    }
}