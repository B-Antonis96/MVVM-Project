using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match4Ever_DAL.DALServices.DataServices
{
    public class DataTools
    {
        //Parameter checker
        public bool ParameterCheck(string parameter, string parameterCheck) => parameter == parameterCheck;
    }
}
