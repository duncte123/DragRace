using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7_dec_2017_DragRace {
    public partial class SerialMonitor : Form {

        private RichTextBox theBox;

        public SerialMonitor() {
            InitializeComponent();
            this.theBox = this.rtbLogDSte;
            this.TopMost = true;
        }

        /// <summary>
        /// This toggles if the monitor is shown on the screen
        /// </summary>
        public void ToggleView() {
            this.Visible = !this.Visible;
        }

        /// <summary>
        /// This prints a message with a new line in the monitor
        /// </summary>
        /// <param name="a_text">the text</param>
        /// <param name="a_color">the color</param>
        public void PrintLn(Object a_text, String a_color) {
            this.Print(a_text + "\n", a_color);
        }

        /// <summary>
        /// This prints a message in the monitor
        /// </summary>
        /// <param name="a_text">the text</param>
        /// <param name="a_color">the color</param>
        public void Print(Object a_text, String a_color) {

            switch (a_color.ToUpper()) {
                case "R": theBox.SelectionColor = Color.Red; break;
                case "G": theBox.SelectionColor = Color.Green; break;
                case "B": theBox.SelectionColor = Color.Blue; break;
                default: theBox.SelectionColor = Color.White; break;
            }

            Debug.Write(a_text);

            theBox.AppendText(Convert.ToString(a_text));
            theBox.ScrollToCaret();
        }

        /// <summary>
        /// This prints a message with a new line in the monitor
        /// </summary>
        /// <param name="a_text">the text to print</param>
        public void PrintLn(Object a_text) {
            PrintLn(a_text, "W");
        }

        /// <summary>
        /// This prints a message in the monitor
        /// </summary>
        /// <param name="a_text">the text to print</param>
        public void Print(Object a_text) {
            Print(a_text, "W");
        }

        /// <summary>
        /// This clears the monitor
        /// </summary>
        public void Clear() {
            theBox.Clear();
            theBox.ScrollToCaret();
            PrintLn("Cleared");
            theBox.ScrollToCaret();
        }

        private void btnClearDSte_Click(object sender, EventArgs e) {
            this.Clear();
        }
    }
}
