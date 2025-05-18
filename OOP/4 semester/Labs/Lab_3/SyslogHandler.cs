using System.Diagnostics;
namespace Lab_3;

internal class SyslogHandler : ILogHandler
{
    public SyslogHandler()
    {
        if(!EventLog.SourceExists("TestLog"))
            EventLog.CreateEventSource("TestLog", "Application");
    }
    public void Handle(string message)
    {
        EventLog.WriteEntry("TestLog", message, EventLogEntryType.Information);
    }
}
