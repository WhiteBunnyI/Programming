#pragma once

#include <includes.hpp>

namespace asd 
{
	void lab8(std::vector<int>& data)
	{
		std::vector<int> temp[10];
		int r = 1;
		bool b = true;
		while (b)
		{
			b = false;
			for (int i = 0; i < data.size(); i++)
			{
				int n = (data[i] / r) % 10; // Получаем разряд 
				n ? b = true : b;

				temp[n].push_back(data[i]);
			}

			data.clear();

			//Записываем в исходный массив
			for (int i = 0; i < 10; i++)
			{
				for (int o = 0; o < temp[i].size(); o++)
				{
					data.push_back(temp[i][o]);
				}
				temp[i].clear();
			}
			//Ставим на чтение следующий разряд
			r *= 10;
		}
	}
}