using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouthProtectionAplication.Services.Diario
{
    public class DiarioService
    {
        private readonly Request _request;

        private string _token;

        private const string apiUrlBase = "";

        public DiarioService(string token)
        {
            _request = new Request();
            _token = token;
        }

    }
}
