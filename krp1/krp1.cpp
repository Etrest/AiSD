#include <stdlib.h>
#include <iostream>

using namespace std;
//Структура ветки
struct node
{
    int data;//поле данных
    node* left;//указатель на левую ветку
    node* right;//указатель на правую ветку
};
//Создание нового узла с заданными левой и правой ветками
struct node* createNode(int aData, struct node* NewNode) {
    NewNode = new node;//выделение памяти
    NewNode->data = aData;//запись данных в узел
    NewNode->left = NULL;//задаем левую ветку
    NewNode->right = NULL;//задаем правую ветку
    return NewNode;
}
//Функция добавления данных
struct node* insertNode(int aData, struct node* NewNode)
{
    //Если ветки не существует
    if (NewNode == NULL) { //создадим ее и зададим в нее данные
        return createNode(aData, NewNode);
    }
    else { //Иначе сверим вносимое
        if (NewNode->data > aData) //Если оно меньше того, что в этой ветке - добавим влево
            NewNode->left = insertNode(aData, NewNode->left);
        else //Иначе в ветку справа
            NewNode->right = insertNode(aData, NewNode->right);
        return NewNode;
    }
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
//Освобождение памяти, выделенной под бинарное дерево
void freeTree(node* NewNode)
{
    if (!NewNode) return; //Если ветки не существует, выход из функции
    freeTree(NewNode->left);//Посещаем левое поддерево
    freeTree(NewNode->right);//Посещаем правое поддерево
    delete NewNode;//удаляем узел
    return;
}
// Прямой обход дерева
void traversePreOrder(struct node* temp) {
    if (temp != NULL) {//Если ветка существует
        cout << " " << temp->data;//Выводим данные в узле
        traversePreOrder(temp->left);//Посещаем левое поддерево
        traversePreOrder(temp->right);//Посещаем правое поддерево
    }
    else return;
}
// Симметричный обход дерева
void traverseInOrder(struct node* temp) {
    if (temp != NULL) {//Если ветка существует
        traverseInOrder(temp->left);//Посещаем левое поддерево
        cout << " " << temp->data;//Выводим данные в узле
        traverseInOrder(temp->right);//Посещаем правое поддерево
    }
    else return;
}

// Обратный обход дерева
void traversePostOrder(struct node* temp) {
    if (temp != NULL) {//Если ветка существует
        traversePostOrder(temp->left);//Посещаем левое поддерево
        traversePostOrder(temp->right);//Посещаем правое поддерево
        cout << " " << temp->data;//Выводим данные в узле
    }
    else return;
}
//Поиск минимального значения данных в дереве
struct node* findMin(struct node* NewNode)
{
    if (NewNode == NULL)//Если ветка не существует
        return NULL;
    else if (NewNode->left != NULL) //Если у узла есть дочерный элемент, двигаемся по ветке
        return findMin(NewNode->left);
    else return NewNode;// Минимальный элемент найден
};
//Удаление элемента дерева
struct node* removeNode(int x, node* NewNode) {
    if (NewNode == NULL) return NULL;//Если ветка не существует
    if (x > NewNode->data)//Если искомый элемент больше узла
        NewNode->right = removeNode(x, NewNode->right);//Продолжаем поиск в правой ветке
    else if (x < NewNode->data)//Если искомый элемент меньше узла
        NewNode->left = removeNode(x, NewNode->left);//Продолжаем поиск в левой ветке
    else
    {
        if (NewNode->left == NULL && NewNode->right == NULL)//Если у узла нет веток
        {
            delete(NewNode);//Удаляем узел
            return NULL;
        }
        else if (NewNode->left == NULL || NewNode->right == NULL)//Если у узла одна ветка
        {
            struct node* temp;//Берем дополнительный указатель
            if (NewNode->left == NULL) {
                temp = NewNode->right; //Присваиваем дочерний элемент указателю
                delete(NewNode);//Удаляем узел
                return temp;//Оставляем дочерний элемент, вместо удаленного узла
            }
            else {
                temp = NewNode->left;//Присваиваем дочерний элемент указателю
                delete(NewNode);//Удаляем узел
                return temp;//Оставляем дочерний элемент, вместо удаленного узла
            }
        }
        //Если у узла две ветки
        else {
            struct node* temp = findMin(NewNode->right);//Присваиваем минимальный в правой ветке указателю
            NewNode->data = temp->data;//Присваиваем узлу данные минимального в правой ветке
            NewNode->right = removeNode(temp->data, NewNode->right);//удаляем элемент, который стал на место удаленного
        }
    }
    return NewNode;
};


int main() {
    setlocale(LC_ALL, "Russian");
    struct node* root = NULL;//Начальная вершина дерева
    int n;//Количество узлов
    int keyFind;
    int keyRemove;
    cout << "Введите количество чисел" << endl;
    cin >> n;//Ввод количества узлов
    int* array = new int[n];//Выделение памяти для массива данных узлов
    for (int i = 0; i < n; i++) {
        cin >> array[i];//Запись в массив данных от пользователя
    }
    root = insertNode(array[0], root);
    for (int i = 1; i < n; i++) {
        insertNode(array[i], root);//Вызов фунции для добавления данных 
    }
    cout << "Обход дерева прямой:" << endl;
    traversePreOrder(root);//Вызов функции прямого обхода
    cout << "\nОбход дерева симметричным:" << endl;
    traverseInOrder(root);//Вызов функции симметричного обхода
    cout << "\nОбход дерева обратным:" << endl;
    traversePostOrder(root);//Вызов функции обратного обхода
    cout << endl << "Введите номер найти" << endl;
    cin >> keyFind;
    cout << "Поиск элемента:" << endl;
    findNode(keyFind, root);//Вызов функции поиска элемента
    cout << "Введите номер удалить" << endl;
    cin >> keyRemove;
    removeNode(keyRemove, root);//Вызов функции удаления элемента
    cout << "Удалить дерево элементов" << endl;
    traversePreOrder(root);
    delete[] array;//Удаление памяти массива array
    freeTree(root);//Вызов функции освобождения памяти, выделенной под бинарное дерево
}
