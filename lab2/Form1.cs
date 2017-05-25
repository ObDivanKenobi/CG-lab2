using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//caa:
//Фигура: цилиндр с заданной высотой и радиусом основания
//Освещение: одноточечное с заданным положением источника
//Проекция: параллельная (задаются углы поворота)од

namespace lab2
{
    public partial class Form1 : Form
    {
        Handler handler;
        Point3D light;

        public Form1()
        {
            InitializeComponent();
            handler = new Handler(pictureBox.Width, pictureBox.Height, trackBarRadius.Value, trackBarHeight.Value);
            light = new Point3D(-500, 500, 0);
            textBoxLightX.Text = "-500";
            textBoxLightY.Text = "500";
            textBoxLightZ.Text = "0";
            pictureBox.Image = handler.SetLight(light);
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Свет выключен за неуплату.", "Алярм!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;

            int x, y, z;
            if (!int.TryParse(textBoxLightX.Text, out x)
                || !int.TryParse(textBoxLightY.Text, out y)
                || !int.TryParse(textBoxLightZ.Text, out z))
            {
                MessageBox.Show("Некорректные координаты источника освещения!");
                return;
            }

            light = new Point3D(x, y, z);
            pictureBox.Image = handler.SetLight(light);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox.Image = handler.SetAngles(trackBarPhi.Value * 10, trackBarPsi.Value * 10);
        }

        private void OnAngleChange(object sender, EventArgs e)
        {
            pictureBox.Image = handler.SetAngles(trackBarPhi.Value * 10, trackBarPsi.Value * 10);
        }

        private void trackBarRadius_ValueChanged(object sender, EventArgs e)
        {
            pictureBox.Image = handler.SetRadius(trackBarRadius.Value);
        }

        private void trackBarHeight_ValueChanged(object sender, EventArgs e)
        {
            pictureBox.Image = handler.SetHeight(trackBarHeight.Value);
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            pictureBox.Image = handler.SetSize(pictureBox.Width, pictureBox.Height);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox.Image = handler.SetSize(pictureBox.Width, pictureBox.Height);
        }
    }
}
