#include <iostream>
#include <ctime>

using namespace std;

#define N 9
#define M 10
#define MIN 11000
#define MAX 53000

struct hashTable
{
    int	key; // значение
    struct hashTable* a; //указатель на элемент
};
//Создание хеш-таблицы
struct hashTable** createHashTable() {
    struct hashTable** H = new hashTable * [M];
    for (int i = 0; i < M; i++)
        H[i] = NULL;//Создаем пустую хеш-таблицу размерности M
    return H;
}
//Вставка нового элемента в хеш-таблицу
void insertHashTable(int data, struct hashTable** H)
{
    struct hashTable* spot = new hashTable;
    spot->key = data;//Запись данных в хеш-таблицу
    int i = data % M;//Функция размещения
    if (H[i] == NULL) {//если позиция свободна добавляем данные
        H[i] = spot;
        spot->a = NULL;
    }
    else { // иначе в позиции создаем список из значений
        spot->a = H[i];
        H[i] = spot;
    }
}
//Поиск элемента
struct hashTable* findHashTable(int data, struct hashTable** H)
{
    int i = abs(data % M); //Функция размещения
    hashTable* spot = H[i]; //Присваиваем значение для сравнения с элементами хеш-таблицы
    while (spot != NULL) {
        if (spot->key == data) //если искомое найдено
            return spot;//вывод элемента
        spot = spot->a;//иначе двигаемя дальше по таблице
    }
    return NULL;
}
//Вывод хеш-таблицы
void showHashTable(struct hashTable** H) {
    struct hashTable* spt = new hashTable;
    for (int i = 0; i < M; i++) {
        if (H[i] != NULL && H[i]->key != -1) {//исключаем пустые позиции из вывода 
            cout << i;//выводим позицию 
            hashTable* spt = H[i];
            while (spt != NULL) {
                cout << " || " << spt->key;//выводим значения в позиции
                spt = spt->a;
            }
            cout << endl;
        }
    }
}
int main()
{
    setlocale(LC_ALL, "Russian");
    srand(time(0));
    int mas[N];
    int i, dataFind;
    for (int i = 0; i < N; i++)
        mas[i] = MIN + rand() % (MAX - MIN + 1);//Заполняем массив данными из диапазона рандомно
    cout << "Данные массива:" << endl;
    for (int i = 0; i < N; i++)
        cout << mas[i] << "||";//Вывод массива данных
    struct hashTable** H;
    H = createHashTable();//вызываем функцию, для создания хеш-таблицы
    for (i = 0; i < N; i++)
        insertHashTable(mas[i], H);//вызываем функцию вставки элемента
    cout << endl << "Хеш-таблица:" << endl;
    showHashTable(H);//Вывод хеш-таблицы
    struct hashTable* p;
    cout << endl << "Найти элемент:" << endl;
    cin >> dataFind;//вводим элемент для поиска
    p = findHashTable(dataFind, H);//вызываем функцию поиска
    if (p == NULL)
        cout << "Не найден" << endl;
    else cout << "Найден:" << p->key << endl; //выводим результат поиска
    return 0;
}
