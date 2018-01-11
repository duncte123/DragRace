using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

            monitor.PrintLn("Setting title");
            this.Text = "Drag Race";
            monitor.PrintLn("Clearing finish text");
            this.lblFinishedCarsDSte.Text = "";
            monitor.PrintLn("Clearing timer text");
            this.lblTimerDSte.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e) {
            monitor.PrintLn("Booting App", false);
            //Hide tabs
            //Remove 3d effects
            this.tabMainDSte.Appearance = TabAppearance.FlatButtons;
            //sets ‘thinness’  
            this.tabMainDSte.ItemSize = new Size(0, 1);
            this.tabMainDSte.SizeMode = TabSizeMode.Fixed;

            monitor.PrintLn("Loading default cars", false);
            monitor.PrintLn("Creating car 1");
            this.cars[0] = new Car("Car 1", Color.Red, this.pnlCar1DSte, randomDSte.Next(3, 7));
            monitor.PrintLn("Creating car 2");
            this.cars[1] = new Car("Car 2", Color.Tomato, this.pnlCar2DSte, randomDSte.Next(3, 7));
            monitor.PrintLn("Creating car 3");
            this.cars[2] = new Car("Car 3", Color.Coral, this.pnlCar3DSte, randomDSte.Next(3, 7));
            monitor.PrintLn("Creating car 4");
            this.cars[3] = new Car("Car 4", Color.Maroon, this.pnlCar4DSte, randomDSte.Next(3, 7));
            monitor.PrintLn("Cars have been loaded");

            #region init car settings
            //Set all the tags of the buttons
            monitor.PrintLn("Preparing settings", false);
            monitor.PrintLn("Creating PreviewStorage for car 1");
            PreviewStorage car1Storage = new PreviewStorage {
                carObj = this.cars[0],
                previewPnl = this.pnlPreviewCar1,
                previewName = this.lblCar1NameDSte
            };
            monitor.PrintLn("Upading car 1");
            car1Storage.update();
            this.btnColorCar1DSte.Tag = car1Storage;
            this.btnNameCar1DSte.Tag = car1Storage;

            monitor.PrintLn("Creating PreviewStorage for car 2");
            PreviewStorage car2Storage = new PreviewStorage {
                carObj = this.cars[1],
                previewPnl = this.pnlPreviewCar2,
                previewName = this.lblCar2NameDSte
            };
            monitor.PrintLn("Upading car 2");
            car2Storage.update();
            this.btnColorCar2DSte.Tag = car2Storage;
            this.btnNameCar2DSte.Tag = car2Storage;

            monitor.PrintLn("Creating PreviewStorage for car 3");
            PreviewStorage car3Storage = new PreviewStorage {
                carObj = this.cars[2],
                previewPnl = this.pnlPreviewCar3,
                previewName = this.lblCar3NameDSte
            };
            monitor.PrintLn("Upading car 2");
            car3Storage.update();
            this.btnColorCar3DSte.Tag = car3Storage;
            this.btnNameCar3DSte.Tag = car3Storage;

            monitor.PrintLn("Creating PreviewStorage for car 4");
            PreviewStorage car4Storage = new PreviewStorage {
                carObj = this.cars[3],
                previewPnl = this.pnlPreviewCar4,
                previewName = this.lblCar4NameDSte
            };
            monitor.PrintLn("Upading car 4");
            car4Storage.update();
            this.btnColorCar4DSte.Tag = car4Storage;
            this.btnNameCar4DSte.Tag = car4Storage;
            monitor.PrintLn("Car settings have been loaded", false);
            #endregion
        }

        private void tmrMoveCarsDSte_Tick(object sender, EventArgs e) {
            monitor.PrintLn("Timer has ticked");
            monitor.PrintLn("Adding one to time");
            this.totalTime++;
            monitor.PrintLn("Setting text");
            this.lblTimerDSte.Text = "Race has been going for: " + this.totalTime + "ms";
            monitor.PrintLn("Calculating finish line");
            int finnishPos = this.pnlFinnishDSte.Left + this.pnlFinnishDSte.Width + 5;
            monitor.PrintLn("Looping over all cars");
            foreach (Car carToMove in this.cars) {
                monitor.PrintLn("Checking if car is finnished");
                if (!(carToMove.carObj.Left >= finnishPos)) {
                    monitor.PrintLn("Checking if car needs new speed");
                    if (carToMove.carObj.Left >= this.pnlNewSpeedTriggerDSte.Left && carToMove.canChangeSpeed) {
                        monitor.PrintLn("Setting new speed", false);
                        carToMove.carSpeed = randomDSte.Next(3, 10);
                    }
                    monitor.PrintLn("Moving car");
                    carToMove.move();
                    monitor.PrintLn("Car has moved");
                } else {
                    monitor.PrintLn("Checking if the car is not finnished");
                    if (!carToMove.finished) {
                        monitor.PrintLn("Set car to finnished");
                        carToMove.carObj.Left = this.pnlFinnishDSte.Left + this.pnlFinnishDSte.Width + 5;
                        carToMove.finished = true;
                        monitor.PrintLn("Log message");
                        monitor.PrintLn("Car " + carToMove.name + " has finished", "G");
                        monitor.PrintLn("Setting finish time");
                        double finishTime = Math.Round((DateTime.Now.TimeOfDay.TotalMilliseconds - startTime) / 1000.0);
                        monitor.PrintLn("Appending text");
                        this.lblFinishedCarsDSte.Text += String.Format(this.finishedTemplate, 
                            carToMove.name, getPos(this.finishedCars.Count), finishTime) + "\n";
                        monitor.PrintLn("Saving cars");
                        this.finishedCars.Add(carToMove);
                    }
                }
            }

            monitor.PrintLn("Checking if all cars are finished");
            if (cars[0].finished && cars[1].finished && cars[2].finished && cars[3].finished) {
                monitor.PrintLn("Stopping timer");
                this.tmrMoveCarsDSte.Stop();
                monitor.PrintLn("Setting running to false");
                this.raceRunning = false;
                monitor.PrintLn("Race has ended", false);
                monitor.Print("First place: ", false);
                monitor.PrintLn(this.finishedCars[0].name, "G", false);
                MessageBox.Show("Race is over");
            }
        }

        /// <summary>
        /// This resets all the cars to the default position
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">the args</param>
        private void btnStartDSte_Click(object sender, EventArgs e) {
            monitor.PrintLn("Clicked the start button", false);
            if (!this.raceRunning) {
                monitor.PrintLn("Starting a race");
                monitor.PrintLn("Resetting total time");
                this.totalTime = 0;
                monitor.PrintLn("Setting running to true");
                this.raceRunning = true;
                monitor.PrintLn("Clearing finnished car text");
                this.lblFinishedCarsDSte.Text = "";
                monitor.PrintLn("Resetting cars");
                this.resetAllCars();
                monitor.PrintLn("Starting timer");
                this.tmrMoveCarsDSte.Start();
                monitor.PrintLn("Setting start time");
                this.startTime = DateTime.Now.TimeOfDay.TotalMilliseconds;
            }
        }

        /// <summary>
        /// This resets all car positions to the default
        /// </summary>
        private void resetAllCars() {
            monitor.PrintLn("Resettings all cars", "R", false);
            this.finishedCars.Clear();
            foreach (Car car in this.cars) {
                monitor.PrintLn("Resetting car " + car.name);
                car.reset(randomDSte.Next(3, 10));
            }
        }

        /// <summary>
        /// returns a string when you input a number (very helpful ik 😛)
        /// </summary>
        /// <param name="pos">The position of the car</param>
        /// <returns>first, second, third or last</returns>
        private String getPos(int pos) {
            monitor.PrintLn("Getting pos from " + pos);
            switch (pos) {
                case 0:
                    monitor.PrintLn("Case 0");
                    return "first";
                case 1:
                    monitor.PrintLn("Case 1");
                    return "second";
                case 2:
                    monitor.PrintLn("Case 2");
                    return "third";
                case 3:
                    monitor.PrintLn("Case 3");
                    return "last";

                default:
                    monitor.PrintLn("Unknown case");
                    return "HOW IS THIS POSSIBLE????";
            }
        }

        private void UpdateCarDSte(object sender, EventArgs e) {
            monitor.PrintLn("Pressed button to set car color");
            monitor.PrintLn("Creating dialog");
            ColorDialog dialog = new ColorDialog();
            monitor.PrintLn("Openeing dialog");
            dialog.ShowDialog();
            monitor.PrintLn("Loading storage");
            PreviewStorage preview = (PreviewStorage)((Button)sender).Tag;
            monitor.PrintLn("Setting color on car");
            preview.carObj.color = dialog.Color;
            monitor.PrintLn("Setting color on preview");
            preview.previewPnl.BackColor = dialog.Color;
            monitor.PrintLn("Color changed");
        }

        private void SetCarName(object sender, EventArgs e) {
            monitor.PrintLn("Pressed button to change car name");
            monitor.PrintLn("Loading storage");
            PreviewStorage preview = (PreviewStorage)((Button)sender).Tag;

            monitor.PrintLn("Creating dialog");
            //Use the visual basic for the input 😛
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                String.Format("Change the name for {0}?", preview.carObj.name), 
                "Change name?", preview.carObj.name, -1, -1);

            monitor.PrintLn("Checking if input is null");
            input = input == null || input == "" ? preview.carObj.name : input;

            monitor.PrintLn("Setting name on car");
            preview.carObj.name = input;
            monitor.PrintLn("Setting name on preview");
            preview.previewName.Text = input;
            monitor.PrintLn("Name changed");
        }

        private void trackToolStripMenuItem_Click(object sender, EventArgs e) {
            monitor.PrintLn("Clicked menu strip 0");
            if (this.raceRunning) return;
            monitor.PrintLn("Changing tab");
            this.tabMainDSte.SelectTab(0);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
            monitor.PrintLn("Clicked menu strip 1");
            if (this.raceRunning) return;
            monitor.PrintLn("Changing tab");
            this.tabMainDSte.SelectTab(1);
        }

        private void cbShowLogDSte_CheckedChanged(object sender, EventArgs e) {
            monitor.PrintLn("Monitor toggled");
            monitor.ToggleView();
        }

    }
}
