using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ColligateDemo
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frm_Main f = new frm_Main();
            try {
                for (int i = 0; i < args.Length; i++)
                {
                    string parametroActual = args[i];
                    int posDosPuntos = parametroActual.IndexOf(":");
                    if (posDosPuntos < 0)
                    {
                        continue;
                    }
                    string paramNombre = parametroActual.Substring(0, posDosPuntos);
                    string paramValue = parametroActual.Substring(posDosPuntos + 1);
                    
                    if (parametroActual.Contains("-ip"))
                    {
                        f.ip = paramValue;
                    }
                    else if (parametroActual.Contains("-port"))
                    {
                        f.port = ushort.Parse(paramValue);
                    }
                    else if (parametroActual.Contains("-usr"))
                    {
                        f.user = paramValue;
                    }
                    else if (parametroActual.Contains("-psw"))
                    {
                        f.pass = paramValue;
                    }                        
                    else if (parametroActual.Contains("-channel"))
                    {
                        f.channel = Int16.Parse(paramValue) -1;
                    }
                    else if (parametroActual.Contains("-caption"))
                    {
                        f.caption = paramValue;
                        f.Text = paramValue;
                    }
                    else if (parametroActual.Contains("-cuenta_id"))
                    {
                        f.cuenta_id = paramValue;
                    }
                    else if (parametroActual.Contains("-sys2015"))
                    {
                        f.sys2015 = paramValue;
                    }
                    else if (parametroActual.Contains("-mysql_DB_name"))
                    {
                        f.mysql_DB_name = paramValue;
                    }
                    else if (parametroActual.Contains("-mysql_DB_host"))
                    {
                        f.mysql_DB_host = paramValue;
                    }
                    else if (parametroActual.Contains("-mysql_DB_port"))
                    {
                        f.mysql_DB_port = Int16.Parse(paramValue);
                    }
                    else if (parametroActual.Contains("-mysql_DB_user"))
                    {
                        f.mysql_DB_user = paramValue;
                    }
                    else if (parametroActual.Contains("-mysql_DB_password"))
                    {
                        f.mysql_DB_password = paramValue;
                    }
                    else if (parametroActual.Contains("-ventanas"))
                    {
                        f.cantidadVentanas = Int16.Parse(paramValue);
                    }
                                        
                
                }
                Application.Run(f);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error de parametros! " + e.Message, "Error");
                Application.Exit();

            }

            
        }
    }
}