#pragma once

#include <includes.hpp>

namespace asd
{
	std::vector<int> convertStrToBinTree(std::string& str)
	{
		std::vector<int> tree(100);

		size_t currentNumIndex = 0;
		int currentNum = 0;

		auto writeNum = [&tree, &currentNumIndex, &currentNum](size_t newIndex)
			{
				if (currentNumIndex >= tree.size())
					tree.resize(currentNumIndex * 2);
				if (currentNum)
				{
					tree[currentNumIndex] = currentNum;
					currentNum = 0;
				}
				currentNumIndex = newIndex;
			};

		for (size_t i = 0; i < str.size(); i++)
		{
			if (str[i] >= '0' && str[i] <= '9')
			{
				currentNum = currentNum * 10 + (str[i] - 48);
				continue;
			}

			if (str[i] == '(')
			{
				writeNum(2 * currentNumIndex + 1);
				continue;
			}

			if (str[i] == ',')
			{
				writeNum(currentNumIndex + 1);
				continue;
			}

			if (str[i] == ')')
			{
				writeNum((currentNumIndex - 2) / 2);
				continue;
			}
		}
		return tree;
	}

	//прямой
	void Direct(std::vector<int>& tree, size_t index = 0)
	{
		if (tree[index] == 0)
			return;
		std::cout << tree[index] << ' ';

		Direct(tree, index * 2 + 1);
		Direct(tree, index * 2 + 2);
	}

	//центральный
	void Center(std::vector<int>& tree, size_t index = 0)
	{
		if (tree[index] == 0)
			return;

		Center(tree, index * 2 + 1);

		std::cout << tree[index] << ' ';

		Center(tree, index * 2 + 2);
	}

	//Концевой
	void End(std::vector<int>& tree, size_t index = 0)
	{
		if (tree[index] == 0)
			return;

		End(tree, 2 * index + 1);
		End(tree, 2 * index + 2);

		std::cout << tree[index] << ' ';
	}

	//Рекурсивные обходы (прямой, центральный, концевой)
	void lab15(std::string& str)
	{
		std::vector<int> tree = convertStrToBinTree(str);
		for (size_t i = 0; i < tree.size(); i++)
		{
			std::cout << tree[i] << ' ';
		}
		std::cout << std::endl;
		Direct(tree);
		std::cout << std::endl;
		Center(tree);
		std::cout << std::endl;
		End(tree);
		std::cout << std::endl;
	}
}