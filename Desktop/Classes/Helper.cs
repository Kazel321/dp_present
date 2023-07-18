using Diplom.Classes.Entities;
using System.Configuration;

namespace Diplom.Classes
{
    public static class Helper
    {
        public static User User;
        public static FormEvents formEvents;
        public static DB DB;
        public static string Filter = "Изображения|*.jpg|Все файлы|*.*";
        public static int id = 999999999;
        public static string ip = ConfigurationManager.ConnectionStrings["DB"].ConnectionString.Split(';')[0].Split('=')[1];
        public static string connection = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
    }

    public enum FormEvents { Добавление, Редактирование}
}
