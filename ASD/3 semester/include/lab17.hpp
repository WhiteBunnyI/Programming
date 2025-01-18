#pragma once

#include <includes.hpp>
#include <lab15.hpp>

namespace asd
{
	void printTreeStr(std::vector<int>& tree, std::size_t index = 0)
	{
		if (tree[index] == 0)
			return;
		std::cout << tree[index];
		index = index * 2 + 1;
		bool isValid = (index + 1) < tree.size();
		bool isLeft = tree[index];
		bool isRight = tree[index + 1];
		bool isHas = isValid && (isLeft || isRight);

		if (isHas)
			std::cout << '(';

		if (isLeft)
		{
			printTreeStr(tree, index);
		}

		if (isHas)
			std::cout << ',';

		++index;
		if (isRight)
		{
			printTreeStr(tree, index);
		}

		if (isHas)
		{
			std::cout << ')';
		}
	}

	void lab17(std::string& str)
	{
		std::vector<int> tree = convertStrToBinTree(str);
		int command = 4;
		/*printTree(tree);
		return;*/
		while (true)
		{
			size_t key;
			size_t index;
			int root;
			Stack<size_t> stack;

			if (command == 4)
			{
				system("cls");
				printTreeStr(tree);
				std::cout << std::endl;
			}

			std::cout << "\nВведите команду:\n1 (Поиск)\n2 (Добавить)\n3 (Удалить)\n4 (Вывести дерево)\n5 (Выйти)\n\n";
			std::cin >> command;

			switch (command)
			{
			case 1:
				std::cout << "Введите ключ для поиска: ";
				std::cin >> key;
				std::cout << std::endl;
				system("cls");
				if (key >= tree.size() || !tree[key])
				{
					std::cout << "Вершина не найдена\n";
					break;
				}
				std::cout << "Вершина найдена: " << tree[key] << std::endl;
				break;

			case 2:
				std::cout << "Введите элемент для добавления: ";
				std::cin >> root;
				std::cout << "\nВведите ключ вершины, к которому необходимо добавить элемент: ";
				std::cin >> key;
				std::cout << std::endl;
				system("cls");
				index = key * 2 + 1;

				if (index > tree.size())
					tree.resize(index);

				if (tree[index] && tree[index + 1])
				{
					std::cout << "Невозможно добавить данный элемент!\n";
					break;
				}

				if (!tree[index])
					tree[index] = root;
				else if (!tree[index + 1])
					tree[index + 1] = root;

				std::cout << "Элемент был успешно добавлен!\n";

				break;

			case 3:
				std::cout << "Введите ключ вершины для удаления: ";
				std::cin >> key;
				std::cout << std::endl;
				system("cls");

				if (!tree[key])
				{
					std::cout << "Вершины не существует!\n";
					break;
				}

				stack.Push(key);
				while (stack.Top())
				{
					index = *stack.Top();
					stack.Pop();
					if (!tree[index])
						continue;
					tree[index] = 0;
					stack.Push(index * 2 + 1);
					stack.Push(index * 2 + 2);
				}
				std::cout << "Вершина была удалена!\n";
				break;

			case 5:
				system("cls");
				printTreeStr(tree);
				std::cout << std::endl;
				system("exit");
				return;

			default:
				break;
			}


		}
	}
}