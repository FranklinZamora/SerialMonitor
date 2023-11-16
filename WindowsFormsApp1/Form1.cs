using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using ComponentFactory.Krypton.Toolkit;

namespace WindowsFormsApp1
{
   
    public partial class TerminalSerial : KryptonForm
    {
        string[] arregloPuertos; //Arreglo para los puertos

        SerialPort puerto;
        string datosPuerto;
        Timer temporizador;

        public TerminalSerial()
        {
            InitializeComponent();
            puerto = new SerialPort(); //Constructor Puerto Serial
            // Enumeracion 
            puerto.DataReceived += Puerto_DataReceived;
            temporizador = new Timer();
            temporizador.Interval = 500;
            temporizador.Enabled = true;
            temporizador.Start();
            
        }


        //Metodo generado parea recibir datos
        private void Puerto_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            datosPuerto = puerto.ReadLine();
            this.Invoke(new Action(ProcessingData));
        }
        private void ProcessingData()
        {
          resultado.Text += datosPuerto.ToString() + Environment.NewLine;
          waiting.Text = "Datos recibidos: ";  
          resultado.Cursor = Cursors.Default;
            if (AScroll.Checked == true)
            {
                panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
            }
            else
            {

            }


        }

        private void MetodoDropDown_cbPuertos(object sender, EventArgs e)
        {
            cbPuertos.Items.Clear();
            arregloPuertos = SerialPort.GetPortNames(); // Metodo que regtresa un array con los puertos seriales en string
            foreach (string element_nombrePort in arregloPuertos)       
            {
                cbPuertos.Items.Add(element_nombrePort);
            }           

        }

       
        private void clean_Click(object sender, EventArgs e)
        {
            resultado.Text = "";
            
            if (labelEstadoPuerto.Text != "Disconnected")
            {
                waiting.Text = "Actualizando...";
 
            }
            else
            {
                waiting.Text = "Esperando datos...";
            }
        }

        private void cbPuertos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Conectar_Click(object sender, EventArgs e)
        {
            try
            {
                if (labelEstadoPuerto.Text == "Disconnected")
                {
                    if (cbPuertos.SelectedItem != null)
                    {
                        puerto.PortName = cbPuertos.SelectedItem.ToString();
                        puerto.Open();
                        colorC.BackColor = Color.Green;
                    }

                    if (puerto.IsOpen)
                    {
                        labelEstadoPuerto.Text = "Connected";
                        Conectar.Text = "Disconnect";
                    }
                }

                else if (labelEstadoPuerto.Text == "Connected")
                {
                    try
                    {
                        puerto.Close();
                        colorC.BackColor = Color.Red;
                        if (puerto.IsOpen == false)
                        {
                            labelEstadoPuerto.Text = "Disconnected";
                            Conectar.Text = "Connect";


                        }

                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message);
                    }

                }
            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void reusltado_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
