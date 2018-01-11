using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7_dec_2017_DragRace {
    class Car {

        private Color carColor;
        private Panel carPanel;
        private int speed;
        private Boolean speedChangedOnece = false;

        /// <summary>
        /// This consructor creates a car with all it's properties
        /// </summary>
        /// <param name="a_name">The cars name</param>
        /// <param name="a_color">The color that the car needs to have</param>
        /// <param name="a_panel">The actual car, I'm using panels for the car and the best way is to sore them in the class</param>
        public Car(String a_name, Color a_color, Panel a_panel, int a_speed) {
            this.name = a_name;
            this.carColor = a_color;
            this.carPanel = a_panel;
            this.speed = a_speed;
            this.finished = false;

            this.carPanel.BackColor = this.carColor;
        }
        
        /// <summary>
        /// Holds the color that the car has
        /// </summary>
        public Color color {
            get => this.carColor;
            set {
                this.carColor = value;
                this.carPanel.BackColor = this.carColor;
            }
        }

        /// <summary>
        /// Holds the name that the car has
        /// </summary>
        public String name { get; set; }

        /// <summary>
        /// This holds the panel that represents the car
        /// </summary>
        public Panel carObj {
            get => this.carPanel;
        }

        /// <summary>
        /// This tells the car if it is finished or not
        /// </summary>
        public Boolean finished { get; set; }

        public int carSpeed {
            get => this.speed;
            set {
                if(!this.speedChangedOnece) {
                    this.speed = value;
                    this.speedChangedOnece = true;
                }
            }
        }

        /// <summary>
        /// This sets the cars position back to 3
        /// I've picked 3 because that is the left position that they are on in the form
        /// </summary>
        public void reset() {
            this.carPanel.Left = 3;
            this.finished = false;
            this.speedChangedOnece = false;
            this.speed = 0;
        }

        /// <summary>
        /// This sets the cars position back to 3
        /// I've picked 3 because that is the left position that they are on in the form
        /// </summary>
        /// <param name="newSpeed">Sets the new speed that the car should have on start</param>
        public void reset(int newSpeed) {
            reset();
            this.speed = newSpeed;
        }

        public void move() {
            if(!this.finished) {
                for(int i = 0; i <= this.speed; i++) {
                    this.carPanel.Left++;
                }
            }
        }

    }
}
