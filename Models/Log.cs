using System.Diagnostics;
namespace Inventario.Models
{
    public class Log
    {
        public Log()
        {

        }

        public void WriteLog(Exception exception)
        {
            string route = "log.txt";
            using (StreamWriter writer = new StreamWriter(route, true))
            {
                writer.WriteLine($"ERROR: {exception.Message}");
                StackTrace stackTrace = new StackTrace(exception, true);
                StackFrame frame = stackTrace.GetFrame(stackTrace.FrameCount - 1)!;
                {
                    if (frame != null)
                    {
                        writer.WriteLine($"time: {DateTime.Now}");
                        writer.WriteLine($"function: {frame.GetMethod().Name}");
                        writer.WriteLine($"file: {frame.GetFileName()}");
                        writer.WriteLine($"line: {frame.GetFileLineNumber()}");
                    }
                }
            }
        }
    }
}
