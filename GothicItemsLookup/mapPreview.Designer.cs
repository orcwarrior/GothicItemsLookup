namespace GothicItemsLookup
{
    partial class mapPreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mapPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mapPic)).BeginInit();
            this.SuspendLayout();
            // 
            // mapPic
            // 
            this.mapPic.Image = global::GothicItemsLookup.Resource1.MAP_WORLD;
            this.mapPic.Location = new System.Drawing.Point(-2, 0);
            this.mapPic.Name = "mapPic";
            this.mapPic.Size = new System.Drawing.Size(847, 878);
            this.mapPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mapPic.TabIndex = 0;
            this.mapPic.TabStop = false;
            this.mapPic.Paint += new System.Windows.Forms.PaintEventHandler(this.mapPic_Paint);
            // 
            // mapPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(843, 681);
            this.Controls.Add(this.mapPic);
            this.Name = "mapPreview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mapPreview";
            this.SizeChanged += new System.EventHandler(this.mapPreview_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.mapPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mapPic;
    }
}