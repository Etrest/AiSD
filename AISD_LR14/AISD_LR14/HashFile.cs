namespace AISD_LR14
{
    public record HashFile
    {
        //ключ блока
        public byte? HashKey { get; set; }
        //Значения блока
        public List<int> Value { get; set; }
        //ссылка на следующий блок
        public HashFile Next { get; set; }

        public HashFile()
        {
            Value = new List<int>();
        }

        public void ShowAll()
        {
            var current = this;
            //вывод значений первого блока
            PrintValue(current);

            while (current.Next is not null)
            {
                current = current.Next;
                //вывод значений текущего блока
                PrintValue(current);
            }
            Console.WriteLine();
        }
        public void Add(int value)
        {
            var hash = GetHash(value);
            var current = this;
            //проверка первого элемента
            if (current.HashKey is null || current.HashKey == hash)
            {
                current.HashKey = hash;
                current.Value.Add(value);
                return;
            }

            while (current.Next is not null)
            {
                current = current.Next;
                //ключ текущего блока не равен хешу нового элемента
                if (current.HashKey != hash)
                    continue;
                //проверка на наличие этого элемента в блоке
                if (!current.Value.Any(x => x == value))
                {
                    current.Value.Add(value);
                }
                else
                {
                    Console.WriteLine($"Значение {value} уже существует!");
                }
                return;
            }
            //создание нового элемента/блока
            var newHashFile = new HashFile()
            {
                HashKey = hash,
                Value = new List<int>() { value }
            };

            //присваение ссылки на новый эл. у последнего блока
            current.Next = newHashFile;
        }
        public void Search(int value)
        {
            var hash = GetHash(value);
            var current = this;

            //поиск значения у первого блока, если ключ равен хешу
            if (current.HashKey == hash)
            {
                SearchInCurrentHashFile(value, current);
                return;
            }

            while (current.Next is not null)
            {
                current = current.Next;

                if (current.HashKey != hash)
                    continue;

                SearchInCurrentHashFile(value, current);
                return;
            }
            //блока с данным значением нет
            Console.WriteLine("Not Found");
        }
        public void Remove(int value)
        {
            var hash = GetHash(value);
            var current = this;

            if (current.HashKey == hash)
            {
                current.Value.Remove(value);
                return;
            }

            while (current.Next is not null)
            {
                current = current.Next;

                if (current.HashKey == hash && current.Value.Any(x => x == value))
                {
                    current.Value.Remove(value);
                    return;
                }
            }
            Console.WriteLine("Элемент не существует");
        }

        //поиск значения в текущем блоке
        private void SearchInCurrentHashFile(int value, HashFile current)
        {
            for (int i = 0; i < current.Value.Count; i++)
            {
                if (current.Value[i] == value)
                {
                    Console.WriteLine($"HashKey: {current.HashKey} Value {value}");
                    return;
                }
            }

            Console.WriteLine("Not Found");
        }

        //созданите хеша
        private byte GetHash(int number) =>
            byte.Parse(number.ToString().ToCharArray().FirstOrDefault().ToString());

        private void PrintValue(HashFile current)
        {
            if (current.HashKey is null)
                return;

            Console.Write("\n\rHashKey: " + current.HashKey + " Value: ");
            foreach (var item in current.Value)
            {
                Console.Write("\t" + item);
            }
        }
    }
}