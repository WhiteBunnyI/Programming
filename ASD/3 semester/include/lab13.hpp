#pragma once

#include <includes.hpp>

namespace asd
{
	class HashTable
	{
		const static size_t CONST = 10;
		using pair = std::pair<size_t, std::string>;
		std::vector<pair> table;
	public:
		HashTable() : table{ CONST, } {};
		~HashTable() = default;
		HashTable(const HashTable& other) = delete;
		void operator=(const HashTable& other) = delete;

		size_t GetHash(const std::string& word)
		{
			size_t hash = 0;
			for (size_t i = 0; word[i] != '\0'; i++)
			{
				hash = (hash + word[i]) % CONST;
			}

			return hash;
		}

		size_t Push(const std::string& word)
		{
			pair a{ GetHash(word), word };
			;
			for (size_t i = a.first; i < table.size(); i++)
			{
				if (table[i].second.empty())
				{
					table[i] = a;
					return i;
				}
				if (table[i].second == word)
					return i;
			}

			table.push_back(a);

			return table.size() - 1;
		}

		std::string Get(const size_t& hash)
		{
			return table[hash].second;
		}

		void Print()
		{
			std::cout << "Hash\t\tA.hash\t\tWord\n";
			for (size_t i = 0; i < table.size(); i++)
			{
				std::cout << i << "\t\t" << table[i].first << "\t\t" << table[i].second << '\n';
			}
		}

	};

	void lab13(char* filename)
	{
		HashTable table;
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