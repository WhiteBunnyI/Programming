#pragma once

#include <includes.hpp>

namespace asd
{
	class HashTableList
	{
		const static size_t CONST = 10;

		using list = std::list<std::string>;
		std::vector<list> table;
	public:
		using pair = std::pair<size_t, size_t>;
		HashTableList() : table{ CONST } {};
		~HashTableList() = default;
		HashTableList(const HashTableList& other) = delete;
		void operator=(const HashTableList& other) = delete;

		size_t GetHash(const std::string& word)
		{
			size_t hash = 0;
			for (size_t i = 0; word[i] != '\0'; i++)
			{
				hash = (hash + word[i]) % CONST;
			}

			return hash;
		}

		pair Push(const std::string& word)
		{
			size_t hash = GetHash(word);
			size_t index = 0;
			for (auto l = table[hash].begin(); l != table[hash].end(); ++l)
			{
				if (*l == word)
				{
					return { hash, index };
				}
				++index;
			}
			table[hash].push_back(word);
			return { hash, table[hash].size() - 1 };
			//table[GetHash(word)].push_front(word);
		}

		std::string Get(const pair& hash)
		{
			auto p = table[hash.first].begin();
			for (size_t i = 0; i < hash.second; i++)
			{
				++p;
			}
			return *p;
		}

		void Print()
		{
			std::cout << "hash\t\twords\n";
			for (size_t i = 0; i < table.size(); i++)
			{
				if (table[i].empty()) continue;
				std::cout << i << "\t\t" << *table[i].begin() << std::endl;
				for (auto l = ++table[i].begin(); l != table[i].end(); l++)
				{
					std::cout << "\t\t" << *l << std::endl;
				}
			}
		}
	};

	void lab14(char* filename)
	{
		HashTableList table;
		std::ifstream input(filename);
		std::string word;

		while (!input.eof())
		{
			input >> word;
			table.Push(word);
		}

		table.Print();
	}

}