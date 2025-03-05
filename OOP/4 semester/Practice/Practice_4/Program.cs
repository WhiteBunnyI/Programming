using System.ComponentModel;

namespace Practice_4
{
    public delegate void PropertyChangedEventHandler<C>(ref C s);
    public delegate bool PropertyChangingEventHandler<C, V>(ref C s, V value);
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

        public V Value {
            get => m_value;
            set
            {
                bool isCan = true;
                var delegates = m_propertyChanging?.GetInvocationList();
                if (delegates == null) return;
                foreach(PropertyChangingEventHandler<TrackedProperty<V>, V> d in delegates)
                {
                    if (!d.Invoke(ref this, value))
                    {
                        isCan = false;
                        Console.WriteLine("False");
                    }
                    
                }

                if(isCan)
                {
                    m_value = value;
                    m_propertyChanged?.Invoke(ref this);
                }
            }
        }

        private PropertyChangedEventHandler<TrackedProperty<V>> m_propertyChanged;
        private PropertyChangingEventHandler<TrackedProperty<V>, V> m_propertyChanging;

#pragma warning disable CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.
        public event PropertyChangedEventHandler<TrackedProperty<V>> PropertyChanged
        {
            add
            {
                m_propertyChanged -= value;         //Для того, чтобы не было дубликатов
                m_propertyChanged += value;
            }

            remove => m_propertyChanged -= value;
        }

        public event PropertyChangingEventHandler<TrackedProperty<V>, V> PropertyChanging
        {
            add
            {
                m_propertyChanging -= value;         //Для того, чтобы не было дубликатов
                m_propertyChanging += value;
            }

            remove => m_propertyChanging -= value;
        }
#pragma warning restore CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.
    }

    public class Class_1
    {
        public TrackedProperty<int> valueThatNeedToBeTracked;


    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Class_1 _class = new Class_1();
            _class.valueThatNeedToBeTracked.PropertyChanging += ValueThatNeedToBeTracked_PropertyChanging1;
            _class.valueThatNeedToBeTracked.PropertyChanging += ValueThatNeedToBeTracked_PropertyChanging2;
            _class.valueThatNeedToBeTracked.PropertyChanged += ValueThatNeedToBeTracked_PropertyChanged;
            _class.valueThatNeedToBeTracked.Value = 11;
            Console.WriteLine("Текущее значение: " + _class.valueThatNeedToBeTracked.Value);
        }

        private static void ValueThatNeedToBeTracked_PropertyChanged(ref TrackedProperty<int> s)
        {
            Console.WriteLine("Значение было изменено: " + s.Value);
        }

        private static bool ValueThatNeedToBeTracked_PropertyChanging1(ref TrackedProperty<int> s, int value)
        {
            Console.WriteLine("Значение начинает изменяться, проверка #1");
            if (value < 0)
                return false;
            return true;
        }

        private static bool ValueThatNeedToBeTracked_PropertyChanging2(ref TrackedProperty<int> s, int value)
        {
            Console.WriteLine("Значение начинает изменяться, проверка #2");
            if (value > 10)
                return false;
            return true;
        }
    }
}
