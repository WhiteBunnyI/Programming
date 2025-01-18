#pragma once

#include <includes.hpp>

namespace asd
{
	using ullong = unsigned long long;

	bool compareLab3(std::pair<std::string, ullong>& left, std::pair<std::string, ullong>& right)
	{
		return left.second < right.second;
	}

	void lab3()
	{
		unsigned long long x;
		std::cout << "¬ведите число x:\n";
		std::cin >> x;

		int l_x = log(x);
		int m_k = l_x / log(3);
		int m_l = l_x / log(5);
		int m_m = l_x / log(7);

		std::vector < std::pair < std::string, ullong> > vec;

		for (int k = 0; k < m_k; k++)
		{
			for (int l = 0; l < m_l; l++)
			{
				for (int m = 0; m < m_m; m++)
				{
					ullong res = powl(3, k) * pow(5, l) * pow(7, m);
					std::string s = std::to_string(k) + '\t' + std::to_string(l) + '\t' + std::to_string(m) + '\t';
					if (res < x)
						vec.emplace_back(s, res);
				}
			}
		}

		std::sort(vec.begin(), vec.end(), compareLab3);

		std::cout << "k:\tl:\tm:\tr:\n";
		for each(auto var in vec)
		{
			std::cout << var.first << " " << var.second << std::endl;
		}
	}
}