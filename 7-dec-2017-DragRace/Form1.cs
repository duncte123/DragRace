using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7_dec_2017_DragRace {
    public partial class Form1 : Form {

        private Car[] cars = new Car[4];
        private List<Car> finishedCars = new List<Car>();
        private Random randomDSte = new Random();
        private String finishedTemplate = "Car {0} has finished {1} in {2} seconds.";
        private double startTime = 0;
        private double totalTime = 0;
        private Boolean raceRunning = false;
        private SerialMonitor monitor = new SerialMonitor();

        public Form1() {
            InitializeComponent();

            this.Text = "Drag Race";
            this.lblFinishedCarsDSte.Text = "";
            this.lblTimerDSte.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e) {
            monitor.PrintLn("Booting App");
            //Hide tabs
            //Remove 3d effects
            this.tabMainDSte.Appearance = TabAppearance.FlatButtons;
            //sets ‘thinness’  
            this.tabMainDSte.ItemSize = new Size(0, 1);
            this.tabMainDSte.SizeMode = TabSizeMode.Fixed;

            monitor.PrintLn("Loading default cars");
            this.cars[0] = new Car("Car 1", Color.Red, this.pnlCar1DSte, randomDSte.Next(3, 7));
            this.cars[1] = new Car("Car 2", Color.Tomato, this.pnlCar2DSte, randomDSte.Next(3, 7));
            this.cars[2] = new Car("Car 3", Color.Coral, this.pnlCar3DSte, randomDSte.Next(3, 7));
            this.cars[3] = new Car("Car 4", Color.Maroon, this.pnlCar4DSte, randomDSte.Next(3, 7));
            monitor.PrintLn("Cars have been loaded");

            #region init car settings
            //Set all the tags of the buttons
            monitor.PrintLn("Preparing settings");
            PreviewStorage car1Storage = new PreviewStorage {
                carObj = this.cars[0],
                previewPnl = this.pnlPreviewCar1,
                previewName = this.lblCar1NameDSte
            };
            car1Storage.update();
            this.btnColorCar1DSte.Tag = car1Storage;
            this.btnNameCar1DSte.Tag = car1Storage;

            PreviewStorage car2Storage = new PreviewStorage {
                carObj = this.cars[1],
                previewPnl = this.pnlPreviewCar2,
                previewName = this.lblCar2NameDSte
            };
            car2Storage.update();
            this.btnColorCar2DSte.Tag = car2Storage;
            this.btnNameCar2DSte.Tag = car2Storage;

            PreviewStorage car3Storage = new PreviewStorage {
                carObj = this.cars[2],
                previewPnl = this.pnlPreviewCar3,
                previewName = this.lblCar3NameDSte
            };
            car3Storage.update();
            this.btnColorCar3DSte.Tag = car3Storage;
            this.btnNameCar3DSte.Tag = car3Storage;

            PreviewStorage car4Storage = new PreviewStorage {
                carObj = this.cars[3],
                previewPnl = this.pnlPreviewCar4,
                previewName = this.lblCar4NameDSte
            };
            car4Storage.update();
            this.btnColorCar4DSte.Tag = car4Storage;
            this.btnNameCar4DSte.Tag = car4Storage;
            monitor.PrintLn("Car settings have been loaded");
            #endregion
        }

        private void tmrMoveCarsDSte_Tick(object sender, EventArgs e) {
            this.totalTime++;
            this.lblTimerDSte.Text = "Race has been going for: " + this.totalTime + "ms";
            int finnishPos = this.pnlFinnishDSte.Left + this.pnlFinnishDSte.Width + 5;
            foreach (Car carToMove in this.cars) {
                if (!(carToMove.carObj.Left >= finnishPos)) {
                    if(carToMove.carObj.Left >= this.pnlNewSpeedTriggerDSte.Left) {
                        carToMove.carSpeed = randomDSte.Next(3, 10);
                    }
                    carToMove.move();
                } else {
                    if (!carToMove.finished) {
                        carToMove.finished = true;
                        monitor.PrintLn("Car " + carToMove.name + " has finished", "G");
                        double finishTime = Math.Round((DateTime.Now.TimeOfDay.TotalMilliseconds - startTime) / 1000.0);
                        this.lblFinishedCarsDSte.Text += String.Format(this.finishedTemplate, 
                            carToMove.name, getPos(this.finishedCars.Count), finishTime) + "\n";
                        this.finishedCars.Add(carToMove);
                    }
                }
            }

            if(cars[0].finished && cars[1].finished && cars[2].finished && cars[3].finished) {
                this.tmrMoveCarsDSte.Stop();
                this.raceRunning = false;
                monitor.PrintLn("Race has ended");
                monitor.Print("First place: ");
                monitor.PrintLn(this.finishedCars[0].name, "G");
                MessageBox.Show("Race is over");
            }
        }

        /// <summary>
        /// This resets all the cars to the default position
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">the args</param>
        private void btnStartDSte_Click(object sender, EventArgs e) {
            if (!this.raceRunning) {
                monitor.PrintLn("Starting a race");
                this.totalTime = 0;
                this.raceRunning = true;
                this.lblFinishedCarsDSte.Text = "";
                this.resetAllCars();
                this.tmrMoveCarsDSte.Start();
                this.startTime = DateTime.Now.TimeOfDay.TotalMilliseconds;
            }
        }

        /// <summary>
        /// This resets all car positions to the default
        /// </summary>
        private void resetAllCars() {
            monitor.PrintLn("Resettings all cars", "R");
            this.finishedCars.Clear();
            foreach (Car car in this.cars) {
                car.reset(randomDSte.Next(3, 10));
            }
        }

        /// <summary>
        /// returns a string when you input a number (very helpful ik 😛)
        /// </summary>
        /// <param name="pos">The position of the car</param>
        /// <returns>first, second, third or last</returns>
        private String getPos(int pos) {
            switch(pos) {
                case 0:
                    return "first";
                case 1:
                    return "second";
                case 2:
                    return "third";
                case 3:
                    return "last";

                default:
                    return "HOW IS THIS POSSIBLE????";
            }
        }

        private void UpdateCarDSte(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            dialog.ShowDialog();
            PreviewStorage preview = (PreviewStorage)((Button)sender).Tag;
            preview.carObj.color = dialog.Color;
            preview.previewPnl.BackColor = dialog.Color;
        }

        private void SetCarName(object sender, EventArgs e) {
            PreviewStorage preview = (PreviewStorage)((Button)sender).Tag;

            //Use the visual basic for the input 😛
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                String.Format("Change the name for {0}?", preview.carObj.name), 
                "Change name?", preview.carObj.name, -1, -1);

            input = input == null || input == "" ? preview.carObj.name : input;

            preview.carObj.name = input;
            preview.previewName.Text = input;
        }

        private void trackToolStripMenuItem_Click(object sender, EventArgs e) {
            if (this.raceRunning) return;
            this.tabMainDSte.SelectTab(0);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (this.raceRunning) return;
            this.tabMainDSte.SelectTab(1);
        }

        private void cbShowLogDSte_CheckedChanged(object sender, EventArgs e) {
            monitor.ToggleView();
        }
    }
}
