/*
� ��������� ����� input.txt ������� ������� �����. 
����� � ������ �����, ���������� �����, �� �������� �� � ���� �� ���� ������ � ������������ ������, 
�������� �� ���������� ������� � ������� ����� ������� ������ ����� � ������� ��������� �����. 
���������� ����� �������� � ���� output.txt. ���� �����, ����� ��������� ����, ������ �������� ����������, ������� � ����� ����������.

��� �����:
�����, ������� ��� � ������ � ����. ������ - �������� ���������� ������� � ������� � ������� ����� �����. 
�����, ������� ����������� � ������ � ����. ������ - �������� ��� ���������.
�����, � ������� ����������� ��� ����� �� ���� � ����. ������ ��� ���� ����� � ����. ������ - �������� ��� ���������.
*/

#include <iostream>
#include <fstream>
#include "StringFunctions.hpp"

void ResetBoolMassive(bool *mas, int len)
{
	for (int i = 0; i < len; i++)
	{
		mas[i] = false;
	}
}

bool IsHaveCharInBoolMassive(char chr, bool* mas)
{
	return mas[chr + 32];
}

void CheckAndWriteChars(char* str, bool* out)
{
	int i = 0;
	while (str[i] != '\0')
	{
		char chr = str::ToLower(str[i]);
		if (str::IsRusChar(chr))
			out[chr + 32] = true;
		
		i++;
	}
}
//���������, ���� �� � str �����, ������� ��� � ������ � ������������ ������.
//���� ����������� �����, ������� ��� � ������ � ����. ������, ���������� true
//���� ����� ���� ��� - ��������� false
bool CheckCharsInStr(char* str, bool* alphabet)
{
	int i = 0;
	while (str[i] != '\0')
	{
		if (!IsHaveCharInBoolMassive(str::ToLower(str[i]), alphabet)) return true;
		i++;
	}
	return false;
}

int main()
{
	setlocale(LC_ALL, "Rus");
	
	char** words = new char* [2000];

	std::ifstream in("input.txt");
	if (!in.is_open())
	{
		std::cout << "���� �� ����������";
		return 0;
	}

	int countWords = 0;
	int maxLenWord = 0;
	bool alphabet[32];

	ResetBoolMassive(alphabet, 32);					//�������������� ������

	while (!in.eof())								//��������� �����
	{
		char word[101];
		in >> word;

		char* w = new char[strlen(word) + 1];
		str::CopyStr(w, word);
		words[countWords] = w;

		int countChar = str::CountCharsInStr(w);	//���-�� ������� ���� � �����
		if (maxLenWord < countChar)					
		{
			maxLenWord = countChar;
			ResetBoolMassive(alphabet, 32);
			CheckAndWriteChars(words[countWords], alphabet);	//��������� ������ alphabet, � ������� ������� i ����� ����� true, 
		}														//���� ���������� ��������� ������� ����� � ������� i
		else if(maxLenWord == countChar)
			CheckAndWriteChars(words[countWords], alphabet);

		countWords++;
	}


	char* out = new char[maxLenWord*countWords*countWords];			//������ � ���������� �����������
	int p = 0;														//��������� �� ������� ������� � ������
	for (int i = 0; i < countWords; i++)
	{
		int len = strlen(words[i]);
		int lenChr = len;
		char sign;
		bool isDontHaveChar = CheckCharsInStr(words[i], alphabet);	//��. �������� �������

		if (!str::IsRusChar(words[i][len - 1]))						//���� ��� �� ������� �����, �.�. ���� ����������
		{
			lenChr--;												//��������� ����� �����
			sign = words[i][lenChr];								//��������� ���� ����������
		}

		if (lenChr == maxLenWord || !isDontHaveChar)				//���� ��� ����� � ����. ������ ��� � ���� ����� ��� ����, �������
		{															//����������� � ������ � ����. ������
			for (int o = 0; o < len; o++)							//�� ������ ������������ ��� �� ����� ������ �� ������ ����������(���� �� ����)
			{
				out[p] = words[i][o];
				p++;
			}
			out[p] = (char)32;
			p++;
			continue;
		}

		int end = p + lenChr + 1;									//��������� �� ����� �����
		out[end - 1] = '(';
		for (int o = 0; o < lenChr; o++)
		{
			if (!IsHaveCharInBoolMassive(words[i][o], alphabet))	//���� ������ ����� �� ����������� � ����� � ����. ������
			{
				out[end] = str::ToLower(words[i][o]);				//���������� ����� � �����
				out[p] = str::ToUpper(words[i][o]);					//���������� ����� � ������� ������� � ������ � ��������� �����
				end++;
			}
			else													//���� ������ ����� ����������� - ������ ������������ ��.
			{
				out[p] = words[i][o];
			}
			
			p++;
		}

		out[end] = ')';												
		p = end + 1;

		if (len != lenChr)											//� ������, ���� � ��� ���� ���� ����������
		{
			out[p] = sign;											//������ ��� � ����� ����� ������
			p++;
		}
		out[p] = (char)32;
		p++;
	}
	out[p] = '\0';

	std::ofstream output("output.txt");

	output << out;
	
	return 0;
}