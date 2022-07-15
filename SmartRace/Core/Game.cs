using SmartRace.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SmartRace.Utils;
using SmartRace.Listeners;
using System.Timers;

namespace SmartRace.Core
{
    class Game
    {
        Panel Canvas { get; set; }
        Track Track { get; set; }
        List<Car> Cars { get; set; }

        System.Timers.Timer Timer { get; set; } = new();

        Camera Camera { get; set; } = new();

        public Game(Panel canvas)
        {
            Canvas = canvas;
            InitializeCanvas();

            ShowMainMenu();
        }

        private void InitializeCanvas()
        {
            typeof(Panel).InvokeMember(
                "DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                Canvas,
                new object[] { true }
                );

            Canvas.BackColor = Color.Green;
        }

        private void ShowMainMenu()
        {
            MainGameMenu menu = new() { Dock = DockStyle.Fill };
            menu.OnCreateTrackButtonClick += (_,_) =>
            {
                menu.Dispose();
                SetupTrackBuilder();
            };

            Canvas.Controls.Add(menu);
        }

        private void SetupTrackBuilder()
        {
            // Initialize track
            Track = new Track(50);

            Camera.Position = new Vector2(Canvas.Width / 2, Canvas.Height / 2);

            void onCanvasClick(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Middle)
                    return;

                Console.WriteLine(e.Location);
                Console.WriteLine(e.X);
                Console.WriteLine(e.Y);

                Track.TryAddPoint(Camera.ConvertCanvasPositionToGamePosition(Camera.MousePosition));

                Canvas.Invalidate();
            }
            Canvas.MouseUp += onCanvasClick;

            void onCanvasMouseHover(object sender, MouseEventArgs e)
            {
                Camera.MousePosition = new Vector2(e.X, e.Y);
                Canvas.Invalidate();
            }
            Canvas.MouseMove += onCanvasMouseHover;

            void onCanvasPaint(object sender, PaintEventArgs e)
            {
                //using (Brush b = new SolidBrush(Color.Red))
                //    e.Graphics.FillCircle(b, Camera.MousePosition.X, Camera.MousePosition.Y, 10);

                //DrawTrack(e.Graphics, true);
                DrawPreviewTrack(e.Graphics, Camera.MousePosition);
            }
            Canvas.Paint += onCanvasPaint;

            MouseDragEventListener mousedragListener = new(Canvas, e =>
            {
                if (e.Button == MouseButtons.Right)
                    Console.WriteLine(e.PositionDelta);
            });
        }

        private void DrawPreviewTrack(Graphics g, Vector2 mousePosition)
        {
            Track.Draw(g, Camera.Position, Camera.Scale, true);

            Vector2 projectedMousePosition = Camera.ConvertCanvasPositionToGamePosition(mousePosition);

            Track.DrawPreviewConstrainedPoint(g, Camera, projectedMousePosition);
        }




        private void DrawTrack(Graphics g, bool showGuides = true)
        {
            Track.Draw(g, Camera.Position, Camera.Scale, showGuides);
        }






    }
}

//class Game
//{
//    // Setup
//    public enum Phase
//    {
//        Menu,
//        BuildTrack,
//        Setup,
//        Simulation,
//        Breeding
//    }
//    public Phase phase;

//    public enum BuildMode
//    {
//        Spline,
//        FreeDraw,
//        Predefined
//    }
//    public BuildMode buildMode = BuildMode.Spline;


//    // Simulation info
//    public int Generation = 0;
//    public int ticks = 0;
//    public int maxTicks = 150;


//    // Car settings
//    public List<Car> Population = new List<Car>();
//    public int individuals = 30;
//    public int offspring = 3;











//    // Neural net settings
//    public double range = 4;
//    public int layers = 1;
//    public int neurons = 10;
//    public int inputs = 8;
//    public int outputs = 2;
//    public string activation = "softsign";
//    public double mutationRate = .01;

//    // Track settings
//    public Map Map;
//    public int mapResolution = 3;
//    public Track Track;
//    public float TrackWidth = 50;

//    // Display settings
//    public bool showMap = false;
//    public bool displaySensors = false;
//    public bool followBest = true;
//    public Image carImage;
//    public Bitmap carSprite;


//    public Game()
//    {
//        Track = new Track(TrackWidth);
//        carImage = Image.FromFile(Path.GetFullPath("./Images/car.png"));
//        carSprite = new Bitmap(carImage, new Size(20, 10));
//    }

//    public void CreateMap()
//    {
//        Map = new Map(Track, mapResolution);
//    }

//    public void CreateCars()
//    {
//        for (int i = 0; i < individuals; i++)
//            Population.Add(new Car(Track, carSprite));
//    }

//    public void Breed()
//    {
//        Population.Sort((car1, car2) => car2.NN.fitness.CompareTo(car1.NN.fitness));

//        for (int i = 0; i < offspring; i++)
//            Population[individuals - 1 - i].NN = NeuralNet.Breed(Population[i * 2].NN, Population[i * 2 + 1].NN);

//        Population[individuals - 1 - offspring].NN.mutate();
//        Population[individuals - 2 - offspring].NN = new NeuralNet(4, 1, 10, 8, 2, "softsign", .01);


//        foreach (Car car in Population)
//        {
//            car.NN.fitness = 0;
//            car.reset();
//        }

//        ticks = 0;

//        Generation++;
//    }
//}
