using GothicItemsLookup.Results;
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
    public partial class ChangeInstanceMsgBox : Form
    {
        static public string choosenID { get; private set; }
        public ChangeInstanceMsgBox()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            choosenID = "";
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            choosenID = inpNewInst.Text.Trim();
            this.Close();
        }

        private static ChangeInstanceMsgBox _iBox;
        public static new DialogResult Show()
        {
            if (_iBox == null)
            {
                _iBox = new ChangeInstanceMsgBox();
            }

            _iBox.ShowInTaskbar = false;
            _iBox.FormBorderStyle = FormBorderStyle.FixedSingle;
            return _iBox.ShowDialog();
        }
    }
}
