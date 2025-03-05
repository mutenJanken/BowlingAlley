namespace BowlingAlley.Singleton
{
    // Singleton pattern klass som skriver loggmeddelanden till en fil
    // med streamwriter.
    // När Logger.Instance.Log() anropas så skapas en ny instans av Logger 
    // som också är den enda instansen av Logger genom programmets gång.
    // private kontruktor så att ingen instans kan skapas utanför Logger.
    // lock säkerställer att bara en tråd åt gången har tillgång till _instance.
    public class Logger
    {
        private static Logger? _instance;
        private static readonly object _lock = new object();
        private string _logFilePath = "log.txt";

        private Logger()
        {
        }

        public static Logger Instance
        {
            get 
            {
                lock (_lock)
                {
                    if(_instance == null)
                    {
                        return _instance = new Logger();
                    }
                    return _instance;
                }
            }
        }

        public void Log(string message)
        {
            using(StreamWriter writer = new(_logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}. {message}");
            }
        }
    }
}
