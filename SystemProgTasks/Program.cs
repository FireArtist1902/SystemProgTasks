using System.Timers;

namespace SystemProgTasks
{
    class Program
    {

        static List<int> GetPrimeNumbers(int end, int start = 2)
        {
            List<int> nums = new List<int>();

            for (int i = start; i <= end; i++)
            {
                nums.Add(i);
            }


            for (int i = 0; i < nums.Count; i++)
            {
                for (int j = i + 1; j < nums.Count; j++)
                {
                    if (nums[j] % nums[i] == 0)
                    {
                        nums.RemoveAt(j);
                    }
                }
            }

            return nums;
        }

        static void Main(string[] args)
        {
            List<int> nums = new List<int>();
            bool work = true;
            while (work)
            {
                Console.Clear();
                Console.WriteLine("0.Вихід");
                Console.WriteLine("1.Вивід дати");
                Console.WriteLine("2.Прості числа в заданому діапазоні");
                Console.WriteLine("3.Дані про масив");
                Console.WriteLine("4.Завдання 4");

                int.TryParse(Console.ReadLine(), out int choice);

                switch (choice)
                {
                    case 0:
                        work = false; 
                        break;
                    case 1:
                        Task task1 = Task.Run(() => { Console.WriteLine(DateTime.Now); });
                        Task task2 = new Task(() => { Console.WriteLine(DateTime.Now); });
                        task2.Start();
                        Task task3 = Task.Factory.StartNew(() => { Console.WriteLine(DateTime.Now); }, TaskCreationOptions.LongRunning);
                        Task.WaitAll(task1, task2, task3);
                        Console.WriteLine("Для продовження натисність ENTER");
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Введіть початок");
                        int.TryParse(Console.ReadLine(), out int start);
                        Console.WriteLine("Введіть кінець");
                        int.TryParse(Console.ReadLine(), out int end);
                        Task task = Task.Run(() => 
                        {
                            nums = GetPrimeNumbers(end, start).ToList();
                            foreach(int i in nums)
                            {
                                Console.WriteLine(i);
                            }
                        });
                        task.Wait();
                        Console.WriteLine("Кількість чисел");
                        Console.WriteLine(nums.Count); 
                        Console.WriteLine("Для продовження натисність ENTER");
                        Console.ReadLine();
                        break;
                    case 3:
                        Random random = new Random();

                        for (int i = 0; i < 100; i++)
                        {
                            nums.Add(random.Next(0, 100));
                        }
                        Console.Clear();
                        Task[] tasks = 
                            {
                                new Task(() => { Console.WriteLine($"Min: {nums.Min()}"); }),
                                new Task(() => { Console.WriteLine($"Max: {nums.Max()}"); }),
                                new Task(() => { Console.WriteLine($"Average: {nums.Average()}"); }),
                                new Task(() => { Console.WriteLine($"Sum: {nums.Sum()}"); })
                            };
                        foreach(var ts in tasks)
                        {
                            ts.Start();
                        }
                        Task.WaitAll(tasks);
                        Console.WriteLine("Для продовження натисність ENTER");
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.Clear();
                        Random random1 = new Random();
                        for (int i = 0; i < 50; i++)
                        {
                            nums.Add(random1.Next(0, 10));
                        }
                        Console.WriteLine("Початковий список");
                        PrintList(nums);
                        Console.WriteLine("Для продовження натисність ENTER");
                        Console.ReadLine();
                        Task task4 = new Task(() => { ClearList(nums); });
                        task4.Start();
                        task4.ContinueWith(Action => nums.Sort());
                        task4.ContinueWith(Action => nums.Find(g => g == 5));
                        break;
                }
            }
        }

        static private void PrintList(List<int> nums)
        {
            for(int i = 0;i < nums.Count;i++)
            {
                Console.Write($"{nums[i]} ");
            }
        }

        static private void ClearList(List<int> nums)
        {
            for (int i = 0;i < nums.Count; i++)
            {
                for(int j = i + 1; j < nums.Count; j++)
                {
                    if (nums[i] == nums[j])
                    {
                        nums.RemoveAt(j);
                    }
                }
            }
        }

        
    }
}