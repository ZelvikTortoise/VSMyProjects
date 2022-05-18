using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGCalculator
{
    public interface IMyForm
    {
        void LoadInCorrectLanguage(Language lang);
        void GoToOtherForm(IMyForm otherForm);        
        
    }
}
