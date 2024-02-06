#include <iostream>
#define Max 50
#define INF 0xcffff

using namespace std;

struct Graph {
	int vertex, edge; //количество вершин и ребер
	int arcs[Max][Max];//матрица весов ребер
};

int dis[Max][Max], path[Max][Max];//dis сохраняет кратчайшее расстояние, path сохраняет путь			
void Floyd(Graph& G)
{
	//Создаем матрицы кратчайших расстояний, пути
	for (int i = 1; i <= G.vertex; i++)
		for (int j = 1; j <= G.vertex; j++)
		{
			dis[i][j] = G.arcs[i][j];//Присваиваем данные из матрицы в dis
			if (dis[i][j] != INF && i != j) 
				path[i][j] = i;//Если значение не 0, не бесконечность, значит это путь. Присваиваем переменной
			else path[i][j] = -1; //иначе обозначаем путь -1
		}

	//Реализация алгоритма
	for (int k = 1; k <= G.vertex; k++)	//Берем промежуточную вершину				
		for (int i = 1; i <= G.vertex; i++)
			for (int j = 1; j <= G.vertex; j++)
			{
				if (dis[i][k] + dis[k][j] < dis[i][j]) {//	если k промежуточная вершина кратчайшего пути
					dis[i][j] = dis[i][k] + dis[k][j]; // изменяем значение в матрице расстояний
					path[i][j] = k;			//сохраняем вершину в матрице пути					
				}
			}
}
//Поиск узлов
void find(int x, int y)
{
	if (path[x][y] == x) {//Если между x и y нет узла, завершите поиск						
		cout << 1;
		return;
	}
	else {					//Если между x и y есть узел t, найдите t, y				
		int t = path[x][y];
		find(t, y);
		
		cout << "||" << t;
	}
	return;
}
//Создаем матрицу смежности
void adjacencyMatrix(Graph& G)
{
	int u, v, w;
	cout << "Введите количество вершин и ребер графа:" << endl;
	cin >> G.vertex >> G.edge;//Получаем количество вершин и ребер графа
	for (int i = 1; i <= G.vertex; i++)
		for (int j = 1; j <= G.vertex; j++)
		{
			if (i == j) G.arcs[i][j] = 0;//на диагонали ставим 0
			else G.arcs[i][j] = INF;//в остальных случаях бесконечность
		}
	for (int i = 0; i < G.edge; i++)
	{
		
		cout << "Введите вершины u, v и расстояние между ними:" << endl;
		cin >> u >> v >> w;//Получаем расстояние между вершинами u, v
		G.arcs[u][v] = w;//Записываем в матрицу
	}
}
//Вывод матрицы кратчайшего пути, кратчайшее расстояние 
void showResultMatrix(Graph& G)
{
	
	cout << "Матрица результатов:" << endl;
	for (int i = 1; i <= G.vertex; i++)
	{
		for (int j = 1; j < G.vertex; j++)
		{
			cout.width(8);
			cout << dis[i][j] << "||"; //Вывод матрицы кратчайшего пути
		}
		cout.width(8);
		cout << dis[i][G.vertex] << endl;//Вывод кратчайшее расстояние
	}
}
//Вывод пути от начальной заданной точки до каждой точки
void showWay(Graph& G, int v) {
	for (int i = 2; i <= G.vertex; i++)
	{

		cout << "Из начальной точки v1 в v" << i << "  Расстояние кратчайшего пути равно: " << dis[v][i] << endl;
		cout << "Путь: ";
		find(1, i);//Вызов функции поиска вершин
		cout << "||" << i << endl;
	}
}

int main()
{
	setlocale(LC_ALL, "Russian");
	Graph G;
	adjacencyMatrix(G);//Вызов функции для матрицы смежности
	Floyd(G);//Вызов функции выполнения алгоритма Флойда
	showResultMatrix(G);//Вызов функции с результирующей матрицей
	showWay(G, 1);//Вызов функции пути, для первой вершины
	return 0;
}
