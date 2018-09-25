using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Clinica
{
    public class SaveBin<T> : ISave<T>
    {
        public SaveBin(string pFileName) : base(pFileName) { }

        private readonly bool encrypt = true;

        private readonly string extension = ".bin";

        public override List<T> LoadList()
        {
            List<T> list;
            if (File.Exists(Utils.DB_PATH + fileName + extension))
            {
                FileStream fs = new FileStream(Utils.DB_PATH + fileName + extension, FileMode.Open);

                BinaryFormatter bin = new BinaryFormatter();


                if (encrypt)
                {
                    Stream stream = Encryption.GetDecryptedStream(fs);

                    list = (List<T>)bin.Deserialize(stream);

                    stream.Close();
                }
                else
                {

                    list = (List<T>)bin.Deserialize(fs);

                    fs.Close();
                }

            }
            else
            {
                list = new List<T>();
            }
            return list;
        }

        public override void SaveList(List<T> pList)
        {
            FileStream fs = new FileStream(Utils.DB_PATH + fileName + extension, FileMode.Create);

            BinaryFormatter bin = new BinaryFormatter();

            if (encrypt)
            {
                Stream stream = Encryption.GetEncryptedStream(fs);

                bin.Serialize(stream, pList);


                stream.Flush();
                stream.Close();
            }
            else
            {
                bin.Serialize(fs, pList);

                fs.Flush();
                fs.Close();
            }
        }
    }
}