using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    class DBoperate
    {
        private readonly String HOSTSTR = "Server=.;Database=JNU-Agile;User ID=edisond;Password=33635468";
        private SqlConnection dbConnection;
        private SqlCommand cmd;
        public void DatabaseConnector()
	    {
            dbConnection = new SqlConnection(HOSTSTR);
	    }

        //将今日数据添加进数据库
        public bool AddFoudItem(List<Found> found)
        {
            try
            {
                dbConnection.Open();
                foreach (Found foundItem in found)
                { 
                    cmd = new SqlCommand("insert into Table_Found values('"
                        + foundItem.Code + "','"
                        + foundItem.Name + "','"
                        + foundItem.Newnet + "','"
                        + foundItem.Totalnet + "','"
                        + foundItem.Dayincrease + "','"
                        + foundItem.Daygrowrate + "','"
                        + foundItem.Weekgrowrate + "')"
                        + foundItem.Annualincome + "')"
                        + foundItem.Date + "')"
                        , dbConnection);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        //根据日期查找基金
        public Found GetFoundItem(DateTime date)
        {
            try
            {
                dbConnection.Open();
                cmd = new SqlCommand("Select * from Table_Found where Date ='" + date.ToString() + "'", dbConnection);
                SqlDataReader foundItem = cmd.ExecuteReader();
                while (foundItem.Read())
                {
                    Found found = new Found();
                    found.Code = foundItem.GetString(0);
                    found.Name = foundItem.GetString(1);
                    found.Newnet = foundItem.GetFloat(2);
                    found.Totalnet = foundItem.GetFloat(3);
                    found.Dayincrease = foundItem.GetFloat(4);
                    found.Daygrowrate = foundItem.GetFloat(5);
                    found.Weekgrowrate = foundItem.GetFloat(6);
                    found.Annualincome = foundItem.GetFloat(7);
                    return found;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
