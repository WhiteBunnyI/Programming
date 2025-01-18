#pragma once

#include <includes.hpp>
#include <_lab1.hpp>

namespace asd
{
	bool IsStrContainsChar(char* chars, char chr)
	{
		for (size_t i = 0; chars[i] != '\0'; i++)
		{
			if (chars[i] == chr)
				return true;
		}

		return false;
	}

	void lab2(std::string& str)
	{
		auto GetNum = [](std::string& str, size_t& index)
			{
				std::string result;
				if (str[index] == '-')
				{
					result += '-';
					++index;
				}
				'0'; '9';
				while (str[index] >= 48 && str[index] <= 57)
				{
					result += str[index];
					++index;
				}
				return result;
			};

		if (!lab1(str))
		{
			std::cout << "Строка неправильная\n";
			return;
		}
		//Пройти проверки
		std::string main;
		Stack<char> stack;

		for (size_t i = 0; i < str.size(); i++)
		{
			const char* t;
			switch (str[i])
			{
			case '*':
			case '/':
				t = stack.Top();
				while (t)
				{
					if (*t == '(')
						break;
					if (*t == '*' || *t == '/')
					{
						main += *t;
						main += ' ';
						stack.Pop();
						t = stack.Top();
						continue;
					}
					break;
				}
				stack.Push(str[i]);
				break;

			case '+':
			case '-':
				if (i == 0 || str[i - 1] == '(')
				{
					main += GetNum(str, i) + " ";
					--i;
					continue;
				}
				t = stack.Top();
				while (t)
				{
					if (*t == '(')
						break;
					main += *t;
					main += ' ';
					stack.Pop();
					t = stack.Top();
				}
				stack.Push(str[i]);
				break;

			case '(':

				stack.Push(str[i]);
				break;

			case ')':
				t = stack.Top();
				while (*t != '(')
				{
					main += *stack.Top();
					main += ' ';

					stack.Pop();
					t = stack.Top();
				}
				stack.Pop();
				break;

			case '=':
				i = str.size();
				break;

			default:
				if (IsStrContainsChar("0123456789", str[i]))
				{
					main += GetNum(str, i) + " ";
					--i;
					break;
				}
				throw std::runtime_error("Unknown operator");
				break;
			}
		}

		while (stack.Top())
		{
			main += *stack.Top();
			main += ' ';
			stack.Pop();
		}
		std::cout << main << std::endl;
		Stack<float> temp;
		float num1;
		float num2;
		for (size_t i = 0; i < main.size(); i++)
		{
			if (main[i] == ' ')
				continue;

			if (!(main[i] == '-' && main[i + 1] == ' ') && !IsStrContainsChar("*/+", main[i]))
			{
				num1 = std::stoi(GetNum(main, i));
				temp.Push(num1);
				continue;
			}

			num2 = *temp.Top();
			temp.Pop();
			num1 = *temp.Top();
			temp.Pop();

			switch (main[i])
			{
			case '*':
				temp.Push(num1 * num2);
				break;

			case '/':
				if (!num2)
					throw std::runtime_error("Division by zero");
				temp.Push(num1 / num2);
				break;

			case '-':
				temp.Push(num1 - num2);
				break;

			case '+':
				temp.Push(num1 + num2);
				break;
			}

		}
		std::cout << *temp.Top();
	}
}