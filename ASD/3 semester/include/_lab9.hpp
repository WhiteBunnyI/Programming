#pragma once

#include <includes.hpp>

namespace asd
{
	bool heapify(std::vector<int>& data, int i, bool isChanged)
	{
		if (i == 0)
			return isChanged;

		int c = i;

		if (i % 2)
			c = c / 2;
		else
			c = (c - 2) / 2;

		if (data[c] < data[i])
		{
			std::swap(data[c], data[i]);
			isChanged = true;
		}
		return heapify(data, c, isChanged);
	}

	void lab9(std::vector<int>& data)
	{
		int count = data.size();
		for (int o = 0; o < count; o++)
		{
			for (int i = (count - o) / 2; i < count - o; i++)
			{
				while (heapify(data, i, false));
			}
			std::swap(data[0], data[count - 1 - o]);
		}
	}
}