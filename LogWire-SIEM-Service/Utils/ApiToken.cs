using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogWire.SIEM.Service.Utils
{
    public class ApiToken
    {

        private static ApiToken _instance;
        private string _token;

        public static ApiToken Instance => _instance ??= new ApiToken();

        public string Token => _token;

        public void Init(string token)
        {
            _token = token;
        }

    }
}
