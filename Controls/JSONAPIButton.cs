using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JSONAPI.Controls
{
    public class JSONAPIButton : Button
    {
        private int resizeCount = 0;
        public JSONAPIButton()
        {
            this.EnabledChanged += new System.EventHandler(this.JSONAPIButton_EnabledChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.JSONAPIButton_Paint);
            this.MouseHover += new EventHandler(this.JSONAPIButton_OnMouseHover);
            this.MouseLeave += new EventHandler(this.JSONAPIButton_OnMouseLeave);
        }

        private void JSONAPIButton_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.BackColor = btn.Enabled ? Color.CornflowerBlue : Color.Gray;
        }
        
        private void JSONAPIButton_Paint(object sender, PaintEventArgs e)
        {
            var btn = (Button)sender;
            /*var drawBrush = new SolidBrush(btn.ForeColor);
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            string tempString = btn.Text;
            //btn.Text = string.Empty;
            e.Graphics.DrawString(tempString, btn.Font, drawBrush, e.ClipRectangle, sf);
            drawBrush.Dispose();
            sf.Dispose();*/
        }

        protected void JSONAPIButton_OnMouseHover(object sender, System.EventArgs e)
        {
            // Get the font size in Points, add one to the
            // size, and reset the button's font to the larger
            // size.
            if (this.Enabled)
            {
                if (resizeCount == 0)
                {
                    float fontSize = Font.SizeInPoints;
                    fontSize += 1;
                    System.Drawing.Size buttonSize = Size;
                    this.Font = new System.Drawing.Font(
                        Font.FontFamily, fontSize, Font.Style);

                    // Increase the size width and height of the button 
                    // by 2 points each.
                    Size = new System.Drawing.Size(Size.Width + 2, Size.Height + 2);
                    resizeCount = 1;
                }
                // Call myBase.OnMouseHover to activate the delegate.
            }
        }

        protected void JSONAPIButton_OnMouseLeave(object sender, System.EventArgs e)
        {
            // Get the font size in Points, subtract one from the
            // size, and reset the button's font to the smaller
            // size.
            if (this.Enabled)
            {
                if (resizeCount == 1)
                {
                    float fontSize = Font.SizeInPoints;
                    fontSize -= 1;
                    System.Drawing.Size buttonSize = Size;
                    this.Font = new System.Drawing.Font(
                        Font.FontFamily, fontSize, Font.Style);

                    // Decrease the size width and height of the button 
                    // by 2 points each.
                    Size = new System.Drawing.Size(Size.Width - 2, Size.Height - 2);
                    resizeCount = 0;
                }
                // Call myBase.OnMouseHover to activate the delegate.
            }
        }
    }
}
