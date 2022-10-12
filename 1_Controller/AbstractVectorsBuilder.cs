using System.Collections.Generic;
using TextClassificationGUI._2_Domain;

namespace TextClassificationGUI._1_Controller;

public abstract class AbstractVectorsBuilder
{
    public abstract void AddVectorToA(List<int> vector);
    public abstract void AddVectorToB(List<int> vector);
    public abstract Vectors GetVectors();
}