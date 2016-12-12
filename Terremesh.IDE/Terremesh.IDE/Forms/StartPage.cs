using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Terremesh.IDE.Forms
{
    public partial class StartPage : DocumentBase
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void StartPage_Load(object sender, EventArgs e)
        {
            chb_ShowOnStartup.Checked = m_ShowOnStartup;
        }

        public static bool ShowOnStartup
        {
            get
            {
                return m_ShowOnStartup;
            }
            set
            {
                m_ShowOnStartup = value;
            }
        }

        private static bool m_ShowOnStartup = true;

        private void chb_ShowOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            ShowOnStartup = chb_ShowOnStartup.Checked;
        }

        private void authorLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel label = (LinkLabel)sender;
            try
            {
                System.Diagnostics.Process.Start(label.Tag as string);
            }
            catch
            {
            }
        }
    }
}
