using finallexamp.Core.Interfaces;

namespace finallexamp.Core.Services
{
    public class LoggerService : ILoggerService
    {
        public void LogInformation(string message)
        {
            Console.WriteLine($"[INFO] {message}");
        }


        public void LogWarning(string message)
        {
            Console.WriteLine($"[WARNING] {message}");
        }


        public void LogError(string message, Exception exception)
        {
            Console.WriteLine($"[ERROR] {message} - Exception: {exception.Message}");
        }
    }
}
