using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace EscapingButtonSpace
{
    public class EscapingButton
    {
        private bool status;
        private List<Point> btnPosition = new List<Point>();
        private Point position;
        private Form form;

        public EscapingButton(Form formLoad, bool _status)
        {
            form = formLoad;
            status = _status;

            form.KeyPreview = true;
            form.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Z) ButtonMove(); };

            CreateLabel();
            ButtonsSaveLocation();

            foreach (Control control in form.Controls)
            {
                control.PreviewKeyDown += new PreviewKeyDownEventHandler(control_PreviewKeyDown);

                Button btn = control as Button;
                if (btn != null)
                {
                    btn.MouseMove += (o, args) => { ButtonMouseMove(btn); };
                    btn.TabStop = false;
                }
            }
        }

        private void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;
            }
        }

        private Stream SoundSelection()
        {
            int num = 0;
            Stream sound = global::EscapingButtonSpace.Properties.Resources.Windows_Hardware_Insert;
            Random rand = new Random();
            num = rand.Next(0, 7);
            switch (num)
            {
                case 0:
                    sound = global::EscapingButtonSpace.Properties.Resources.rabbit_scream_1;
                    break;
                case 1:
                    sound = global::EscapingButtonSpace.Properties.Resources.rabbit_scream_2;
                    break;
                case 2:
                    sound = global::EscapingButtonSpace.Properties.Resources.rabbit_scream_3;
                    break;
                case 3:
                    sound = global::EscapingButtonSpace.Properties.Resources.rabbit_scream_4;
                    break;
                case 4:
                    sound = global::EscapingButtonSpace.Properties.Resources.rabbit_scream_short_1;
                    break;
                case 5:
                    sound = global::EscapingButtonSpace.Properties.Resources.rabbit_scream_short_2;
                    break;
                case 6:
                    sound = global::EscapingButtonSpace.Properties.Resources.rabbit_scream_short_3;
                    break;
                case 7:
                    sound = global::EscapingButtonSpace.Properties.Resources.rabbit_scream_short_4;
                    break;
            }
            return sound;
        }

        private void PlaySound()
        {
            try
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(SoundSelection());
                player.Load();
                player.Play();
            }
            catch { }
        }

        private void ButtonMove()
        {
            if (status == false)
            {
                status = true;           
            }
            else
            {
                status = false;
                ButtonRestoreLocation();
            }
        }

        private void ButtonMouseMove(Control control)
        {
            if (status == true)
            {
                foreach (Control controlLab in form.Controls)
                {
                    Label label = controlLab as Label;
                    if (label != null)
                    {
                        if (label.Name == "labelEBS02264465441")
                        {
                            label.Focus();
                        }
                    }
                }
                Random r = new Random();
                control.Left = r.Next(0, form.ClientSize.Width - control.Width);
                control.Top = r.Next(0, form.ClientSize.Height - control.Height);
                PlaySound();
            }
        }

        private void ButtonsSaveLocation()
        {
            foreach (Control control in form.Controls)
            {
                Button bnt = control as Button;
                if (bnt != null)
                {
                    position.X = bnt.Location.X;
                    position.Y = bnt.Location.Y;
                    btnPosition.Add(position);              
                }
            }
        }

        private void ButtonRestoreLocation()
        {
            int i = 0;
            foreach (Control control in form.Controls)
            {
                Button btn = control as Button;
                if (btn != null)
                {
                    btn.Left = (int)btnPosition[i].X;
                    btn.Top = (int)btnPosition[i].Y;
                    i++;
                }
            }
        }

        private void CreateLabel()
        {
            System.Windows.Forms.Label labelEBS02264465441 = new System.Windows.Forms.Label();
            labelEBS02264465441.Location = new System.Drawing.Point(101, 50);
            labelEBS02264465441.Name = "labelEBS02264465441";
            labelEBS02264465441.Size = new System.Drawing.Size(35, 13);
            labelEBS02264465441.TabIndex = 0;
            labelEBS02264465441.Text = "";
            form.Controls.Add(labelEBS02264465441);
        }
    }
}
