using System.Collections.Generic;

namespace TextClassificationGUI._2_Domain;

public class Vectors
{
    private readonly List<List<int>> _vectorsInA;
    private readonly List<List<int>> _vectorsInB;

    public Vectors()
    {
        _vectorsInA = new List<List<int>>();

        _vectorsInB = new List<List<int>>();
    }

    public void AddVectorToA(List<int> vector)
    {
        _vectorsInA.Add(vector);
    }

    public void AddVectorToB(List<int> vector)
    {
        _vectorsInB.Add(vector);
    }

    public List<List<int>> GetVectorsInA()
    {
        return _vectorsInA;
    }

    public List<List<int>> GetVectorsInB()
    {
        return _vectorsInB;
    }
}