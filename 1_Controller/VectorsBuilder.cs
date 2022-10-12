using System.Collections.Generic;
using TextClassificationGUI._2_Domain;

namespace TextClassificationGUI._1_Controller;

public class VectorsBuilder : AbstractVectorsBuilder
{
    private readonly Vectors _vectors;

    public VectorsBuilder()
    {
        _vectors = new Vectors();
    }

    public override void AddVectorToA(List<int> vector)
    {
        _vectors.AddVectorToA(vector);
    }

    public override void AddVectorToB(List<int> vector)
    {
        _vectors.AddVectorToB(vector);
    }

    public override Vectors GetVectors()
    {
        return _vectors;
    }
}