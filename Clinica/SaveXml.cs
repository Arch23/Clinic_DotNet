using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Clinica
{
    public class SaveXml<T> : ISave<T>
    {
        private readonly bool encrypt = true;

        private readonly string extension = ".xml";

        public SaveXml(string pFileName): base(pFileName) { }

        public override void SaveList(List<T> pList)
        {
            FileStream fs = new FileStream(Utils.DB_PATH + fileName + extension, FileMode.Create);

            XmlSerializer xml = new XmlSerializer(typeof(List<T>));

            if(encrypt){
                Stream stream = Encryption.GetEncryptedStream(fs);

                xml.Serialize(stream, pList);


                stream.Flush();
                stream.Close();
            }
            else
            {
                xml.Serialize(fs, pList);

                fs.Flush();
                fs.Close();
            }
        }

        public override List<T> LoadList()
        {
            List<T> list;
            if (File.Exists(Utils.DB_PATH + fileName + extension))
            {
                FileStream fs = new FileStream(Utils.DB_PATH + fileName + extension, FileMode.Open);

                XmlSerializer xml = new XmlSerializer(typeof(List<T>));


                if (encrypt)
                {
                    Stream stream = Encryption.GetDecryptedStream(fs);

                    list = (List<T>)xml.Deserialize(stream);

                    stream.Close();
                }
                else
                {

                    list = (List<T>)xml.Deserialize(fs);

                    fs.Close();
                }

            }
            else
            {
                list = new List<T>();
            }
            return list;
        }
    }
}