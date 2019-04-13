using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL;
using BL.Models;
using UI.Resources;

namespace UI
{
    public partial class Form1 : Form
    {
        List<ClosestAgent> _list = new List<ClosestAgent>();

        public Form1()
        {
            InitializeComponent();
            //webBrowser1.ScriptErrorsSuppressed = true;

            // hide the map till user clicks the button to show it loaded with data.
            webBrowser1.Hide();

            //
            // Init the DGV to suits our needs 
            dgvAgents.AutoGenerateColumns = false;
            dgvAgents.ColumnCount = 2;
            dgvAgents.Columns[0].HeaderText = "Name";
            dgvAgents.Columns[0].DataPropertyName = "Name";
            dgvAgents.Columns[1].HeaderText = "Distance(meters)";
            dgvAgents.Columns[1].DataPropertyName = "Distance";

            GetAgents();
        }

        private void GetAgents()
        {
            try
            {
                BLClass bl = new BLClass();

                _list = bl.GetCloestAgent();

                // used for extracting the data that we need to string format
                // var combined = string.Join(",", list.Select(c => c.Name.ToString() + " " + c.ToLat.ToString() + " " + c.ToLng.ToString()));

                dgvAgents.DataSource = _list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.ToString());
            }
        }

        private void btnShowOnMap_Click(object sender, EventArgs e)
        {
            try
            {
                string curDir = Directory.GetCurrentDirectory();

                var json = GoogleHelpers.CreateJson(_list);
                this.webBrowser1.DocumentText = GoogleHtml.GeoJsonHtml.Replace("{GEO_JSON}", json);
                //this.webBrowser1.Url = new Uri("http://localhost/googleMap");
                // 
                // Show the map
                webBrowser1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.ToString());
            }

        }
    }
}
