namespace KomunikatorServerTest
{
    class main
    {
        static void Main(string[] args)
        {
            DataBaseAccessor dba = new DataBaseAccessor();
            dba.connectToDataBase("localhost", 5432, "margo", "parowa123", "komunikator");
            dba.selectUserById(1);
            dba.login("margok1234@gmail.com", "parowa123");
        }
    }
}
