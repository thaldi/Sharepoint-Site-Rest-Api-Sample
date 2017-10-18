using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SharepointRestSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHelper helper = new ServiceHelper(true);
        }
    }
}
