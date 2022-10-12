using System.Collections.Generic;
using System.Linq;

namespace TextClassificationGUI._2_Domain;

public class FileLists
{
    private List<string> _a;
    private List<string> _b;
    private List<string> _benchA;
    private List<string> _benchB;

    public void SetA(List<string> a)
    {
        _a = a;
    }

    public void SetB(List<string> b)
    {
        _b = b;
    }

    public List<string> GetA()
    {
        return _a;
    }

    public List<string> GetB()
    {
        return _b;
    }

    public void SetBenchA(List<string> benchA)
    {
        _benchA = benchA;
    }
    
    public void SetBenchB(List<string> benchB)
    {
        _benchB = benchB;
    }
    
    public List<string> GetBenchA()
    {
        return _benchA;
    }
    
    public List<string> GetBenchB()
    {
        return _benchB;
    }
}