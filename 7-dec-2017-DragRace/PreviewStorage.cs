using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7_dec_2017_DragRace {
    class PreviewStorage {

        public PreviewStorage() {  }

        public Car carObj { get; set; }
        public Panel previewPnl { get; set; }
        public Label previewName { get; set; }

        public void update() {
            this.previewPnl.BackColor = this.carObj.color;
            this.previewName.Text = this.carObj.name;
        }

    }
}
