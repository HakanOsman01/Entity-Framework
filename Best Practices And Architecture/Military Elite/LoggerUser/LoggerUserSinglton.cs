namespace LoggerUser
{
    sealed class LoggerUserSinglton
    {
        public static LoggerUserSinglton instance;
        private static object lockObject = new object();
        public string Name { get; set; }
        public string Password { get; set; }

       protected LoggerUserSinglton()
       {

       }
       public static LoggerUserSinglton Intance
       {
            get
            {
                if(instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new LoggerUserSinglton();
                        }
                       

                    }
                }
                return instance;

            }
            
       }



    }
}
