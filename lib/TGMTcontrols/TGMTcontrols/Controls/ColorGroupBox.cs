using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TGMTcontrols.Controls
{
    public class ColorGroupBox : GroupBox
    {
        Color m_borderColor = Color.FromArgb(92, 133, 200);
        Color m_insideColor = Color.FromArgb(232, 240, 250);
        GraphicsPath shape;
        int radius = 8;
        int m_thickness = 1;

        //int margin = 10;

        public ColorGroupBox()
        {
            this.BackColor = Color.FromArgb(232, 240, 250);
            this.ForeColor = Color.FromArgb(21, 66, 139);
            this.Paint += GroupBox_Paint;
            this.Margin = new Padding(3, 8, 3, 3);
            this.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Regular);
        }

        private void GroupBox_Paint(object sender, PaintEventArgs e)
        {
            GroupBox box = sender as GroupBox;
            if (box == null) return;

            e.Graphics.Clear(this.BackColor);            
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            int width = this.Width - this.Margin.Right;
            int height = this.Height - this.Margin.Bottom;
            shape = new RoundedRectangleF(width, height, radius, this.Margin.Left, this.Margin.Top).Path;

            // Draw the original border
            using (Pen pen = new Pen(m_borderColor, m_thickness))
            {
                e.Graphics.DrawPath(pen, shape);
                e.Graphics.FillPath(new SolidBrush(m_insideColor), shape);
            }

            // Draw the text with a custom color
            if(box.Text != "")
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(21, 66, 139))) // Set text color here
                {
                    //Font font = new Font("Segoe UI", 10, FontStyle.Bold);
                    SizeF textSize = e.Graphics.MeasureString(box.Text, this.Font);
                    int textX = 10; // Adjusted to align with original text position
                    int textY = 0;  // Keep it above the border
                    e.Graphics.FillRectangle(new SolidBrush(m_insideColor), textX - 2, textY, textSize.Width + 4, textSize.Height + 10);
                    e.Graphics.DrawString(box.Text, this.Font, brush, 10, 0);
                }
            }            
        }

        public Color BorderColor
        {
            get
            {
                return m_borderColor;
            }
            set
            {
                m_borderColor = value;
                Invalidate();
            }
        }
        public Color InsideColor
        {
            get
            {
                return m_borderColor;
            }
            set
            {
                m_borderColor = value;
                Invalidate();
            }
        }

        public int Radius
        {
            get
            {
                return radius;
            }
            set
            {
                this.radius = value;
                Invalidate();
            }
        }

        public int BorderThickness
        {
            get
            {
                return m_thickness;
            }
            set
            {
                this.m_thickness = value;
                Invalidate();
            }
        }
    }
}
