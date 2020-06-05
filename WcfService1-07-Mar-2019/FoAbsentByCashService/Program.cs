using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayer;


namespace FoAbsentByCashService
{
    class Program
    {
        static void Main(string[] args)
        {
            ReasonStatusBal _bal = new ReasonStatusBal();
            int flag= _bal.FO_CashPendingLeaveMark();
        }
    }
}
