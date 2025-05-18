namespace Lab_3;

internal class Logger
{
    List<ILogFilter> m_filters;
    List<ILogHandler> m_handlers;
    public Logger(List<ILogFilter> filters, List<ILogHandler> handlers)
    {
        m_filters = filters;
        m_handlers = handlers;
    }

    public Logger(ILogFilter filter, List<ILogHandler> handlers) : this([filter], handlers) { }

    public Logger(ILogFilter filter, ILogHandler handlers) : this([filter], [handlers]) { }

    public void Log(string message)
    {
        foreach (ILogFilter filter in m_filters)
            if (!filter.Match(message)) return;

        foreach (ILogHandler handler in m_handlers)
            handler.Handle(message);
    }
}
