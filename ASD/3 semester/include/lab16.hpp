#pragma once

#include <includes.hpp>
#include <lab15.hpp>

namespace asd
{
	void lab16(std::string& str)
	{
		std::vector<int> tree = convertStrToBinTree(str);
		//for (size_t i = 0; i < tree.size(); i++)
		//{
		//	std::cout << tree[i] << ' ';
		//}
		//std::cout << std::endl;

		Stack<size_t> needToCheck;
		needToCheck.Push(0);

		while (needToCheck.Top())
		{
			size_t index = *needToCheck.Top();
			needToCheck.Pop();

			if (!tree[index])
				continue;

			std::cout << tree[index] << ' ';

			size_t next = 2 * index + 2;
			if (next < tree.size())
				needToCheck.Push(next);

			--next;
			needToCheck.Push(next);

		}
		std::cout << std::endl;

	}
}