using System.Collections.Generic;

namespace Task13
{
    internal class Program
    {
        static void Main()
        {
            int CONTAINER_VOLUME = 10;
            List<int> itemsVolume = [4, 8, 1, 4, 2, 1, 8, 6, 3, 9];


            itemsVolume.Sort();
            itemsVolume.Reverse();
            int minPossiblyContainers = (int)MathF.Ceiling(itemsVolume.Sum() / (float)CONTAINER_VOLUME);

            List<List<int>> containers = [];

            for (int i = 0; i < itemsVolume.Count; i++)
            {
                bool isPut = false;
                for (int j = 0; j < containers.Count; j++)
                {
                    if (containers[j].Sum() + itemsVolume[i] <= CONTAINER_VOLUME)
                    {
                        containers[j].Add(itemsVolume[i]);
                        isPut = true;
                        break;
                    }   
                }

                if (!isPut)
                    containers.Add([itemsVolume[i]]);
            }

            Console.WriteLine($"Теоритически минимальное кол-во контейнеров: {minPossiblyContainers}");
            Console.WriteLine($"Получили {containers.Count} контейнеров");
        }
    }
}
