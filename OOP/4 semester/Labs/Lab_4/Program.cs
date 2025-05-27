namespace Lab_4;

public delegate void PropertyChangedEventHandler<C>(ref C _class);
public delegate bool PropertyChangingEventHandler<C, V>(ref C _class, V value);
public interface IPropertyChanged<C>
{
    public event PropertyChangedEventHandler<C> PropertyChanged;
}

public interface IPropertyChanging<C, V>
{
    public event PropertyChangingEventHandler<C, V> PropertyChanging;
}

public struct TrackedProperty<V> : IPropertyChanged<TrackedProperty<V>>, IPropertyChanging<TrackedProperty<V>, V>
{
    private V m_value;

    private PropertyChangedEventHandler<TrackedProperty<V>> m_propertyChangedHandler;
    private PropertyChangingEventHandler<TrackedProperty<V>, V> m_propertyChangingHandler;

    public V Value
    {
        get => m_value;
        set
        {
            var delegates = m_propertyChangingHandler?.GetInvocationList();
            if (delegates == null) return;

            foreach (PropertyChangingEventHandler<TrackedProperty<V>, V> d in delegates)
            {
                if (!d.Invoke(ref this, value))
                {
                    return;
                }

            }

            m_value = value;
            m_propertyChangedHandler?.Invoke(ref this);
        }
    }

    public event PropertyChangedEventHandler<TrackedProperty<V>> PropertyChanged
    {
        add
        {
            m_propertyChangedHandler -= value;         //Для того, чтобы не было дубликатов
            m_propertyChangedHandler += value;
        }

        remove => m_propertyChangedHandler -= value;
    }

    public event PropertyChangingEventHandler<TrackedProperty<V>, V> PropertyChanging
    {
        add
        {
            m_propertyChangingHandler -= value;         //Для того, чтобы не было дубликатов
            m_propertyChangingHandler += value;
        }

        remove => m_propertyChangingHandler -= value;
    }
}

public class Class_1
{
    public TrackedProperty<int> valueThatNeedToBeTracked;
}

internal class Program
{
    static void Main()
    {
        Class_1 _class = new Class_1();
        _class.valueThatNeedToBeTracked.PropertyChanging += PropertyChanging1;
        _class.valueThatNeedToBeTracked.PropertyChanging += PropertyChanging2;
        _class.valueThatNeedToBeTracked.PropertyChanged += PropertyChanged;
        _class.valueThatNeedToBeTracked.Value = 7;
        Console.WriteLine("Текущее значение: " + _class.valueThatNeedToBeTracked.Value);
        _class.valueThatNeedToBeTracked.Value = 11;
        Console.WriteLine("Текущее значение: " + _class.valueThatNeedToBeTracked.Value);
        _class.valueThatNeedToBeTracked.Value = -545;
        Console.WriteLine("Текущее значение: " + _class.valueThatNeedToBeTracked.Value);
        _class.valueThatNeedToBeTracked.Value = 0;
        Console.WriteLine("Текущее значение: " + _class.valueThatNeedToBeTracked.Value);
    }

    private static void PropertyChanged(ref TrackedProperty<int> s)
    {
        Console.WriteLine("Значение было изменено: " + s.Value);
    }

    private static bool PropertyChanging1(ref TrackedProperty<int> s, int value)
    {
        Console.WriteLine("Значение начинает изменяться, проверка #1");
        if (value < 0)
            return false;
        return true;
    }

    private static bool PropertyChanging2(ref TrackedProperty<int> s, int value)
    {
        Console.WriteLine("Значение начинает изменяться, проверка #2");
        if (value > 10)
            return false;
        return true;
    }
}
