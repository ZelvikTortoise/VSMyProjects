using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGCalculator
{
    interface IObjectAG
    {
        bool IsPointOfThisObject(MyPoint point);
        ObjectInfo GetInfo1(Language lang);
        List<ObjectInfo> GetAllInfoPossibilities(Language lang);        
    }

    interface IObject2D : IObjectAG
    {
        string GetGeneralEquation();
    }

    interface IObject3D : IObjectAG
    {

    }
}
