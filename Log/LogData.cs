using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    [Serializable]
    public class LogData
    {
        private int SHIFT = 10;

        public string Message { get { return Decode(Message); }
            set {
                Message = Encode(value);
            }
        }
        public DateTime Date { get; set; }

        private string Encode(string pMessage)
        {
            string result = "";

            for(int i = 0; i < pMessage.Length; i++)
            {
                result += Convert.ToChar(((int)pMessage[i]) + SHIFT);
            }

            return (result);
        }

        private string Decode(string pMessage)
        {
            string result = "";

            for (int i = 0; i < pMessage.Length; i++)
            {
                result += Convert.ToChar(((int)pMessage[i]) - SHIFT);
            }

            return (result);
        }
    }
}
