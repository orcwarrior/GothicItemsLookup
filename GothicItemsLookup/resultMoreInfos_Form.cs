using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GothicItemsLookup
{
    public partial class resultMoreInfos_Form : Form
    {
        List<Label> labels = new List<Label>();
        public resultMoreInfos_Form(List<string> atribs,List<string> values)
        {
            InitializeComponent();
            InitializeLabels(atribs, values);
        }

        private void InitializeLabels(List<string> atribs, List<string> values)
        {
            Point labelStart_Atr = new Point(10, 5);
            Point labelStart_Val = new Point(170, 5);
            Point labelMaxPos = new Point(0, 0);
            int labelYSpace = 20;
            for (int i = 0; i < atribs.Count; i++)
            {
                Label atr = new Label();
                atr.Text = atribs[i];
                atr.Location = new Point(labelStart_Atr.X,labelStart_Atr.Y +labelYSpace * i);
                atr.Size = new Size(150, atr.Size.Height);
                atr.TextAlign = ContentAlignment.MiddleLeft;

                Label val = new Label();
                val.Text = (i<=values.Count)?values[i]:"BRAK";
                val.Location = labelStart_Val;
                val.Location = new Point(labelStart_Val.X, labelStart_Val.Y + labelYSpace * i);
                val.Size = new Size(420, val.Size.Height);
                val.TextAlign = ContentAlignment.MiddleLeft;
                if (val.Text.Length > 65)
                {
                    val.TextAlign = ContentAlignment.TopLeft;
                    val.Text = val.Text.Insert(65, "\n");
                    val.Size = new Size(val.Size.Width,val.Size.Height+ 5);
                }
                labelMaxPos = val.Location;
                labels.Add(atr);
                labels.Add(val);
                this.Controls.Add(labels[labels.Count - 2]);
                this.Controls.Add(labels[labels.Count - 1]);
            }
            this.Size = new Size(labelMaxPos.X + 450, labelMaxPos.Y + 80);
            this.btnClose.Location = new Point(btnClose.Location.X, labelMaxPos.Y + 25);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
