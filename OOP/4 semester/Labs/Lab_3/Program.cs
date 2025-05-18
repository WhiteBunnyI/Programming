using Lab_3;

string[] logs =
    [
    "There was a seriousle ErRoR: hahaha just joke :)",
    "There was a error: CS0057",
    "There was a error: CS188",
    "There was a warn: CS1405",
    "There was a warn: CS4447",
    ];

SimpleLogFilter simpleFilter = new SimpleLogFilter("warn");
ReLogFilter reLogFilter = new ReLogFilter("CS...[7, 8]");

ConsoleHandler consoleHandler = new ConsoleHandler();
FileHandler fileHandler = new FileHandler();
SocketHandler socketHandler = new SocketHandler();
SyslogHandler syslogHandler = new SyslogHandler();

Logger otherLogger = new Logger(simpleFilter, [consoleHandler, fileHandler, socketHandler, syslogHandler]);

foreach (string log in logs)
{
    otherLogger.Log(log);
}

Console.WriteLine();

fileHandler.Handle("");

Logger logger = new Logger([simpleFilter, reLogFilter], [consoleHandler, fileHandler, socketHandler, syslogHandler]);

foreach (string log in logs)
{
    logger.Log(log);
}

