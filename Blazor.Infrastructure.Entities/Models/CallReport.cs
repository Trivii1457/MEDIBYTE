using System;
using System.Collections.Generic;
using System.Text;

namespace Dominus.Backend.Data
{
    public class CallReport
    {
        public string ReportName { get; set; }

        private string connectionName;

        public string ConnectionName
        {
            get
            {
                return connectionName;
            }
            set
            {
                connectionName = value.Replace(".", "--");
            }
        }

        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }

        public string ConvertStringToHex()
        {

            Byte[] stringBytes = Encoding.UTF8.GetBytes(ToString());
            StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
            foreach (byte b in stringBytes)
            {
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }

        public string ConvertHexToString(string hexInput)
        {
            int numberChars = hexInput.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16);
            }
            return Encoding.UTF8.GetString(bytes);
        }


        //public byte[] ToByteArray()
        //{
        //    return Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(this));
        //}

        //public CallReport LoadData(byte[] utfBytes)
        //{
        //    return System.Text.Json.JsonSerializer.Deserialize<CallReport>(Encoding.UTF8.GetString(utfBytes, 0, utfBytes.Length));
        //}
    }
}
