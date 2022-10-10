using System.Data.OleDb;
while (true)
{
    OleDbConnection connection = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; " +
        "Data Source = 1001songs.accdb");
    OleDbCommand command = connection.CreateCommand();
    OleDbDataReader reader = null;
    command.Connection = connection;

    Random rand = new Random();
    List<int> list1 = new List<int>();

    string check = Console.ReadLine();
    if (check == "new")
    {
        connection.Open();
        command.CommandText = "DELETE * FROM table1 WHERE selected=true";
        command.ExecuteNonQuery();
        connection.Close();

        connection.Open();
        command.CommandText = "SELECT * FROM table1";
        reader = command.ExecuteReader();
        while (reader.Read())
        {
            list1.Add(Convert.ToInt32(reader[0]));
        }
        connection.Close();

        int first, second, third, depo;
        first = list1[rand.Next(0, list1.Count)];
        while (true)
        {
            depo = rand.Next(0, list1.Count);
            if (list1[depo] != first)
            {
                second = list1[depo];
                break;
            }
        }
        while (true)
        {
            depo = rand.Next(0, list1.Count);
            if (list1[depo] != first && list1[depo] != second)
            {
                third = list1[depo];
                break;
            }
        }

        connection.Open();
        command.CommandText = "UPDATE table1 SET selected=true WHERE index=" + first;
        command.ExecuteNonQuery();
        command.CommandText = "UPDATE table1 SET selected=true WHERE index=" + second;
        command.ExecuteNonQuery();
        command.CommandText = "UPDATE table1 SET selected=true WHERE index=" + third;
        command.ExecuteNonQuery();
        connection.Close();

        Console.WriteLine("New albums selected");
    }
    else if (check == "show")
    {
        connection.Open();
        command.CommandText = "SELECT * FROM table1 WHERE selected=true";
        reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(reader[1] + "   by   " + reader[2] + "   (" + reader[3] + ")");
        }
        connection.Close();
    }
    else if (check == "quit") break;
}