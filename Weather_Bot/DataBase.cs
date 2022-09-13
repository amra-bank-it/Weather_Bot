using Parsering;
using System.Configuration;
using System.Data.SqlClient;

namespace NewAbhWeatherBot
{
    public class DataBase
    {
        public static void AddToDB(string url, ref string city, ref string temp, ref string feels, ref string wind, ref string humidity, ref string pressure, ref string water)
        {
            Parser.Parse(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
            SqlConnection con = new(ConfigurationManager.ConnectionStrings["Parser"].ConnectionString);
            string query = "INSERT INTO AbhWeather (City, Temp, Feels, Wind, Humidity, Pressure, Water) VALUES (@City, @Temp, @Feels, @Wind, @Humidity, @Pressure, @Water)";
            SqlCommand command = new(query, con);
            command.Parameters.AddWithValue("@City", city);
            command.Parameters.AddWithValue("@Temp", temp);
            command.Parameters.AddWithValue("@Feels", feels);
            command.Parameters.AddWithValue("@Wind", wind);
            command.Parameters.AddWithValue("@Humidity", humidity);
            command.Parameters.AddWithValue("@Pressure", pressure);
            command.Parameters.AddWithValue("@Water", water);
            try
            {
                con.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Произошла ошибка при передаче данных в базу. Код ошибки: " + e.ToString());
            }
            finally
            {
                con.Close();
            }
            return;
        }
    }
}
