using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRace.Core
{
    class Map
    {

    }
}

//public class Map
//{
//    public PointF TopLeft;

//    public int resolution;

//    public int[,] values;
//    public int width, height;
//    public PointF size;

//    public Bitmap display;
//    public Bitmap trackMap;



//    public bool Check(Vector2 pos)
//    {
//        if ((int)Math.Floor((pos.X - TopLeft.X) / (double)resolution) < 0
//            || (int)Math.Floor((pos.X - TopLeft.X) / (double)resolution) > values.GetLength(0)
//            || (int)Math.Floor((pos.Y - TopLeft.Y) / (double)resolution) < 0
//            || (int)Math.Floor((pos.Y - TopLeft.Y) / (double)resolution) > values.GetLength(1))
//            return false;

//        return values[(int)Math.Floor((pos.X - TopLeft.X) / (double)resolution), (int)Math.Floor((pos.Y - TopLeft.Y) / (double)resolution)] == 1;
//    }

//    public static bool CheckMap(Map map, Vector2 position)
//    {
//        return map.Check(position);
//    }

//    public Map(Bitmap bitmap, int resolution)
//    {
//        this.resolution = (int)resolution;
//        this.width = (int)Math.Floor((double)bitmap.Width / (double)resolution);
//        this.height = (int)Math.Floor((double)bitmap.Height / (double)resolution);

//        this.values = new int[width, height];

//        this.display = new Bitmap(bitmap.Width, bitmap.Height);

//        for (int i = 0; i < this.width; i++)
//            for (int j = 0; j < this.height; j++)
//            {
//                int validCount = 0;

//                for (int ii = 0; ii < resolution; ii++)
//                    for (int jj = 0; jj < resolution; jj++)
//                        if (bitmap.GetPixel(i * resolution + ii, j * resolution + jj).ToArgb() == Color.FromArgb(255, 0, 0, 0).ToArgb())
//                            validCount++;

//                values[i, j] = ((double)validCount / ((double)resolution * (double)resolution) > 0.5) ? 1 : 0;

//                Graphics g = Graphics.FromImage(display);

//                if (values[i, j] == 1)
//                {
//                    g.FillRectangle(new SolidBrush(Color.Red), i * resolution, j * resolution, resolution, resolution);
//                    g.DrawRectangle(new Pen(Color.Black), i * resolution, j * resolution, resolution, resolution);
//                }
//                else
//                {
//                    g.FillRectangle(new SolidBrush(Color.White), i * resolution, j * resolution, resolution, resolution);
//                    g.DrawRectangle(new Pen(Color.Black), i * resolution, j * resolution, resolution, resolution);
//                }
//            }
//    }

//    public Map(Track track, int resolution)
//    {
//        this.resolution = resolution;

//        this.TopLeft = track.GetTopLeft();

//        this.size = track.Size();

//        this.width = (int)size.X / resolution;
//        this.height = (int)size.Y / resolution;

//        // The actual track drawing
//        this.trackMap = new Bitmap((int)this.size.X, (int)this.size.Y);
//        Graphics g = Graphics.FromImage(trackMap);
//        track.Draw(g, Geometry.Mult(track.GetTopLeft(), -1));

//        // Sensor display
//        this.display = new Bitmap((int)this.size.X, (int)this.size.Y);



//        this.values = new int[(int)this.size.X / resolution, (int)this.size.Y / resolution];

//        for (int i = 0; i < this.width; i++)
//            for (int j = 0; j < this.height; j++)
//            {
//                int validCount = 0;

//                for (int ii = 0; ii < resolution; ii++)
//                    for (int jj = 0; jj < resolution; jj++)
//                        if (trackMap.GetPixel(i * resolution + ii, j * resolution + jj).ToArgb() == Color.FromArgb(255, 0, 0, 0).ToArgb())
//                            validCount++;

//                values[i, j] = ((double)validCount / ((double)resolution * (double)resolution) > 0.5) ? 1 : 0;

//                Graphics gg = Graphics.FromImage(display);

//                if (values[i, j] == 1)
//                {
//                    gg.FillRectangle(new SolidBrush(Color.Red), i * resolution, j * resolution, resolution, resolution);
//                    gg.DrawRectangle(new Pen(Color.Black), i * resolution, j * resolution, resolution, resolution);
//                }
//                else
//                {
//                    gg.FillRectangle(new SolidBrush(Color.White), i * resolution, j * resolution, resolution, resolution);
//                    gg.DrawRectangle(new Pen(Color.Black), i * resolution, j * resolution, resolution, resolution);
//                }
//            }
//    }
//}






