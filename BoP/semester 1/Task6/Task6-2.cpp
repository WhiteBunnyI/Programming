/*
���� ������, ���������� ������� �����.
���� � ������ ���� �����, � ����p�� ���� ��� ���������� ������� �����,
�� ������� �� ���� ������ ������ ���������,
� ��������� ������ �p������p����� � ������, ����p����� �� ����� 3-� �������, ������� �����.
���������� ����� ������� � ���������� ��p����. 
(������ ���������: ����������)
*/

#include <iostream>
#include <fstream>

void CopyStr(char* first, char* second)
{
	int i = 0;
	while (second[i] != '\0')
	{
		first[i] = second[i];
		i++;
	}
	first[i] = '\0';
}

bool IsLocatedInStr(char chr, char* str, int& pos)
{
	int i = 0;
	while (str[i] != '\0')
	{
		if (chr == str[i])
		{
			pos = i;
			return true;
		}
		i++;
	}
	return false;
}

char ToLower(char chr)
{
	if (chr >= -64 && chr < -32) return chr + 32;
	return chr;
}

bool IsVowel(char chr)
{
	chr = ToLower(chr);
	char vowels[] = "���������";
	int temp;
	return IsLocatedInStr(chr, vowels, temp);
}

bool IsVowel(char chr, int& pos)
{
	chr = ToLower(chr);
	char vowels[] = "���������";
	return IsLocatedInStr(chr, vowels, pos);
}

bool IsVoiceless(char chr)
{
	chr = ToLower(chr);
	char voiceless[] = "����������";
	int temp;
	return IsLocatedInStr(chr, voiceless, temp);
}

void DeleteCharInStr(char* str, int pos)
{
	int i = pos + 1;
	while (str[i] != '\0')
	{
		str[i - 1] = str[i];
		i++;
	}
	str[i - 1] = '\0';

}

void DuplicateCharInStr(char*& str, int pos)
{
	int i = pos + 1;
	char pastChar = str[pos];
	char currentChar;
	do
	{
		currentChar = str[i];
		str[i] = pastChar;
		i++;
		pastChar = currentChar;

	} while (currentChar != '\0');
	str[i] = '\0';

}

bool IsHavePairVowel(char* word)
{
	bool vowels[10] = { 0,0,0,0,0,0,0,0,0,0 };
	int i = 0;
	int pos = 0;
	while (word[i] != '\0')
	{
		bool isVowel = IsVowel(word[i], pos);
		if (isVowel && vowels[pos]) return true;
		if (isVowel) vowels[pos] = true;
		i++;
	}
	return false;
}

int CountVowelsInStr(char* str)
{
	int i = 0;
	int count = 0;
	while (str[i] != '\0')
	{
		if (IsVowel(str[i])) count++;
		i++;
	}
	return count;
}

int main()
{
	setlocale(LC_ALL, "Rus");

	int N = 2000;

	char** words = new char* [N];

	std::ifstream in("input.txt");

	
	int count = 0;								//���-�� ����
	bool isHavePairVowel = false;				//������� �� ���� ������� � �����
	while (!in.eof())
	{
		char word[101];
		in >> word;

		char* w = new char[strlen(word)*2+1];
		CopyStr(w, word);
		words[count] = w;

		if (!isHavePairVowel) isHavePairVowel = IsHavePairVowel(w);

		count++;
	}

	if (isHavePairVowel)							//������� ������ ����� � ������
	{
		for (int i = 0; i < count; i++)
		{
			int o = 0;
			while (words[i][o] != '\0')
			{
				if (IsVoiceless(words[i][o]))		//���� ������ - ������� ��� �����
				{
					DeleteCharInStr(words[i], o);
					o--;
				}
				o++;
			}

			if (o == 0)								//���� ���� �� �������� - ������� ��������� �� �������
			{
				delete[] words[i];
				for (int o = i + 1; o < count; o++)
				{
					words[o - 1] = words[o];
				}
				count--;
				i--;
			}
		}
	}
	else											//����� ��������� ������� � ������, ������� ����� �� ����� 3 ������� ����
	{
		for (int i = 0; i < count; i++)
		{
			if (CountVowelsInStr(words[i]) >= 3)
			{
				int o = 0;
				while (words[i][o] != '\0')
				{
					if (IsVowel(words[i][o]))
					{
						DuplicateCharInStr(words[i], o);
						o++;
					}
					o++;
				}
			}
		}
	}

	for (int i = 0; i < count - 1; i++)				//��������� ����� � ���������� �������
	{
		for (int o = i + 1; o < count; o++)
		{
			int currentChr = 0;
			while (words[i][currentChr] != '\0')
			{
				char iC = ToLower(words[i][currentChr]);
				char oC = ToLower(words[o][currentChr]);

				if (iC < oC) break;
				
				if ((words[o][currentChr] == '\0') || (iC > oC))
				{
					std::swap(words[i], words[o]);
					break;
				}

				currentChr++;
			}
		}
	}

	for (int i = 0; i < count; i++)
	{
		std::cout << words[i] << std::endl;
	}

	return 0;
}