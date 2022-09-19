using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TokenApp
{


    public class SmsResponceData
    {
        public int id { get; set; }
        public string from { get; set; }
        public long number { get; set; }
        public string text { get; set; }
        public int status { get; set; }
        public string extendStatus { get; set; }
        public string channel { get; set; }
    }
    public class SmsResponce
    {

        public SmsResponceData data {get;set;}


    }
}