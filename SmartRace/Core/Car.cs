using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SmartRace.Core
{
    class Car
    {
        private Random Random { get; set; } = new Random(Guid.NewGuid().GetHashCode());

        // Initial position
        public Vector2 InitialPosition { get; set; }
        public Vector2 InitialDirection { get; set; }

        // Kinectic data
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get => Velocity.Length(); }
        public float SpeedSquared { get => Velocity.LengthSquared(); }

        // The brain
        public NeuralNet NeuralNet { get; set; }

        // a e s t h e t i c s
        public Color Color { get; set; }
        public Bitmap Sprite { get; set; }

        //public Vector2[] sensors;

        public Car(Vector2 initialPosition, Vector2 initialDirection)
        {
            // Saves the initial position
            InitialPosition = new Vector2(initialPosition.X, initialPosition.Y);
            InitialDirection = new Vector2(initialDirection.X, initialDirection.Y);

            // Stores it to current position
            Position = new Vector2(InitialPosition.X, InitialPosition.Y);
            Direction = new Vector2(InitialDirection.X, InitialDirection.Y);
            Velocity = new Vector2(0f, 0f);

            // Sets random car color
            List<KnownColor> colorList = Enum.GetValues(typeof(KnownColor)).Cast<KnownColor>().ToList();
            Color = Color.FromKnownColor(colorList[Random.Next(0, colorList.Count - 1)]);

            // Assign a neural net to the car
            NeuralNet = new NeuralNet(4, 1, 10, 8, 2, ActivationFunction.SoftSign, 0.05f);

            // View sensors data
            //sensors = new Vector2[7];
        }
    }
}

//class Car
//{
//    public Car()
//    {

//    }

//    // Random number generator
//    private Random random = new Random((int)DateTime.Now.Ticks);
//    //private Random random = new Random(Guid.NewGuid().GetHashCode());

//    // Initial position
//    public Vector2 pos0;
//    public float dir0;

//    // Kinectic data
//    public Vector2 pos;
//    public float spd;
//    public float acc;
//    public float dir;

//    // Neural net
//    public NeuralNet NN;

//    // a e s t h e t i c s
//    public Color color;
//    public Bitmap sprite;

//    public Vector2[] sensors;

//    public Car(float startX, float startY, float startDir)
//    {
//        // Saves the initial position
//        this.pos0 = new Vector2(startX, startY);
//        this.dir0 = startDir;

//        // Stores it to current position
//        pos = new Vector2(pos0.X, pos0.Y);
//        dir = dir0;

//        // Sets random car color
//        List<KnownColor> colorList = Enum.GetValues(typeof(KnownColor)).Cast<KnownColor>().ToList();
//        this.color = Color.FromKnownColor(colorList[random.Next(0, colorList.Count - 1)]);
//        //Console.WriteLine(color.GetHue());

//        // Assign a neural net to the car
//        NN = new NeuralNet(4, 1, 10, 8, 2, "softsign", .05);

//        // View sensors data
//        sensors = new Vector2[7];

//        //float
//    }

//    public Car(Track track, Bitmap carSprite)
//    {
//        // Saves the initial position
//        this.pos0 = Geometry.ToVector2(track.TrackPieces[0].points[0]);
//        this.dir0 = Geometry.Atan2(track.TrackPieces[0].points[1], track.TrackPieces[0].points[0]);

//        // Stores it to current position
//        pos = new Vector2(pos0.X, pos0.Y);
//        dir = dir0;

//        // Sets random car color
//        List<KnownColor> colorList = Enum.GetValues(typeof(KnownColor)).Cast<KnownColor>().ToList();
//        this.color = Color.FromKnownColor(colorList[random.Next(0, colorList.Count - 1)]);
//        //Console.WriteLine(color.GetHue());

//        // Assign a neural net to the car
//        NN = new NeuralNet(4, 1, 10, 8, 2, "softsign", .05);

//        // View sensors data
//        sensors = new Vector2[7];

//        // Car sprite
//        sprite = carSprite;
//    }

//    public void Update(Map map)
//    {
//        this.spd += this.acc;
//        this.spd = this.spd < -2 ? -2 : this.spd;

//        //Check map
//        //spd = map.Check(this.pos) ? this.spd : 0;
//        if (!map.Check(this.pos))
//            this.spd = 0;
//        //this.NN.fitness -= 1

//        pos.add(spd * Math.Cos(dir), spd * Math.Sin(dir));

//        NN.fitness += spd;

//    }

//    public void drive(float[,] input)
//    {
//        this.acc = ((input[0, 0] > 0 && this.spd >= 0) || this.spd < 0) ? input[0, 0] * .05 : input[0, 0] * .15;
//        this.dir += input[1, 0] * .05 * (1 - 1 / (1 + Math.Abs(this.spd))) * Math.Sign(this.spd); //* avgDeltaTime / (1 / 30)
//    }

//    public float[,] getInputs(Map map)
//    {
//        float[,] inputs = new float[8, 1];
//        int maxIncrements = 60;

//        //float prevx, prevy, x, y, angle;

//        inputs[7, 0] = this.spd;

//        for (int i = 0; i < inputs.GetLength(0) - 1; i++)
//        {
//            float angle = this.dir + ((i - 3f) / 10f) * Math.PI;

//            for (int j = 0; j < maxIncrements; j++)
//            {
//                float prevx = this.pos.X + (2 * Math.Cos(((i - 3f) / 10f) * Math.PI)) * j * Math.Cos(angle) * 4f;
//                float prevy = this.pos.Y + (2 * Math.Cos(((i - 3f) / 10f) * Math.PI)) * j * Math.Sin(angle) * 4f;

//                float x = this.pos.X + (2 * Math.Cos(((i - 3f) / 10f) * Math.PI)) * (j + 1) * Math.Cos(angle) * 4f;
//                float y = this.pos.Y + (2 * Math.Cos(((i - 3f) / 10f) * Math.PI)) * (j + 1) * Math.Sin(angle) * 4f;

//                if (!map.Check(new Vector2(x, y)) || j == maxIncrements - 1)
//                {
//                    x = prevx;
//                    y = prevy;
//                    inputs[i, 0] = Math.Sqrt(Math.Pow(x - this.pos.X, 2) + Math.Pow(y - this.pos.Y, 2));

//                    sensors[i] = new Vector2(x, y);
//                    /*if (showInputs)
//                    {
//                        stroke(255); line(this.pos.x, this.pos.y, x, y)
//                    }*/

//                    break;
//                }
//            }
//        }

//        return inputs;
//    }

//    public void reset()
//    {
//        //random = new Random(Guid.NewGuid().GetHashCode());
//        float randomSpread = 5f;
//        this.dir = dir0;
//        this.pos = new Vector2(pos0.X - randomSpread / 2 + randomSpread * random.NextDouble(), pos0.Y - randomSpread / 2 + randomSpread * random.NextDouble());
//        this.spd = 0;
//        this.acc = 0;
//    }

//    private static Bitmap RotateImage(Bitmap bmp, float angle)
//    {
//        float height = bmp.Height;
//        float width = bmp.Width;
//        int hypotenuse = System.Convert.ToInt32(System.Math.Floor(Math.Sqrt(height * height + width * width)));
//        Bitmap rotatedImage = new Bitmap(hypotenuse, hypotenuse);
//        using (Graphics g = Graphics.FromImage(rotatedImage))
//        {
//            g.TranslateTransform((float)rotatedImage.Width / 2, (float)rotatedImage.Height / 2); //set the rotation point as the center into the matrix
//            g.RotateTransform(angle); //rotate
//            g.TranslateTransform(-(float)rotatedImage.Width / 2, -(float)rotatedImage.Height / 2); //restore rotation point into the matrix
//            g.DrawImage(bmp, (hypotenuse - width) / 2, (hypotenuse - height) / 2, width, height);
//        }
//        return rotatedImage;
//    }

//    public Bitmap RotatedCar()
//    {
//        float height = sprite.Height;
//        float width = sprite.Width;
//        int hypotenuse = System.Convert.ToInt32(System.Math.Floor(Math.Sqrt(height * height + width * width)));
//        Bitmap rotatedImage = new Bitmap(hypotenuse, hypotenuse);
//        using (Graphics g = Graphics.FromImage(rotatedImage))
//        {
//            g.TranslateTransform((float)rotatedImage.Width / 2, (float)rotatedImage.Height / 2); //set the rotation point as the center into the matrix
//            g.RotateTransform((float)dir * 180 / (float)Math.PI); //rotate
//            g.TranslateTransform(-(float)rotatedImage.Width / 2, -(float)rotatedImage.Height / 2); //restore rotation point into the matrix
//            g.DrawImage(sprite, (hypotenuse - width) / 2, (hypotenuse - height) / 2, width, height);
//        }
//        return rotatedImage;
//    }
//}
