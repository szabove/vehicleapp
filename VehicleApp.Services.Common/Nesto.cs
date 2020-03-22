using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Services.Common
{
    class Nesto : INesto
    {
        public void Pucaj()
        {
            Debug.Print("HIT");
        }
    }

    interface INesto
    {
        void Pucaj();
    }



}
