#include "pch.h"
#include <iostream>
#include <ctime>
#include <string>
#include "windows.h"
using namespace std;
//Из-за разных типов чисел в массивах приходилось писать разные функции для вывода массивов, разве что в разнице и сумме можно было объеденить, но было удобней оставить так
//Сумма и вычитание идентичны за исключением команд, так что "лучше" посмотреть код одного из них
//Сделать умножение и деление, а также сравнение не является возможным в данном случае//Видимо из-за того что я изначально сумма и разность сделаны по элементно не получается сделать сразу весь массив. Так как в чистом проекте эти инструкции работают предсказуемо и правильно
#pragma region Addition
void Sum(int c, size_t length, int n)
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
			pxor mm0, mm0;
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
				pxor mm0, mm0;
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
				pxor mm0, mm0;
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
#pragma endregion

#pragma region Subtraction
void Sub(int c, size_t length, int n)
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
			pxor mm0, mm0;
		l1:
			movq mm0, [edx];
			movq mm1, [edx]
			psubb mm0, [edi]; -это для CHAR с обычной арифметикой
			psubsb mm1, [edi]; насыщенная для беззнаковой надо дописать u перед s и так с другими инструкциями с насыщенной арифметикой
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
				pxor mm0, mm0;
			l11:
				movq mm0, [edx];
				movq mm1, [edx]
					psubsw mm1, [edi]; насыщенная
					psubw mm0, [edi]; -это для SHORT
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
				pxor mm0, mm0;
			l13:
				movq mm0, [edx];
				psubd mm0, [edi]; -это для  INT //Насыщенной нет
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
#pragma endregion
#pragma region MulAndDiv
void MulAndDiv() 
{
	
}
#pragma endregion

int main()
{
	setlocale(LC_ALL, "Russian");
	short cmp1[5], cmp2[5];
	int s;
	cout << "Выберите функцию\n1.Сумма\n2.Вычитание\n3.Умножение и деление\n";
	cin >> s;
	cout << "Введите размер массива. Затем он заполнится случайными числами\n";
	size_t length;
	cin >> length;
	int n = length;
	srand(time(NULL));
	if (s == 1) { 
		cout << "Выберите формат\n1.Байт\n2.Слово\n3.Двойное слово\n";//не применимо для умножения, оно не работает
		int c;
		cin >> c;
		Sum(c, length, n); 
	}
	else
		if (s == 2)
		{
			cout << "Выберите формат\n1.Байт\n2.Слово\n3.Двойное слово\n";//не применимо для умножения, оно не работает
			int c;
			cin >> c;
			Sub(c, length, n);
		}
		else
		{
			if (s == 3) 
			{
				//Умножение сколько не пробовал не работает//почему не понятно, при этом на новом проекте заработает
				int stepen;
				cout << "n" << "= ";
				cin >> stepen;
				int* a = new int[length];
				int* s1 = new int[length];
				int* s2 = new int[length];
				cout << "Первый массив\n";
				for (size_t i = 0; i < length; i++)
				{
					a[i] = rand();
					Sleep(50);
				}
				for (size_t i = 0; i < length; i++)
				{
					cout << (int)a[i] << " ";
				}
				cout << "\n";
				__asm
				{
					push eax;
					push edx;
					push edi;
					push esi;
					xor eax, eax;
					xor edx, edx;
					xor esi, esi;
					xor edi, edi;
					mov eax, dword ptr[stepen]; //степень
					mov edx, dword ptr[a];//массив левый
					mov esi, dword ptr[s1];//массив для умножения
					mov edi, dword ptr[s2];//массив для деления
					pxor mm0, mm0
						movq mm0, [edx]
						psllw  mm0, eax //умножение без знака
						movq[esi], mm0
						//деление на 2^n
						pxor mm0, mm0
						movq  mm0, [edx]
						psraw  mm0, eax //деление без знака
						movq[edi], mm0
						emms;
					pop esi;
					pop edi;
					pop edx;
					pop eax;
				}
				cout << "Умножение\n";
				for (int i = 0; i < length; i++)
				{
					cout << s1[i] << " ";
				}
				cout << "\nДеление\n";
				for (int i = 0; i < length; i++)
				{
					cout << s2[i] << " ";
				}
				cout << "\n";
			}
			else 
			{
				short a[5];
				short b[5];
				cout << "Первый массив\n";
				for (size_t i = 0; i < 5; i++)
				{
					a[i] = rand() % 10;
					Sleep(50);
					b[i] = rand() % 10;
					Sleep(50);
				}
				for (size_t i = 0; i < 5; i++)
				{
					cout << a[i] << " ";
				}
				cout << "\nВторой массив\n";
				for (size_t i = 0; i < 5; i++)
				{
					cout << b[i] << " ";
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
					pxor mm0, mm0;
					movq mm0, a;
					movq mm1, b;
					pcmpeqw mm1, mm0;
					movq cmp1, mm1;
					movq mm1, b;
					pcmpgtw mm1, mm0;
					movq cmp2, mm1;
					emms;
					pop ecx;
					pop edi;
					pop edx;
					pop eax;
					pop ebx;
				}
				cout << "\nравно(-1)/ неравно(0):\n";
				for (size_t i = 0; i < 5; i++)
				{
					cout << cmp1[i] << " ";
				}
				cout << "\nбольше(-1)/ меньше(0):\n";
				for (size_t i = 0; i < 5; i++)
				{
					cout << cmp2[i] << " ";
				}
				cout << "\n";
			}
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
