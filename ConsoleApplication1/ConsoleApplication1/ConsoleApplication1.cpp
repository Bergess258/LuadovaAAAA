#include "pch.h"
#include <iostream>
#include <ctime>
#include "windows.h"
using namespace std;

//Сумма и вычитание идентичны за исключением команд, так что "лучше" посмотреть код одного из них
void Sum(int c,int length) 
{
	if (c == 1)
	{
		char* a = new char[length];
		char* b = new char[length];
		char* s1 = new char[length];
		char* s2 = new char[length];
		cout << "Первый массив\n";
		for (size_t i = 0; i < length; i++)
		{
			a[i] = rand() % 252 - 126;
			Sleep(50);
			b[i] = rand() % 252 - 126;
			Sleep(50);
		}
		for (size_t i = 0; i < length; i++)
		{
			cout << (int)a[i] << " ";
		}
		cout << "\nВторой массив\n";
		for (size_t i = 0; i < length; i++)
		{
			cout << (int)b[i] << " ";
		}
		cout << "\n";
		__asm {
			push ebx;
			push eax;
			push edx;
			push edi;
			push ecx;
			xor eax, eax;
			xor ebx, ebx;
			xor edx, edx;
			xor ecx, ecx;
			xor edi, edi;
			mov eax, dword ptr[s1]; //массив куда складываем левый и правый массивы для обычной арифметики
			mov ebx, dword ptr[s2]; //массив куда складываем левый и правый массивы для насыщенной арифметики
			mov edx, dword ptr[a];//массив левый
			mov edi, dword ptr[b];//массив правый
			mov ecx, dword ptr[n];//переменная определяющее количество элементов массива.
		l1:
			movq mm0, [edx];
			movq mm1, [edx]
			paddb mm0, [edi]; -это для CHAR с обычной арифметикой
			paddsb mm1, [edi]; насыщенная для беззнаковой надо дописать u перед s и так с другими инструкциями с насыщенной арифметикой
			movq[eax], mm0;
			movq[ebx], mm1;
			add edx, 8;
			add edi, 8;
			add eax, 8; смещение 8, MMX регистр 8 байт
			add ebx, 8;
			sub ecx, 8; -это для CHAR
			jc l2; это так перестраховаться
			jnz short l1;
		l2:
			emms;
			pop ecx;
			pop edi;
			pop edx;
			pop eax;
			pop ebx;
		}
		cout << "\nСложение простой арифметикой\n";
		for (size_t i = 0; i < length; i++)
		{
			cout << (int)s1[i] << " ";
		}
		cout << "\nСложение насыщенной арифметикой\n";
		for (size_t i = 0; i < length; i++)
		{
			cout << (int)s2[i] << " ";
		}
		cout << "\n";
	}
	else
		if (c == 2)
		{
			short* a = new short[length];
			short* b = new short[length];
			short* s1 = new short[length];
			short* s2 = new short[length];
			cout << "Первый массив\n";
			for (size_t i = 0; i < length; i++)
			{
				a[i] = rand() % 65534 - 32767;
				Sleep(50);
				b[i] = rand() % 65534 - 32767;
				Sleep(50);
			}
			for (size_t i = 0; i < length; i++)
			{
				cout << (int)a[i] << " ";
			}
			cout << "\nВторой массив\n";
			for (size_t i = 0; i < length; i++)
			{
				cout << (int)b[i] << " ";
			}
			cout << "\n";
			__asm {
				push ebx;
				push eax;
				push edx;
				push edi;
				push ecx;
				xor eax, eax;
				xor ebx, ebx;
				xor edx, edx;
				xor ecx, ecx;
				xor edi, edi;
				mov eax, dword ptr[s1]; //массив куда складываем левый и правый массивы для обычной арифметики
				mov ebx, dword ptr[s2]; //массив куда складываем левый и правый массивы для насыщенной арифметики
				mov edx, dword ptr[a]; //массив левый
				mov edi, dword ptr[b]; //массив правый
				mov ecx, dword ptr[n]; //переменная определяющее количество элементов массива.
			l11:
				movq mm0, [edx];
				movq mm1, [edx]
					paddsw mm1, [edi]; насыщенная
					paddw mm0, [edi]; -это для SHORT
					movq[eax], mm0;
				movq[ebx], mm1;
				add edx, 8;
				add edi, 8;
				add eax, 8; смещение 8, MMX регистр 8 байт
					add ebx, 8;
				sub ecx, 4; -это для INT
					jc l22; это так перестраховаться
					jnz short l11;
			l22:
				emms;
				pop ecx;
				pop edi;
				pop edx;
				pop eax;
				pop ebx;
			}
			cout << "\nСложение простой арифметикой\n";
			for (size_t i = 0; i < length; i++)
			{
				cout << (int)s1[i] << " ";
			}
			cout << "\nСложение насыщенной арифметикой\n";
			for (size_t i = 0; i < length; i++)
			{
				cout << (int)s2[i] << " ";
			}
			cout << "\n";
		}
		else
		{
			int* a = new int[length];
			int* b = new int[length];
			int* s1 = new int[length];
			cout << "Первый массив\n";
			for (size_t i = 0; i < length; i++)
			{
				a[i] = rand() % 2147483645 - 1073741823;
				Sleep(50);
				b[i] = rand() % 2147483645 - 1073741823;
				Sleep(50);
			}
			for (size_t i = 0; i < length; i++)
			{
				cout << (int)a[i] << " ";
			}
			cout << "\nВторой массив\n";
			for (size_t i = 0; i < length; i++)
			{
				cout << (int)b[i] << " ";
			}
			cout << "\n";
			__asm {
				push eax;
				push edx;
				push edi;
				push ecx;
				xor eax, eax;
				xor edx, edx;
				xor ecx, ecx;
				xor edi, edi;
				mov eax, dword ptr[s1]; //массив куда складываем левый и правый массивы для обычной арифметики //насыщенной нет
				mov edx, dword ptr[a];//массив левый
				mov edi, dword ptr[b];//массив правый
				mov ecx, dword ptr[n];//переменная определяющее количество элементов массива.
			l13:
				movq mm0, [edx];
				paddd mm0, [edi]; -это для  INT //Насыщенной нет
				movq[eax], mm0;
				add edx, 8;
				add edi, 8;
				add eax, 8; смещение 8, MMX регистр 8 байт
					sub ecx, 2; -это для INT
					jc l23; это так перестраховаться
					jnz short l13;
			l23:
				emms;
				pop ecx;
				pop edi;
				pop edx;
				pop eax;
			}
			cout << "\nСложение простой арифметикой\n";
			for (size_t i = 0; i < length; i++)
			{
				cout << (int)s1[i] << " ";
			}
			cout << "\n";
		}
}
int main()
{
	setlocale(LC_ALL, "Russian");
	cout << "Выберите формат\n1.Слово\n2.Двойное слово\n3.Квадрослово\n";
	int c;
	cin >> c;
	cout << "Введите размер массива. Затем он заполнится случайными числами\n";
	size_t length;
	cin >> length;
	int n = length;
	srand(time(0));
	int s;
	cout << "Выберите функцию\n1.Сумма\n2.Вычитание\n3.Умножение и деление\n";
	cin >> s;
	
	if (s == 1) Sum(c, length);
	else
		if (s == 2) 
		{

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
