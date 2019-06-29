using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CopperHedge
{
    public partial class InstrumentForm : Form
    {
        string _settingsFile = @"C:\HatDev\CopperHedge\Settings.txt";

        public InstrumentForm()
        {
            InitializeComponent();

            ReadSettings();
        }

        public string GetSetting(string columnName, int rowIndex)
        {
            DataGridViewCell cell = instrumentGrid[columnName, rowIndex];
            return cell.Value.ToString();
        }

        public string[] GetSettings(int rowIndex)
        {
            DataGridViewRow row = instrumentGrid.Rows[rowIndex];
            int columnCount = instrumentGrid.Columns.Count;
            string[] settings = new string[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                settings[i] = row.Cells[i].Value.ToString();
            }
            return settings;
        }

        private void InstrumentsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void ReadSettings()
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(_settingsFile))
                {
                    String line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] settings = line.Split(',');
                        instrumentGrid.Rows.Add(settings);
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    } // class
} // namespace
