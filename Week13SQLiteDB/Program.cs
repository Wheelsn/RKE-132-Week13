using System.Data.SQLite;
ReadData(CreateConnection());
//InsertCustomer(CreateConnection());
//RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db;Version = 3; New = True; Compress = True");
   
    try
 {
    connection.Open();
    Console.WriteLine("DB found");
 }
catch
  {
    Console.WriteLine("DB not found");
  }

    return connection;
}   

static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT customer.firstName, customer.lastName, status.statustype" +
        "FROM customerStatus " +
        "JOIN customer ON customer.rowid = customerStatus.customerId " +
        "JOIN status ON status.rowid = customerStatus.statusId " +
        "ORDER BY status.statustype ";

    reader = command.ExecuteReader();       

    while (reader.Read())
    {
        string readerRowid = reader.GetString(0);     
        string readerStringFisrtName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($" { readerRowid}  Full Name: {readerStringFisrtName} {readerStringLastName} Date of Birth: {readerStringDoB}");
    }
    myConnection.Close();
}
static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, dob;

    Console.WriteLine("Enter First Name: ");
    fName = Console.ReadLine();
    Console.WriteLine("Enter Last Name: ");
    lName = Console.ReadLine();
    Console.WriteLine("Enter Date of Birth (mm-dd-yyyy): ");
    dob = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) + VALUES ('{fName}, '{lName}', '{dob}')";
   int rowInserted = command.ExecuteNonQuery();
   Console.WriteLine($"Row inserted: {rowInserted} ");
   

    ReadData(myConnection);
}

static void RemoveCustomer (SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string iDToDelete;
    Console.WriteLine("Enter the ID of the customer to delete: ");
    iDToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {iDToDelete}";
    int rowDeleted = command.ExecuteNonQuery();
    Console.WriteLine($"Row deleted: {rowDeleted}");

    ReadData(myConnection);
}

