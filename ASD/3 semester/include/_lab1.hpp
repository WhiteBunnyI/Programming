#pragma once

#include <includes.hpp>

namespace asd
{
	bool lab1(std::string& str)
	{
		asd::Stack<char> stack;

		//std::string str = "(){}[]()(()){()()}(([]))()()()(())()";

		//char str[10000];
		//std::cin.getline(str, 10000);
		//int len = std::strlen(str);

		if (str.size() == 0)
		{
			std::cout << "������ �� ����������\n";
			return false;
		}

		int i;
		for (i = 0; i < str.size(); i++)
		{
			char chr = str[i];
			if (chr == '(' || chr == '{' || chr == '[')
			{
				stack.Push(chr);
				continue;
			}

			char top;
			if (chr == ')' || chr == '}' || chr == ']')
			{
				if (stack.TryPop(top))
				{
					if (chr == ')' && top == '(')
					{
						stack.Pop();
						continue;
					}
					if (chr == '}' && top == '{')
					{
						stack.Pop();
						continue;
					}
					if (chr == ']' && top == '[')
					{
						stack.Pop();
						continue;
					}
				}
				break;
			}
		}


		//��������� ��� ������ � ������ �� �������� � �����
		if (i == str.size() && stack.Top() == nullptr)
		{
			//std::cout << "������ ����������\n";
			return true;
		}

		//std::cout << "������ ������������\n";
		return false;
	}
}