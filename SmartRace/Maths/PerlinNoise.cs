using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRace.Maths
{
    public class PerlinNoise
    {
        private static readonly Random random = new(Guid.NewGuid().GetHashCode());

        static readonly int PERLIN_YWRAPB = 4;
        static readonly int PERLIN_YWRAP = 1 << PERLIN_YWRAPB;
        static readonly int PERLIN_ZWRAPB = 8;
        static readonly int PERLIN_ZWRAP = 1 << PERLIN_ZWRAPB;
        readonly int PERLIN_SIZE = 4095;
        int perlin_octaves = 4; // default to medium smooth
        double perlin_amp_falloff = 0.5; // 50% reduction/octave

        private double Scaled_cosine(double i)
        {
            return 0.5 * (1.0 - Math.Cos(i * Math.PI));
        }

        double[] perlin;


        public double Noise(params double[] pos)
        {
            double x = pos[0];
            double y = pos.Length > 1 ? pos[1] : 0;
            double z = pos.Length > 2 ? pos[2] : 0;
            if (perlin == null)
            {
                perlin = new double[PERLIN_SIZE + 1];
                for (var i = 0; i < PERLIN_SIZE + 1; i++)
                {
                    perlin[i] = random.NextDouble();
                }
            }

            if (x < 0)
            {
                x = -x;
            }
            if (y < 0)
            {
                y = -y;
            }
            if (z < 0)
            {
                z = -z;
            }

            int xi = (int)Math.Floor(x);
            int yi = (int)Math.Floor(y);
            int zi = (int)Math.Floor(z);
            double xf = x - xi;
            double yf = y - yi;
            double zf = z - zi;
            double rxf, ryf;

            double r = 0;
            double ampl = 0.5;

            double n1, n2, n3;

            for (int o = 0; o < perlin_octaves; o++)
            {
                int of = xi + (yi << PERLIN_YWRAPB) + (zi << PERLIN_ZWRAPB);

                rxf = Scaled_cosine(xf);
                ryf = Scaled_cosine(yf);

                n1 = perlin[of & PERLIN_SIZE];
                n1 += rxf * (perlin[of + 1 & PERLIN_SIZE] - n1);
                n2 = perlin[of + PERLIN_YWRAP & PERLIN_SIZE];
                n2 += rxf * (perlin[of + PERLIN_YWRAP + 1 & PERLIN_SIZE] - n2);
                n1 += ryf * (n2 - n1);

                of += PERLIN_ZWRAP;
                n2 = perlin[of & PERLIN_SIZE];
                n2 += rxf * (perlin[of + 1 & PERLIN_SIZE] - n2);
                n3 = perlin[of + PERLIN_YWRAP & PERLIN_SIZE];
                n3 += rxf * (perlin[of + PERLIN_YWRAP + 1 & PERLIN_SIZE] - n3);
                n2 += ryf * (n3 - n2);

                n1 += Scaled_cosine(zf) * (n2 - n1);

                r += n1 * ampl;
                ampl *= perlin_amp_falloff;
                xi <<= 1;
                xf *= 2;
                yi <<= 1;
                yf *= 2;
                zi <<= 1;
                zf *= 2;

                if (xf >= 1.0)
                {
                    xi++;
                    xf--;
                }
                if (yf >= 1.0)
                {
                    yi++;
                    yf--;
                }
                if (zf >= 1.0)
                {
                    zi++;
                    zf--;
                }
            }
            return r;
        }

        public void NoiseDetail(int lod, double falloff)
        {
            if (lod > 0)
            {
                perlin_octaves = lod;
            }
            if (falloff > 0)
            {
                perlin_amp_falloff = falloff;
            }
        }




        /*
        public void NoiseSeed() {
            // Linear Congruential Generator
            // Variant of a Lehman Generator


            lcg.setSeed(seed);
            perlin = new double[PERLIN_SIZE + 1];
            for (var i = 0; i < PERLIN_SIZE + 1; i++)
            {
                perlin[i] = lcg.rand();
            }
        }

        private void lcg() {
            // Set to values from http://en.wikipedia.org/wiki/Numerical_Recipes
            // m is basically chosen to be large (as it is the max period)
            // and for its relationships to a and c
            var m = 4294967296;
            // a - 1 should be divisible by m's prime factors
            var a = 1664525;
            // c and m should be co-prime
            var c = 1013904223;
            var seed, z;
            return {
            setSeed: function setSeed(val)
                {
                    // pick a random seed if val is undefined or null
                    // the >>> 0 casts the seed to an unsigned 32-bit integer
                    z = seed = (val == null ? Math.random() * m : val) >>> 0;
                },
                    getSeed: function getSeed()
                {
                    return seed;
                },
                    rand: function rand()
                {
                    // define the recurrence relationship
                    z = (a * z + c) % m;
                    // return a float in [0, 1)
                    // if z = m then z / m = 0 therefore (z % m) / m < 1 always
                    return z / m;
                }
            }
        }
        */

    }
}
