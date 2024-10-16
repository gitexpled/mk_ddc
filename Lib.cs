using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;

namespace SimpleAgroPrint
{
    public class Lib
    {

        /*       public Pantalla GetPantalla()
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=192.168.250.110;Initial Catalog=DDCTEST;User Id=sa;Password=MB021Z/A");
            Pantalla p = new Pantalla();
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                //cmd.CommandText = "SELECT * FROM zpl_ventana where nombre_tabla = '192.168.250.11'";// + GetIP() + "'" ;
                cmd.CommandText = "select ip_pantalla, stock, zpl, p.centro, '192.168.0.1', kilos_material,p.proceso  from proceso p left join zpl_ventana zv on p.proceso = zv.proceso left join posicion_zpl pz on pz.id = zv.id_posicion where ip_pantalla = '192.168.0.1'and (stock > 0 or estado = 'VIGENTE') GROUP BY ip_pantalla, stock, zpl, p.centro, kilos_material, p.proceso";// + GetIP() + "'" ;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                reader = cmd.ExecuteReader();
                // Data is accessible through the DataReader object here.

                
                p.Botones = new Boton[4];
                int i = 0;
                while (reader.Read())
                {
                    p.CantBotones++; 
                    p.Botones[i] = new Boton();
                    p.Botones[i].Cantidad = int.Parse(reader.GetValue(1).ToString());
                    //p.Botones[i].Nombre = reader.GetValue(1).ToString();
                    p.Botones[i].ZPL = reader.GetValue(2).ToString();
                    p.Centro = reader.GetValue(3).ToString(); ;
                    p.Ip = reader.GetValue(0).ToString();
                    //p.Printer = reader.GetValue(5).ToString();
                    p.Printer = reader.GetValue(4).ToString();
                    p.Botones[i].Kg = decimal.Parse(reader.GetValue(5).ToString());
                    p.Proceso = reader.GetValue(6).ToString();
                    i++;

                }

            }
            catch (Exception ex)
            {
                
                
            }
            sqlConnection1.Close();
            
            return p;
        }

        
        public string GetKilosActuales(string proceso)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=192.168.250.110;Initial Catalog=DDCTEST;User Id=sa;Password=MB021Z/A");
            string kilosActuales = "0";
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                //cmd.CommandText = "SELECT * FROM zpl_ventana where nombre_tabla = '192.168.250.11'";// + GetIP() + "'" ;
                cmd.CommandText = "select kilos_etiquetas from proceso where proceso = '" + proceso + "' group by proceso, kilos_etiquetas";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                reader = cmd.ExecuteReader();
                // Data is accessible through the DataReader object here.


                
                int i = 0;
                
                while (reader.Read())
                {                  
                    kilosActuales = reader.GetValue(0).ToString();

                    i++;

                }

            }
            catch (Exception ex)
            {


            }
            sqlConnection1.Close();

            return kilosActuales;
        }


        public int UpdateKilosEtiquetas(string proceso, decimal kilos)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=192.168.250.110;Initial Catalog=DDCTEST;User Id=sa;Password=MB021Z/A");
            int response = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                //cmd.CommandText = "SELECT * FROM zpl_ventana where nombre_tabla = '192.168.250.11'";// + GetIP() + "'" ;
                
                decimal kilosActuales = decimal.Parse(GetKilosActuales(proceso));
                string kilosNuevos = (kilosActuales + kilos).ToString();

                cmd.CommandText = "update proceso set kilos_etiquetas = '" + kilosNuevos + "' where proceso = '" + proceso + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                response = cmd.ExecuteNonQuery();
                // Data is accessible through the DataReader object here.

                int i = 0;

                

            }
            catch (Exception ex)
            {


            }
            sqlConnection1.Close();

            return response ;
        }

        */

        public string  GetIP()
        {
            string Home = "127.0.0.1";
            try
            {

                string HostName = Dns.GetHostName();

                System.Net.IPHostEntry IPHost = Dns.GetHostByName(HostName);


                //TO DO: CAMBIAR INDICE 1 POR INDICE 0
                 string DeviceIP = IPHost.AddressList[0].ToString();

                if (DeviceIP != IPAddress.Parse(Home).ToString())
                {

                    return DeviceIP;

                }

                else { return ""; }

            }

            catch (System.Net.WebException webEx)
            {

                return webEx.Message;

            }

            catch (System.Exception othEx)
            {

                return othEx.Message;

            }

        }
    }

    public struct Boton
    {
        public string Nombre;
        public int Cantidad;
        public string ZPL;
        public decimal Kg; 
    }
    public struct Pantalla
    {
        public string Ip;
        public string Printer;
        public Boton[] Botones;
        public string Linea;
        public string Centro;
        public int CantBotones;
        public string Proceso;

    }

}
