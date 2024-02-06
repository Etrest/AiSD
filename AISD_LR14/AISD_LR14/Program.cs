using AISD_LR14;

var rnd = new Random();
var current = new HashFile();

int value = default;

//Создание сегмента
for (int i = 0; i < 20; i++)
{
    value = rnd.Next(1, 100);
    //Добавляем / связываем блоки
    current.Add(value);
}
current.ShowAll();

Console.WriteLine("\n\rSearch");
current.Search(500);
current.Search(value);

Console.WriteLine($"Remove {value}");
current.Remove(value);
current.Remove(value);
current.ShowAll();