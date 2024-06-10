using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PZ_24
{
    public partial class Form1 : Form
    {
        bool drawing;
        int historyCounter; //Счетчик истории 
        GraphicsPath currentPath;
        Point oldLocation;
        static public Pen currentPen;
        Color historyColor; //Сохранение текущего цвета перед использованием ластика 
        List<Image> History; //Список для истории \


        public Form1()
        {
            InitializeComponent();
            drawing = false;
            currentPen = new Pen(Color.Black);
            currentPen.Width = trackBar1.Value;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void saveF2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDlg = new SaveFileDialog();
            SaveDlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            SaveDlg.Title = "Save an Image File";
            SaveDlg.FilterIndex = 4;
            SaveDlg.ShowDialog();
            

            if (SaveDlg.FileName != "") //Если введено не пустое имя 
            {
                System.IO.FileStream fs =
                (System.IO.FileStream)SaveDlg.OpenFile();
                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        this.picDrawingSurface.Image.Save(fs, ImageFormat.Jpeg);
                        break;
                    case 2:
                        this.picDrawingSurface.Image.Save(fs, ImageFormat.Bmp);
                        break;
                    case 3:
                        this.picDrawingSurface.Image.Save(fs, ImageFormat.Gif);
                        break;
                    case 4:
                        this.picDrawingSurface.Image.Save(fs, ImageFormat.Png);
                        break;
                }
                fs.Close();
            }

        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (solidToolStripMenuItem.Checked == true)
            {
                solidToolStripMenuItem.Checked = false;
            }
            else
            {
                solidToolStripMenuItem.Checked = true;
                dotToolStripMenuItem.Checked = false;
                dashDotDotToolStripMenuItem.Checked = false;
            }
            currentPen.DashStyle = DashStyle.Solid;
        }

        private void penToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dotToolStripMenuItem.Checked == true)
            {
                dotToolStripMenuItem.Checked = false;
            }
            else
            {
                dotToolStripMenuItem.Checked = true;
                solidToolStripMenuItem.Checked = false;
                dashDotDotToolStripMenuItem.Checked = false;
            }
            currentPen.DashStyle = DashStyle.Dot;
        }

        private void dashDotDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dashDotDotToolStripMenuItem.Checked == true)
            {
                dashDotDotToolStripMenuItem.Checked = false;
            }
            else
            {
                dashDotDotToolStripMenuItem.Checked = true;
                solidToolStripMenuItem.Checked = false;
                dotToolStripMenuItem.Checked = false;
            }
            currentPen.DashStyle = DashStyle.DashDotDot;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("v6.66\npaint\nby oshepkov vladislav");
        }

        private void picDrawingSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (picDrawingSurface.Image == null)
            {
                MessageBox.Show("Сначала создайте новый файл!");
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
            }
            if (e.Button == MouseButtons.Right)
            {
                historyColor = currentPen.Color;
                currentPen.Color = Color.White;
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
            }
        }

        private void newCtrlNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (picDrawingSurface.Image != null)
            {
                var result = MessageBox.Show("Сохранить текущее изображение перед созданием нового рисунка?", "Предупреждение", MessageBoxButtons.YesNoCancel);

                switch (result)
                {
                    case DialogResult.No: break;
                    case DialogResult.Yes: saveF2ToolStripMenuItem_Click(sender, e); break;
                    case DialogResult.Cancel: return;
                }
            }
            History = new List<Image>();
            History.Clear();
            historyCounter = 0;
            Bitmap pic = new Bitmap(686, 469);
            picDrawingSurface.Image = pic;
            Graphics g = Graphics.FromImage(picDrawingSurface.Image);
            g.Clear(Color.White);
            g.DrawImage(picDrawingSurface.Image, 0, 0, 686, 469);
            History.Add(new Bitmap(picDrawingSurface.Image));
            
        }

        private void openF3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OP = new OpenFileDialog();
            OP.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            OP.Title = "Open an Image File";
            OP.FilterIndex = 1; 
            if (OP.ShowDialog() != DialogResult.Cancel)
            picDrawingSurface.Load(OP.FileName);
            picDrawingSurface.AutoSize = true;

        }

        private void picDrawingSurface_MouseUp(object sender, MouseEventArgs e)
        {
            History.RemoveRange(historyCounter + 1, History.Count - historyCounter - 1);
            History.Add(new Bitmap(picDrawingSurface.Image));
            if (historyCounter + 1 < 10) historyCounter++;
            if (History.Count - 1 == 10) History.RemoveAt(0);

            drawing = false;
            try
            {
                currentPath.Dispose();
            }
            catch { };

            if (e.Button == MouseButtons.Right)
            {
                currentPen.Color = historyColor;
            }
        }

        private void picDrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            label_XY.Text = e.X.ToString() + ", " + e.Y.ToString();
            if (drawing)
            {
                Graphics g = Graphics.FromImage(picDrawingSurface.Image);
                currentPath.AddLine(oldLocation, e.Location);
                g.DrawPath(currentPen, currentPath);
                oldLocation = e.Location;
                g.Dispose();
                picDrawingSurface.Invalidate();
            }     
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            currentPen.Width = trackBar1.Value;
        }

        private void undoCtrlZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (History.Count != 0 && historyCounter != 0)
            {
                picDrawingSurface.Image = new Bitmap(History[--historyCounter]);
            }
            else MessageBox.Show("История пуста");

        }

        private void renoCtrlShiftZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (historyCounter < History.Count - 1)
            {
                picDrawingSurface.Image = new Bitmap(History[++historyCounter]);
            }
            else MessageBox.Show("История пуста");
        }

        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Color c  = Color.White;
            Colors af = new Colors(c);
            af.Owner = this;
            af.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Bitmap pic = new Bitmap(686, 469);
            picDrawingSurface.Image = pic;
            Graphics g = Graphics.FromImage(picDrawingSurface.Image);
            g.Clear(Color.White);
            g.DrawImage(picDrawingSurface.Image, 0, 0, 750, 500);

        }
    }
}
