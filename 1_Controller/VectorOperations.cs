using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TextClassificationGUI._2_Domain;

namespace TextClassificationGUI._1_Controller;

public class VectorOperations
{
    public static string GetClosestVectorType(Vectors vectors, List<int> vector, int range)
    {
        if (range % 2 == 0) throw new Exception("Range must be odd");
        
        List<Pair> pairs = new();
        vectors.GetVectorsInA().ForEach(vec => pairs.Add(
            new Pair(CalculateDistance(vec, vector), "ClassA")));
        vectors.GetVectorsInB().ForEach(vec => pairs.Add(
            new Pair(CalculateDistance(vec, vector), "ClassB")));
        // Get the closest vectors in range
        var closestVectors = pairs
            .OrderBy(pair => pair.Distance)
            .Take(range)
            .ToList();
        // Decide the type of the vector
        return closestVectors
            .GroupBy(pair => pair.Type)
            .OrderByDescending(type => type.Count())
            .First()
            .Key;

    }
    
    private static double CalculateDistance(List<int> vector1, List<int> vector2)
    {
        // zip the two vectors together
        var zipped = vector1.Zip(vector2, (x, y) => new { x, y });
        // iterate through the zipped vectors and square the difference, then sum the results
        double distance = zipped.Sum(val => Math.Pow(val.x - val.y, 2));
        // return the square root of the distance
        return Math.Sqrt(distance);
    }
    
    class Pair
    {
        public double Distance { get;}
        public string Type { get;}
        
        public Pair(double distance, string type)
        {
            Distance = distance;
            Type = type;
        }
    }
}