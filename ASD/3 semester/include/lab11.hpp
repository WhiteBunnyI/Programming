#pragma once

#include <includes.hpp>

namespace asd
{
	void QuickSort(std::vector<int>& data, size_t first, size_t last)
	{
		if (last - first <= 1) return;
		size_t pivot = last - 1;
		size_t wall = first;

		for (size_t i = first; i < last; i++)
		{
			if (data[pivot] > data[i])
			{
				std::swap(data[wall], data[i]);
				++wall;
				continue;
			}

		}

		std::swap(data[wall], data[pivot]);

		QuickSort(data, first, wall);
		QuickSort(data, wall + 1, last);
	}

	void lab11(std::vector<int>& data)
	{
		QuickSort(data, 0, data.size());
	}
}