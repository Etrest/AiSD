#include <stdlib.h>
#include <iostream>
using namespace std;

//Структура ветки
struct node
{
    int data;//поле данных
    node* left;//указатель на левую ветку
    node* right;//указатель на правую ветку
    int height;//высота дерева
};

//Получение высоты дерева
int height(struct node* N)
{
    if (N == NULL)//Если дерева не существует
        return 0;//высота равна 0
    return N->height;//Иначе получаем высоту
}

//Вычисление максимального значения
int max(int a, int b)
{
    return (a > b) ? a : b;//Если a>b, то max = a иначе b
}

struct node* createNode(int aData, struct node* NewNode)
{
    NewNode = new node;//выделение памяти
    NewNode->data = aData;//запись данных в узел
    NewNode->left = NULL;//задаем левую ветку
    NewNode->right = NULL;//задаем правую ветку
    NewNode->height = 1;//задаем высоту
    return NewNode;
}
//Функция поворота вправо
struct node* rightRotate(struct node* y)
{
    struct node* x = y->left;//Присваиваем указателю левую ветку дерева
    struct node* temp = x->right;//Присваиваем указателю правую ветку temp
     // Выполняем вращение
    x->right = y;
    y->left = temp;
    // Изменение высоты
    y->height = max(height(y->left), height(y->right)) + 1;//Находим максимальную ветку между левой и правой
    x->height = max(height(x->left), height(x->right)) + 1;//Находим максимальную ветку между левой и правой
    return x;//Вывод нового дерева
}
// Функция поворота влево
struct node* leftRotate(struct node* x)
{
    struct node* y = x->right;//Присваиваем указателю правую ветку 
    struct node* temp = y->left;//Присваиваем указателю левую ветку temp
     // Выполняем вращение
    y->left = x;
    x->right = temp;
    // Изменение высоты
    x->height = max(height(x->left), height(x->right)) + 1;//Находим максимальную ветку между левой и правой
    y->height = max(height(y->left), height(y->right)) + 1;//Находим максимальную ветку между левой и правой
    return y;//Вывод нового дерева
}
//Добавление элемента в дерево
struct node* insertNode(int aData, struct node* NewNode)
{
    //Если ветки не существует
    if (NewNode == NULL)
        return createNode(aData, NewNode);//создадим ее и зададим в нее данные
    if (NewNode->data > aData) //Если оно меньше того, что в этой ветке - добавим влево
        NewNode->left = insertNode(aData, NewNode->left);
    else if (NewNode->data < aData)//Иначе в ветку справа
        NewNode->right = insertNode(aData, NewNode->right);
    else
        return NewNode;
    //Изменяем высоту дерева
    NewNode->height = 1 + max(height(NewNode->left), height(NewNode->right));
    //Задаем переменную со значением разницы между высотами ветвей
    int balance;
    if (NewNode == NULL)
        balance = 0;
    else
        balance = height(NewNode->left) - height(NewNode->right);
    // Если левое поддерево левого дочернего элемента 
    if (balance > 1 && aData < NewNode->left->data)
        return rightRotate(NewNode);//Вызов функции правого поворота

    // Если правое поддерево правого потомка
    if (balance < -1 && aData > NewNode->right->data)
        return leftRotate(NewNode);//Вызов функции левого поворота

    // Если правое поддерево левого дочернего элемента
    if (balance > 1 && aData > NewNode->left->data)
    {
        NewNode->left = leftRotate(NewNode->left);
        return rightRotate(NewNode);//Вызов функции правого поворота
    }
    // если левое поддерево правого дочернего элемента
    if (balance < -1 && aData < NewNode->right->data)
    {
        NewNode->right = rightRotate(NewNode->right);
        return leftRotate(NewNode);//Вызов функции левого поворота
    }
    return NewNode;
}

//Функция поиска
void findNode(int key, node*& NewNode)
{
    if (!NewNode) {//Если ветки не существует, выход из функции   
        cout << "Элемент не найден" << endl;
        return;
    }
    else if (key < NewNode->data)//Если узел больше ключа проходим левую ветку
        return findNode(key, NewNode->left);//вызов функции для левой ветки
    else if (key > NewNode->data)//Если узел больше ключа проходим правую ветку
        return findNode(key, NewNode->right);//вызов функции для правой ветки
    else if (key == NewNode->data) {
        cout << "Элемент найден" << endl;//Если узел равен ключу элемент найден
        return;
    }
}
// Прямой обход дерева
void traversePreOrder(struct node* temp) {
    if (temp != NULL) {//Если ветка существует
        cout << " " << temp->data;//Выводим данные в узле
        traversePreOrder(temp->left);//Посещаем левое поддерево
        traversePreOrder(temp->right);//Посещаем правое поддерево
    }
}


int main()
{
    setlocale(LC_ALL, "Russian");
    int n;
    int keyFind;
    struct node* root = NULL;//Начальная вершина дерева
    cout << "Введите количество чисел" << endl;
    cin >> n;//Ввод количества узлов
    int* array = new int[n];//Выделение памяти для массива данных узлов
    cout << endl << "Входной элемент:" << endl;
    for (int i = 0; i < n; i++) {
        cin >> array[i];//Запись в массив данных от пользователя
    }
    for (int i = 0; i < n; i++) {
        root = insertNode(array[i], root);//Вызов фунции для добавления данных 
    }
    cout << "Обход дерева предзаказов:" << endl;
    traversePreOrder(root);//Вызов функции прямого обхода
    cout << endl << "Найти элемент:" << endl;
    cin >> keyFind;//Ввод элемента для поиска
    findNode(keyFind, root);//Вызов функции поиска элемента
    return 0;
}
