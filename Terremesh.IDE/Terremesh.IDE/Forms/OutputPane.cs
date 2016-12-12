using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Terremesh.IDE.Forms
{
    public partial class OutputPane : PaneBase
    {
        private class Listener : TraceListener
        {
            public Listener(OutputPane pane)
            {
                m_Pane = pane;
            }

            public override void Write(string message)
            {
                if (!m_Pane.m_OutputLines.IsDisposed)
                {
                    m_Pane.m_OutputLines.AppendText(message);
                }
            }

            delegate void AssignTextDelegate (string msg);

            public override void WriteLine(string message)
            {
                if (!m_Pane.m_OutputLines.IsDisposed && message != null)
                {
#if false
                    if (m_Pane.m_OutputLines.InvokeRequired)
                    {
                        AssignTextDelegate d = delegate(string msg)
                        {
                            m_Pane.m_OutputLines.AppendText(msg);
                            m_Pane.m_OutputLines.AppendText(Environment.NewLine);
                        };
                        m_Pane.m_OutputLines.Invoke(d, new object[] { message });
                    }
                    else
#endif
                    {
                        m_Pane.m_OutputLines.AppendText(message);
                        m_Pane.m_OutputLines.AppendText(Environment.NewLine);
                    }
                }
            }
            private OutputPane m_Pane;
        }

        private Listener m_Listener = null;

        public OutputPane()
        {
            InitializeComponent();
            m_Listener = new Listener(this);

            System.Diagnostics.Trace.Listeners.Add(m_Listener);

            Trace.WriteLine("Output pane created");
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_OutputLines.Copy();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_OutputLines.Clear();
        }

        private void tsbClearAll_Click(object sender, EventArgs e)
        {
            m_OutputLines.Clear();
        }
    }
}
