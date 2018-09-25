using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Log
{
    public static class Creator
    {
        private static string FormatError(string pErrorMessage)
        {
            return $"[{DateTime.Now.ToString("dd/MM/yyyy-HH:mm:ss")}]: {pErrorMessage}";
        }

        public static void SaveLogBin(string pErrorMessage) {
            LogData logData = new LogData {
                Message = pErrorMessage,
                Date = DateTime.Now
            };

            string FILE_NAME = "C://Logs//log.bin";
            FileStream fs = null;

            if (File.Exists(FILE_NAME)) {
                fs = new FileStream(FILE_NAME, FileMode.Append);
            }
            else
            {
                fs = new FileStream(FILE_NAME, FileMode.Create);
            }

            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, logData);

            fs.Flush();
            fs.Close();
        }

        public static LogData LoadLogBin()
        {
            LogData logData;

            string FILE_NAME = "C://Logs//log.bin";

            FileStream fs = new FileStream(FILE_NAME, FileMode.Open);

            BinaryFormatter bf = new BinaryFormatter();

            logData = (LogData)bf.Deserialize(fs);

            fs.Close();

            return logData;
        }

        public static void SaveLogXml(string pErrorMessage)
        {
            string FILE_NAME = "C://Logs//log.xml";
            FileStream fs = new FileStream(FILE_NAME,FileMode.Create);

            XmlSerializer xml = new XmlSerializer(typeof(LogData));

            LogData logData = new LogData {
                Message = pErrorMessage,
                Date = DateTime.Now
            };

            xml.Serialize(fs, logData);

            fs.Flush();
            fs.Close();
        }

        public static LogData LoadLogXml()
        {
            string FILE_NAME = "C://Logs//log.xml";
            FileStream fs = new FileStream(FILE_NAME, FileMode.Open);
            XmlSerializer xml = new XmlSerializer(typeof(LogData));

            LogData logData = (LogData)xml.Deserialize(fs);

            fs.Close();

            return logData;
        }

        public static void WriteLog(string pErrorMessage)
        {
            string FILE_NAME = "C://TSC_CLINIC//Logs//log.txt";
            StreamWriter sw = null;

            if (File.Exists(FILE_NAME))
                sw = File.AppendText(FILE_NAME);
            else
                sw = new StreamWriter(FILE_NAME);

            try
            {
                sw.WriteLine(FormatError(pErrorMessage));
            }catch(Exception)
            {
                new StreamWriter($"{DateTime.Now.ToString("dd/MM/yyyy-HH:mm:ss")}.txt").WriteLine($"Log file blocked!\r\n{FormatError(pErrorMessage)}");
            }
            finally
            {
                if(sw != null)
                {
                    sw.Flush();
                    sw.Close();
                }
            }
        }
    }
}
