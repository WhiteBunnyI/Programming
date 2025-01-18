/*
��� ����, ���������� ������� �����.
����� � ������ N<=2000 ����, ���������� ���������� ���-�� ��������� �� ���� �������.
�������� ��������� ����� � ��������� ���� � ������� �������� ���-�� ���������.
��� ������� ����� ������� ����� ��� ���-��. ��� ��������� ����� ������ ���� �������!
*/


#include <iostream>
#include <fstream>

char GetLowerCase(char chr)						//������� ��� �������� ������� � ������ �������
{
	if (chr >= -64 && chr < -32)
	{
		return chr + 32;
	}
	return chr;
}

void StrToLower(char* str)						//������� ��� �������� ���� ������ � ������ �������
{
	int i = 0;
	while (str[i] != '\0')
	{
		str[i] = GetLowerCase(str[i]);
	}
}

bool IsVowel(char chr)							//������� ����������� ������� �� �����(������)
{
	chr = GetLowerCase(chr);
	char vowels[21] = "���������";
	for (int i = 0; i < 10; i++)
	{
		if (chr == vowels[i]) return true;
	}
	return false;
}

void AddStr(char* dest, char* source)			//������� ��� ���������� ������ � ������ ������ �����������
{
	int i = 0;
	while (source[i] != '\0')
	{
		dest[i] = source[i];
		i++;
	}
	dest[i] = '\0';
}

bool IsStrEqual(char* first, char* second)				//������� ����������� ��������� �� ��� ������
{
	if (strlen(first) != strlen(second)) return false;
	int i = 0;
	while (first[i] != '\0')
	{
		if (GetLowerCase(first[i]) != GetLowerCase(second[i])) return false;
		i++;
	}
	return true;
}

void SwapStr(char* first, char* second)					//������� �������� ������� 2 ������
{
	char temp[101];
	AddStr(temp, first);
	AddStr(first, second);
	AddStr(second, temp);

}

int main()
{
	const int N = 2000;					//���-�� ����
	const int L = 41;					//���-�� ���� � ������(20)
	std::setlocale(LC_ALL, "Rus");

	int* counts = new int[N];			//��������� �� ������ � ���-�� ��� ������� � ���
	char* words = new char[N*L];		//��������� �� ������� ������ � ����, ��� ����� ��������� �����
										//��� ����� ���� �� ����� ������� ������ ������� ������ ����������
	std::ifstream in("input.txt");		//������� ��������� �� �� ������ � �������� � ����

	int current = 0;					//���-�� ��������� ����, �����-���� �������
	while (!in.eof())
	{
		char word[L];
		int count = 0;					//���-�� ��� ������� � �����
		
		in >> word;						//��������� �����
		
		int i = 0;
		while (word[i] != '\0')			//��������� � ������ ������� � ��������� ���� ����
		{
			if (i > 0 && IsVowel(word[i - 1]) && IsVowel(word[i]))
			{
				count++;
			}
			i++;
		}
		if (count != 0)
		{
			counts[current] = count;			//���������� ���-�� ��� ������� �����
			AddStr(words + L*current, word);	//���������� ����� � ������
			current++;							
		}
		
		std::cout << word << " " << count << std::endl;
	}

	for (int i = 0; i < current-1; i++)				//��������� �� �������� ���-�� ��� �������
	{
		for (int o = i + 1; o < current; o++)
		{
			if (counts[o] == 0) continue;
			if (IsStrEqual(words + L * i, words + L * o))	//���� ������� ������������� ����� - 
			{												//������ ��� ���-�� ��� ������� � 0
				counts[o] = 0;								//� ���������� ����� � ������� ���-��� ��� ����� ��������������
				continue;
			}
			if (counts[i] < counts[o])						//���������� ���������
			{
				std::swap(counts[i], counts[o]);
				SwapStr(words + L * i, words + L * o);
			}
		}
	}

	std::fstream out("output.txt");
	for (int i = 0; i < current; i++)						//���������� � �������� ���� ���������� �����
	{
		if (counts[i] == 0) continue;						//���������� ����� � 0 ���-��� ��� �������
		std::cout << counts[i] << " " << words + L * i << std::endl;
		out << words + L * i << std::endl;
	}

	return 0;
}