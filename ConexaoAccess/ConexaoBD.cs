using System;
using System.Data.OleDb;
using ConexaoAccess.Properties;

namespace ConexaoAccess
{
    public class ConexaoBD
    {

        public static void Conecatr()
        {
            // String de conexao definida na properties/settings do projeto.
            // A fonte de dados é o arquico accdb que está no PDM.
            // Provider=Microsoft.ACE.OLEDB.12.0;Data Source="C:\ELETROFRIO\ENGENHARIA SMR\produtos finais eletrofrio\mecânica\Rack padrao\CONFIGURADOR
            using (OleDbConnection conn = new OleDbConnection(Settings.Default.connAccess))
            {
                using (var command = new OleDbCommand(@"Select * from Item", conn))
                {
                    try
                    {
                        conn.Open();
                        var reader = command.ExecuteReader();
                        Console.WriteLine("Itens da tabela itens do RP");

                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetString(0) + " " + reader.GetString(1));
                        }

                    }
                    catch (OleDbException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            Console.ReadKey();

        }

        public static void Select()
        {

        }

        public static void Update()
        {

        }
    }
}