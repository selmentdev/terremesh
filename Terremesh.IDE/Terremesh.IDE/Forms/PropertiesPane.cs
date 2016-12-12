using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Terremesh.IDE.Forms
{
    public partial class PropertiesPane : Terremesh.IDE.Forms.PaneBase
    {
        /// <summary>
        /// Create new PropertiesPane
        /// </summary>
        public PropertiesPane()
        {
            InitializeComponent();
        }

        public void RegisterObject(object o)
        {
            objectsList.Items.Add(o);
            if (objectsList.Items.Count == 1)
            {
                objectsList.SelectedIndex = 0;
            }
        }

        public void UnregisterObject(object o)
        {
            objectsList.Items.Remove(o);
        }

        private void objectsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objectsList.Items.Count != 0)
            {
                propertyGrid1.SelectedObject = objectsList.Items[objectsList.SelectedIndex];
            }
        }
    }
}
