using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GothicItemsLookup.Results;
using LogSys;

namespace GothicItemsLookup
{
    public partial class mapPreview : Form
    {
        // TODO Ładnie by było stworzyć jakąś klase obslugiwanych map
        // z opdowiednimi atrybutami i odwolywac sie do nich przez liste
        // ów map.
        private ICollection<searchResult> ptsCollection;
        public mapPreview(ICollection<searchResult> col)
        {
            InitializeComponent();
            ptsCollection = col;
            mapOrginalSize = mapPic.Size;
            formOrginalSize = this.Size;
        }
        Size formOrginalSize;
        Size mapOrginalSize;
        SizeF mapScale;
        Point mapCenter = new Point(435, 435);
        Size mapPtsToPX = new Size(-118, 174);// x=118, y=162 per pixel
        private void mapPic_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            foreach (searchResult res in ptsCollection)
            {
                if (res is zenSearchResult)
                {
                    zenSearchResult zenRes = res as zenSearchResult;
                    // Jesli nie world.zen to nie rysujemy :)
                    if (zenRes.src.file.IndexOf("WORLD.ZEN") >= 0)
                    {
                        Point pos = new Point(zenRes.pos[0] / mapPtsToPX.Height, // x (on map))
                                              zenRes.pos[2] / mapPtsToPX.Width); // y (on map)
                        pos.Offset(mapCenter);
                        Brush b = new SolidBrush(zenRes.myColor);
                        Pen p = new Pen(b, 4 + Math.Min(zenRes.amount / 2, 15) +     // zwieksz pkt zaleznie od il. itemów
                            ((zenRes.type == Scanners.findResultType.ITEM) ? 3 : 0));// zwieksz jeśli jest pojedynczym itemem
                        e.Graphics.DrawEllipse(p, new Rectangle(pos, new Size(2, 2)));
                        e.Graphics.FillEllipse(new SolidBrush(Color.Black), new Rectangle(pos, new Size(2, 2)));
                        new LogMsg("Map-Preview: new point: ("+pos.X + ", " + pos.Y + ") amount: "+zenRes.amount, eDebugMsgLvl.INFO);
                        
                        e.Graphics.DrawString("P(" + pos.X + "," + pos.Y + ")", new System.Drawing.Font("Arial", 15), b, new PointF(10, 10));
                    }
                }
            }
        }

        private void mapPreview_SizeChanged(object sender, EventArgs e)
        {
            ///Size formDiff = this.Size;
            ///formDiff -= formOrginalSize;
            ///mapPic.Size -= formDiff;
            mapScale = new SizeF((mapPic.Width + 0.0001f) / mapOrginalSize.Width,
                                 (mapPic.Height + 0.0001f) / mapOrginalSize.Height);
            //mapPic.Scale(mapScale);
        }
    }
}
