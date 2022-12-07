using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _name = String.Empty;
        private string _phone = String.Empty;
        private string _detail= String.Empty;   
        private string _studentid= String.Empty; 
         public string Phone{
            get => _phone;
            set => _phone = value;
        }

        public string Name{
            get => _name;
            set => _name = value;
        }
        public string Detail{
            get => _detail;
            set => _detail = value;
        }
        public string Studentid{
            get => _studentid;
            set => _studentid = value;
        }
                
        public void Save(){
           using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        UPDATE orders
        SET name = $name,detail= $detail,studentid= $studentid
        WHERE phone = $phone
    ";
                commandUpdate.Parameters.AddWithValue("$name", Name);
                commandUpdate.Parameters.AddWithValue("$phone", Phone);
                commandUpdate.Parameters.AddWithValue("$detail", Detail);
                commandUpdate.Parameters.AddWithValue("$studentid", Studentid);
                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(name, phone,detail,studentid)
            VALUES($name, $phone,$detail,$studentid)
        ";
                    commandInsert.Parameters.AddWithValue("$name", Name);
                    commandInsert.Parameters.AddWithValue("$phone", Phone);
                    commandInsert.Parameters.AddWithValue("$detail", Detail);
                     commandInsert.Parameters.AddWithValue("$studentid", Studentid);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}