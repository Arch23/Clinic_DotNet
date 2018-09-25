using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinica
{
    public abstract class ISave<T>
    {
        protected readonly string fileName;

        public ISave(string pFileName) {
            fileName = pFileName;
        }

        public abstract void SaveList(List<T> pList);

        public abstract List<T> LoadList();
    }
}