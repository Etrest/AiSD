//переменная бесконечности
var infinity = 999;

//иницилизация матрицы
int[,] array = new int[6, 6]
{
    {0,         3,         infinity,    3,       6,         infinity},
    {infinity,  0,         4,           7,       infinity,  4},
    {3,         8,         0,           5,       infinity,  2},
    {infinity,  6,         infinity,    0,       3,         infinity},
    {7,         infinity,  1,           4,       0,         4},
    {5,         2,         infinity,    infinity,2,         0}
};

PrintMatrix(array);
Floid(array);
Console.WriteLine("\n\rАлгоритм Флойда : ");
PrintMatrix(array);


static void PrintMatrix(int[,] array)
{
    for (var i = 0; i < 6; i++)
    {
        for (var j = 0; j < 6; j++)
            Console.Write(array[i, j] + "\t");
        Console.WriteLine();
    }
}

static void Floid(int[,] array)
{
    for (var k = 0; k < 6; k++)
    {
        for (var i = 0; i < 6; i++)
        {
            for (var j = 0; j < 6; j++)
            {
                //если текущий путь больше новому
                if (array[i, j] > array[i, k] + array[k, j])
                {
                    //присваиваем новый путь к вершине
                    array[i, j] = array[i, k] + array[k, j];
                }
            }
        }
    }
}