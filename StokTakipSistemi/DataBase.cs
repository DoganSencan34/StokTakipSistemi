using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakipSistemi
{
    class DataBase
    {
        public static string ConStr = "server=DESKTOP-86INSUI\\DGN; user id=dgn; password=asasas123123123; database=StokTakipSistemi;";

        public static object execParameterSql(string sql, string[] parameters, SqlDbType[] types, object[] values)
        {//Parametreli işlem yaparken kullandığımız sınıf
            SqlConnection conn = new SqlConnection("server=DESKTOP-86INSUI\\DGN; user id=dgn; password=asasas123123123; database=StokTakipSistemi;");
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = sql;


            for (int i = 0; i < parameters.Length; i++)
            {
                SqlParameter param = new SqlParameter(parameters[i], types[i]);
                param.Value = values[i];
                command.Parameters.Add(param);
            }
            command.Connection = conn;
            object result = command.ExecuteScalar();
            conn.Close();
            return result;
        }


        public static string execScalar(string sql, string isNullStr = null)
        {
            SqlConnection conn = new SqlConnection(ConStr);
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = sql;
            command.Connection = conn;

            string result;
            if (isNullStr != null)
            {
                try
                {
                    result = command.ExecuteScalar().ToString();
                }
                catch (Exception)
                {
                    result = isNullStr;
                }
            }
            else
            {
                result = command.ExecuteScalar().ToString();
            }

            conn.Close();
            return result;
        }


















        public static DataTable select(string sql)
        {
            SqlConnection conn = new SqlConnection(ConStr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public static void ExecSql(string sql)
        {
            SqlConnection conn = new SqlConnection(ConStr);
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = sql;
            command.ExecuteNonQuery();
            conn.Close();
        }

       


        public static string selectImageSql(string sql)
        {//resmi sql çekiyor
            SqlConnection conn = new SqlConnection("server=DESKTOP-86INSUI\\DGN; user id=erd; password=erd; database=ERDCRM;");
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = sql;
            command.Connection = conn;
            SqlDataReader cursor = command.ExecuteReader();
            byte[] imageBytes = null;
            if (cursor.Read())
            {
                imageBytes = (byte[])cursor[0];
            }
            cursor.Close();
            conn.Close();
            string base64String = Convert.ToBase64String(imageBytes, 0, imageBytes.Length);
            string imageString = "data:image/jpeg;base64," + base64String;
            return imageString;
        }

        public static string byteToImageString(byte[] imageBytes)//resmi img etiketi yüklemek için
        {
            string base64String = Convert.ToBase64String(imageBytes, 0, imageBytes.Length);
            string imageString = "data:image/jpeg;base64," + base64String;
            return imageString;
        }


        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream mStream = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(mStream);
            }
        }



    }
}
