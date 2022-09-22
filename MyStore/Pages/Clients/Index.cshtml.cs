using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
//for using the data type List
using System.Collections.Generic;
namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public String errorMessage = "";
        public List<ClientInfo> listClients = new List<ClientInfo> ();

        public void OnGet()
        {
            try
            {//connect to Database
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=bb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    { 
                        using (SqlDataReader reader = command.ExecuteReader())//sql data reader
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo);
                            }
                        }                
                    }
                }
            }

            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
    }

    public class ClientInfo 
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
        public String Action;
    }
}


