using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.DALServices.DataServices
{
    public sealed class DataTools
    {
        //Parameter checker
        public bool ParameterCheck(string parameter, string parameterCheck) => parameter == parameterCheck;

        //Groote checker
        public bool SizeChecker(int var, int var2) => var > var2;
    }
}
