using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGCalculator
{
    interface IParametric
    {
        char ParameterName { get; set; }

        string GetParametricEquationForX();
        string GetParametricEquationForY();
        string GetParametricEquationForZ(); 

        string GetParameterNameAndRange();

        bool CheckParametricEquationEquivalency(MyPoint A, Vector[] vectors);
    }
}
