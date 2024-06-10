﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PZ_24
{
    public partial class Colors : Form
    {
        static Color colorResult;

        public Colors(Color color)
        {
            InitializeComponent();
            Scroll_Red.Tag = numeric_Red;
            Scroll_Green.Tag = numeric_Green;
            Scroll_Blue.Tag = numeric_Blue;
            numeric_Red.Tag = Scroll_Red;
            numeric_Green.Tag = Scroll_Green;
            numeric_Blue.Tag = Scroll_Blue;
            numeric_Red.Value = color.R;
            numeric_Green.Value = color.G;
            numeric_Blue.Value = color.B;
        }

        private void Colors_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void Scroll_Red_ValueChanged(object sender, EventArgs e)
        {
            ScrollBar scrollBar = (ScrollBar)sender;
            NumericUpDown numericUpDown = (NumericUpDown)scrollBar.Tag;
            numericUpDown.Value = scrollBar.Value;
            UpdateColor();
        }
        private void numeric_Red_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            ScrollBar scrollBar = (ScrollBar)numericUpDown.Tag;
            scrollBar.Value = (int)numericUpDown.Value;
            UpdateColor();
        }
        private void Scroll_Green_ValueChanged(object sender, EventArgs e)
        {
            ScrollBar scrollBar = (ScrollBar)sender;
            NumericUpDown numericUpDown = (NumericUpDown)scrollBar.Tag;
            numericUpDown.Value = scrollBar.Value;
            UpdateColor();
        }
        private void numeric_Green_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            ScrollBar scrollBar = (ScrollBar)numericUpDown.Tag;
            scrollBar.Value = (int)numericUpDown.Value;
            UpdateColor();
        }
        private void Scroll_Blue_ValueChanged(object sender, EventArgs e)
        {
            ScrollBar scrollBar = (ScrollBar)sender;
            NumericUpDown numericUpDown = (NumericUpDown)scrollBar.Tag;
            numericUpDown.Value = scrollBar.Value;
            UpdateColor();
        }
        private void numeric_Blue_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            ScrollBar scrollBar = (ScrollBar)numericUpDown.Tag;
            scrollBar.Value = (int)numericUpDown.Value;
            UpdateColor();
        }
        private void UpdateColor()
        {
            colorResult = Color.FromArgb(Scroll_Red.Value, Scroll_Green.Value, Scroll_Blue.Value);
            pictureBox1.BackColor = colorResult;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Scroll_Red.Value = colorDialog.Color.R;
                Scroll_Green.Value = colorDialog.Color.G;
                Scroll_Blue.Value = colorDialog.Color.B;
                colorResult = colorDialog.Color;
                UpdateColor();
            }
        }

        private void buttonOther_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Scroll_Red.Value = colorDialog.Color.R;
                Scroll_Green.Value = colorDialog.Color.G;
                Scroll_Blue.Value = colorDialog.Color.B;
                colorResult = colorDialog.Color;
                UpdateColor();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.currentPen.Color = colorResult;
            Close();
        }
    }
}