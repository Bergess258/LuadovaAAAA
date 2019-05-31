#include "pch.h"
#include <iostream>
using namespace std;

int main()
{
	setlocale(LC_ALL, "Russian");
	cout << "Выберите формат\n1.Слово\n2.Двойное слово\n3.Квадрослово\n";
	int c;
	cin >> c;
	cout << "Введите размер массива. Затем он заполнится случайными числами\n";
	size_t length;
	cin >> length;
	if (c == 1) 
	{
		char* a = new char[length];
		char* b = new char[length];
		char* s = new char[length];
		cout << "Первый массив\n";
		for (size_t i = 0; i < length; i++)
		{
			a[i] = rand() % 252 -126;
			b[i] = rand() % 252 - 126;
		}
		for (size_t i = 0; i < length; i++)
		{
			cout << (int)a[i]<<" ";
		}
		cout << "\nВторой массив\n";
		for (size_t i = 0; i < length; i++)
		{
			cout << (int)b[i] << " ";
		}
		cout << "\n";
		__asm {
			movq mm0, qword ptr[a]
			paddb mm0, qword ptr[b]
			mov s, mm0
		}
	}
	else
		if (c == 2) 
		{
			short* a = new short[length];
			short* b = new short[length];
			for (size_t i = 0; i < length; i++)
			{
				a[i] = rand() % 10;
				b[i] = rand() % 10;
			}
			for (size_t i = 0; i < length; i++)
			{
				cout << a[i] + " ";
			}
			cout << "\n";
			for (size_t i = 0; i < length; i++)
			{
				cout << b[i] + " ";
			}
			cout << "\n";
		}
		else 
		{
			int* a = new int[length];
			int* b = new int[length];
			for (size_t i = 0; i < length; i++)
			{
				a[i] = rand() % 10;
				b[i] = rand() % 10;
			}
			for (size_t i = 0; i < length; i++)
			{
				cout << a[i] + " ";
			}
			cout << "\n";
			for (size_t i = 0; i < length; i++)
			{
				cout << b[i] + " ";
			}
			cout << "\n";
		}
	
}

// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
