using BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFoNotUpdateAppByFO
{
    class Program
    {
        static void Main(string[] args)
        {
            ReasonStatusBal _bal = new ReasonStatusBal();
            int flag = _bal.FoNotUpdateApp();
        }
    }
}
