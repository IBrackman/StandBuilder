using System.Collections.Generic;

namespace StandBuilder
{
    public class NetworkModel
    {
        public List<Figure> AllFigures { get; private set; }
        public List<string> Routes { get; private set; }
        
        public void GetModelFromFile(string fileName)
        {
            AllFigures = new List<Figure>();

            Routes = new List<string>();

            Parser.Parse(fileName, AllFigures);
        }
    }
}