using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using SmartRace.Maths;

namespace SmartRace.Core
{
    public enum ActivationFunction
    {
        Identity,
        Binary,
        SoftSign,
        ReLU
    }

    class NeuralNetInitializationSettings
    {
        public float Range { get; set; }
        public int Layers { get; set; }
        public int NeuronsPerLayer { get; set; }
        public int Inputs { get; set; }
        public int Outputs { get; set; }
        public ActivationFunction ActivationFunction { get; set; }
        public float MutationRate { get; set; }
    }

    class NeuralNet
    {
        private Random Random = new Random(Guid.NewGuid().GetHashCode());

        private int Layers { get; set; }
        private int NeuronsPerLayer { get; set; }
        private int Inputs { get; set; }
        private int Outputs { get; set; }
        private float Range { get; set; }
        private float MutationRate { get; set; }

        private ActivationFunction ActivationFunction { get; set; }

        public float Fitness { get; set; } = 0;

        private float[][,] Weights { get; set; }
        //private float[][,] Neurons { get; set; } // Probably not needed, it is possible to instantiate neuron array on calculation

        public NeuralNet(NeuralNetInitializationSettings settings)
        {
            Range = settings.Range;
            Layers = settings.Layers;
            NeuronsPerLayer = settings.NeuronsPerLayer;
            Inputs = settings.Inputs;
            Outputs = settings.Outputs;
            ActivationFunction = settings.ActivationFunction;
            MutationRate = settings.MutationRate;

            InitializeWeights();
        }

        public NeuralNet(float range, int layers, int neurons, int inputs, int outputs, ActivationFunction activationFunction, float mutationRate)
        {
            Range = range;
            Layers = layers;
            NeuronsPerLayer = neurons;
            Inputs = inputs;
            Outputs = outputs;
            ActivationFunction = activationFunction;
            MutationRate = mutationRate;

            // Initialize weights
            InitializeWeights();
        }

        private void InitializeWeights()
        {
            Weights = new float[Layers + 1][,];
            for (int i = 0; i < Layers + 1; i++)
            {
                if (i == 0) { Weights[i] = new float[NeuronsPerLayer, Inputs]; }
                if (i > 0 && i < Layers) { Weights[i] = new float[NeuronsPerLayer, NeuronsPerLayer]; }
                if (i == Layers) { Weights[i] = new float[Outputs, NeuronsPerLayer]; }

                for (int j = 0; j < Weights[i].GetLength(0); j++)
                    for (int k = 0; k < Weights[i].GetLength(1); k++)
                        Weights[i][j, k] = ((float)Random.NextDouble() * 2 - 1) * Range;
            }
        }

        public float[,] GetOutput(float[,] input)
        {
            float[,] outputMatrix = new float[Outputs, 1];

            float[][,] neurons = new float[Layers][,];
            for (int i = 0; i < Layers; i++)
                neurons[i] = new float[NeuronsPerLayer, 1];

            for (int i = 0; i < Layers + 1; i++)
            {
                if (i == 0)
                    neurons[i] = NormalizeMatrix(Matrix.Mult(Weights[i], input), ActivationFunction);
                if (i > 0 && i < Layers)
                    neurons[i] = NormalizeMatrix(Matrix.Mult(Weights[i], neurons[i - 1]), ActivationFunction);
                if (i == Layers)
                    outputMatrix = NormalizeMatrix(Matrix.Mult(Weights[i], neurons[i - 1]), ActivationFunction);
            }

            return outputMatrix;
        }

        public float GetFitness() => Fitness;
        public void SetFitness(float fitness) => Fitness = fitness;
        public void AddFitness(float fitness) => Fitness += fitness;

        public void Mutate()
        {
            for (int i = 0; i < Layers + 1; i++)
                for (int j = 0; j < Weights[i].GetLength(0); j++)
                    for (int k = 0; k < Weights[i].GetLength(1); k++)
                        if (Random.NextDouble() < MutationRate)
                            Weights[i][j, k] = ((float)Random.NextDouble() * 2 - 1) * Range;
        }

        public static NeuralNet Breed(NeuralNet parent1, NeuralNet parent2)
        {
            NeuralNet offspring = new NeuralNet(
                parent1.Range,
                parent1.Layers,
                parent1.NeuronsPerLayer,
                parent1.Inputs,
                parent1.Outputs,
                parent1.ActivationFunction,
                parent1.MutationRate
                );


            for (int i = 0; i < offspring.Layers + 1; i++)
                for (int j = 0; j < offspring.Weights[i].GetLength(0); j++)
                    for (int k = 0; k < offspring.Weights[i].GetLength(1); k++)
                        if (offspring.Random.NextDouble() > offspring.MutationRate)
                        {
                            if (offspring.Random.NextDouble() > 0.5)
                                offspring.Weights[i][j, k] = parent1.Weights[i][j, k];
                            else
                                offspring.Weights[i][j, k] = parent2.Weights[i][j, k];
                        }

            return offspring;
        }

        private static float[,] NormalizeMatrix(float[,] values, ActivationFunction activation)
        {
            for (int i = 0; i < values.GetLength(0); i++)
                for (int j = 0; j < values.GetLength(1); j++)
                    values[i, j] = NormalizeValue(values[i, j], activation);

            return values;
        }

        private static float NormalizeValue(float value, ActivationFunction activation)
        {
            switch (activation)
            {
                case ActivationFunction.Identity:
                    return value;
                case ActivationFunction.Binary:
                    return value > 0 ? 1 : 0;
                case ActivationFunction.SoftSign:
                    return value / (1 + Math.Abs(value));
                case ActivationFunction.ReLU:
                    return value > 0 ? 0 : value;
                default:
                    return value;
            }
        }
    }
}
