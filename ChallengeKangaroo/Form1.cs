using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChallengeKangaroo
{
    public partial class Form1 : Form
    {
        public int p1;
        public int v1;
        public int p2;
        public int v2;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarEntradas();
               
                Stopwatch timeMeasure = new Stopwatch();
                timeMeasure.Start();
                label5.Text = CalcularEncuentro(p1, v1, p2, v2);
                timeMeasure.Stop();
                label6.Text = $"Tiempo ejecución con iteración: '{timeMeasure.ElapsedMilliseconds}ms'";

                Stopwatch timeMeasure2 = new Stopwatch();
                timeMeasure2.Start();
                label8.Text = CalcularEncuentroFormula(p1, v1, p2, v2);
                timeMeasure2.Stop();
                label7.Text = $"Tiempo ejecución conm fórmula: '{timeMeasure2.ElapsedMilliseconds}ms'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ValidarEntradas()
        {
            p1 = Convert.ToInt32(numericUpDown1.Value);
            v1 = Convert.ToInt32(numericUpDown2.Value);
            p2 = Convert.ToInt32(numericUpDown3.Value);
            v2 = Convert.ToInt32(numericUpDown4.Value);

            Validar("Posición 1", p1, Constantes.Tope_Min_Pos, Constantes.Tope_Max_Pos);
            Validar("Velocidad 1", v1, Constantes.Tope_Min_Vel, Constantes.Tope_Max_Vel);
            Validar("Posición 2", p2, p1 + 1, Constantes.Tope_Max_Pos);
            Validar("Velocidad 2", v2, Constantes.Tope_Min_Vel, Constantes.Tope_Max_Vel);
        }
        private void Validar(string nombre, decimal value, int min,int max)
        {
            if (!Information.IsNumeric(value) ||
                value < min ||
                value > max)
                throw new Exception($"{nombre} debe ser un entero mayor o igual que {min} y menor o igual que {max}. Intente nuevamente"); 
        }

        private string CalcularEncuentro(decimal p1, decimal v1, decimal p2, decimal v2)
        {
            if (v2 >= v1)
                return $"NO se cruzarán";

            for (int i = 0; i <= Constantes.Tope_Max_Pos; i++)
            {
                var salto1 = p1 + v1*i;
                var salto2 = p2 + v2*i;
                if (salto1 == salto2)
                    return $"SI se cruzarán en la posición: {salto1}";
            }
            return $"NO se cruzarán";
        }

        private string CalcularEncuentroFormula(decimal p1, decimal v1, decimal p2, decimal v2)
        {
            if (v2 >= v1)
                return $"NO se cruzarán";

            decimal tiempoInterseccion = (p2 - p1) / (v1 - v2);
            if (tiempoInterseccion > 0)
            {
                var interseccion = p1 + v1 * tiempoInterseccion;
                return $"SI se cruzarán en la posición: {interseccion}";
            }
            else
            {
                return $"NO se cruzarán";
            }
        }

    }
}
