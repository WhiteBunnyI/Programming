#pragma once

#include <includes.hpp>

namespace asd
{
	void lab6(std::vector<int>& data)
	{
		for (int i = 0; i < data.size(); i++)
		{
			int minimum = i;
			for (int o = i + 1; o < data.size(); o++)
			{
				if (data[minimum] > data[o])
					minimum = o;
			}
			if (i != minimum)
				std::swap(data[i], data[minimum]);
		}
	}
}