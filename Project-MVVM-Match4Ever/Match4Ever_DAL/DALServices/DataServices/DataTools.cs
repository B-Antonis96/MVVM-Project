using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Match4Ever_DAL.DALServices.AuthenticationServices.AuthenticationParts.DataEnums;

namespace Match4Ever_DAL.DALServices.DataServices
{
    public class DataTools
    {
        //Parameter checker
        public bool ParameterCheck(string parameter, string parameterCheck) => parameter == parameterCheck;

        //Groote checker
        public bool SizeChecker(int var, int var2) => var > var2;
    }
}
