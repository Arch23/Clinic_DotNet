using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinica
{
    public abstract class ISave<T>
    {
        protected readonly string fileName;

        //Factory
        public static ISave<T> GetSerializer(SerializerOptions pOptions, string pFileName)
        {
            ISave<T> serializer;

            switch (pOptions)
            {
                case SerializerOptions.BIN:
                    serializer = new SaveBin<T>(pFileName);
                    break;
                case SerializerOptions.XML:
                    serializer = new SaveXml<T>(pFileName);
                    break;
                default:
                    serializer = null;
                    break;
            }

            return serializer;
        }

        protected ISave(string pFileName) {
            fileName = pFileName;
        }

        public abstract void SaveList(List<T> pList);

        public abstract List<T> LoadList();
    }
}