// матрица связей
int[][] matrix = {
        new int[] { 0,  5,  14, 10, 0,  0,    0,    0,  0,  0 },
        new int[] { 5,  0,  19, 0,  0,  3,    0,    0,  0,  0},
        new int[] { 14, 19, 0,  6,  6,  5,    8,    0,  0,  0},
        new int[] { 10, 0,  6,  0,  11, 0,    0,    0,  0,  0},
        new int[] { 0,  0,  6,  11, 0,  0,    10,   0,  7,  0},
        new int[] { 0,  3,  5,  0,  0,  0,    9,    8,  0,  0},
        new int[] { 0,  0,  8,  0,  10, 9,    0,    3,  8,  14},
        new int[] { 0,  0,  0,  0,  0,  8,    3,    0,  0,  2},
        new int[] { 0,  0,  0,  0,  7,  0,    8,    0,  0,  3},
        new int[] { 0,  0,  0,  0,  0,  0,    14,   2,  3,  0}
    };

// размер матрицы (10)
int size = matrix.Length;

// посещенные вершины
bool[] visitedMas = new bool[size];

// минимальное расстояние
int[] minLengthMas = new int[size];

PrintMatrixRelationship(size, matrix);
Search(matrix, minLengthMas, visitedMas);
PrintMinLength(size, minLengthMas);
for (int i = 1; i <= size; i++)
{
    MinPath(i);
}


void PrintMatrixRelationship(int size, int[][] matrix)
{
    // Вывод матрицы связей
    Console.WriteLine("Матрица:\n\r");
    for (int i = 0; i < size; i++)
    {
        for (int j = 0; j < size; j++)
            Console.Write(matrix[i][j] + "\t");
        Console.WriteLine();
    }
    Console.WriteLine();
}

void Search(int[][] matrix, int[] minLengthMas, bool[] visitedMas)
{
    int temp, minIndex, min;

    //Инициализация вершин и расстояний(задаем МАКСИМАЛЬНЫй вес)
    for (int i = 0; i < minLengthMas.Length; i++)
    {
        minLengthMas[i] = int.MaxValue;
        visitedMas[i] = false;
    }
    minLengthMas[0] = 0;

    do
    {
        minIndex = int.MaxValue;
        min = int.MaxValue;
        for (int i = 0; i < minLengthMas.Length; i++)
        {
            // Если вершину ещё не посетили и вес меньше min
            if (visitedMas[i] == false && minLengthMas[i] < min)
            {
                min = minLengthMas[i];
                minIndex = i;
            }
        }
        // Добавляем найденный минимальный вес к текущему весу вершины и сравниваем с текущим минимальным весом вершины
        if (minIndex != int.MaxValue)
        {
            for (int i = 0; i < minLengthMas.Length; i++)
            {
                if (matrix[minIndex][i] > 0)
                {
                    temp = min + matrix[minIndex][i];
                    if (temp < minLengthMas[i])
                    {
                        minLengthMas[i] = temp;
                    }
                }
            }
            visitedMas[minIndex] = true;
        }
    } while (minIndex < int.MaxValue);
}

void PrintMinLength(int size, int[] minLengthMas)
{
    // Вывод кратчайших расстояний до вершин
    for (int i = 0; i < size; i++)
    {
        Console.WriteLine($"Кратчайшие расстояния до вершин 1 до {i + 1}: {minLengthMas[i]}");
    }
}

void MinPath(int lastVertex)
{
    int[] visitedVertex = new int[size]; // массив посещенных вершин
    int end = lastVertex - 1; // индекс конечной вершины
    visitedVertex[0] = lastVertex; //конечная вершина(до которой идем)
    int start = 1; // индекс первой вешины
    int weight = minLengthMas[end]; // вес конечной вершины(до которой ищем)

    while (end != 0) // пока не дошли до вершины
    {
        for (int i = 0; i < size; i++)
            if (matrix[i][end] != 0)   // проверка на наличие связи
            {
                int temp = weight - matrix[i][end]; // определяем вес пути из предыдущей вершины
                if (temp == minLengthMas[i]) // если вес совпал с рассчитанным, значит из этой вершины и был переход
                {
                    weight = temp; // сохраняем новый вес
                    end = i;       // сохраняем предыдущую вершину
                    visitedVertex[start] = i + 1; // записываем ее в массив
                    start++;
                }
            }
    }

    Console.Write($"\nВывод кратчайшего пути от 1 - {lastVertex}: \t");
    for (int i = start - 1; i >= 0; i--)
        Console.Write(visitedVertex[i] + " ");
}