using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TokenApp
{
    public class SmsResultDto
    {

        public int code { get; set; }
        public string status { get; set; }
        public string status_text { get; set; }
    }
}