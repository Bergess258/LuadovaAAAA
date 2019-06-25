#include "pch.h"
#include <iostream>
#include <string>
using namespace std;

int main()
{
	setlocale(LC_ALL, "Russian");
	const int size = 4;
	__int16 A[size], B[size], sum[size], diff[size], mullres[size], divres[size], cmpres1[size], cmpres2[size];
	__int64 n;

	// Заполнить массив с клавиатуры
	cout << "Введите массив A" << endl;
	for (int i = 0; i < size; i++)
	{
		cout << i + 1 << "." << "= ";
		cin >> A[i];
	}
	cout << "Введите массив B" << endl;
	for (int i = 0; i < size; i++)
	{
		cout << i + 1 << "." << "= ";
		cin >> B[i];
	}

	__asm //сравнение
	{
		movq  mm0, A
		movq  mm1, B
		pcmpeqw mm1, mm0
		movq cmpres1, mm1

		movq  mm1, B
		pcmpgtw 	mm1, mm0
		movq cmpres2, mm1
	}
	cout << endl;
	cout << "\nРезультат: ";
	cout << "\n равно(-1)/ неравно(0): ";
	for (int i = 0; i < size; i++)
	{
		cout << cmpres1[i] << " ";
	}
	cout << endl;
	cout << "\n больше(-1)/ меньше(0): ";
	for (int i = 0; i < size; i++)
	{
		cout << cmpres2[i] << " ";
	}
	cout << endl;


	system("pause");
}