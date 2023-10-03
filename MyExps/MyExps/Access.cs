using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExps
{
    internal class Access
    {
        public bool acc; // будет отвечать за доступ пользователю (вошел/невошел)
        public Storage stor;
        public Access()
        {
            acc = false;
            stor = new Storage();
        }

        public void Registration(string log, string pass) // регистрация
        {  
            if(stor.Find(log, pass) == null) // поиск подобных пользователей
            {
                acc = true;
                User user = new User(log, pass);
                stor.Add(user);
                stor.Save();
            }
        }
        public void LogIn(string log, string pass) // авторизация
        {
            if (stor.Find(log, pass) != null)
            {
                acc = true;
            }
        }
        public void Exit() => acc = false; // выход из акка
  
    }
}
