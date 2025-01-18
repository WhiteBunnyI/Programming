#pragma once

#include <includes.hpp>

namespace asd
{
	void lab5(std::vector<int>& data)
	{
		for (int i = 1; i < data.size(); i++)
		{
			int pos = i;
			for (int o = i - 1; o >= 0; o--)
			{
				if (data[pos] < data[o])
				{
					std::swap(data[pos], data[o]);
					pos = o;
				}
			}
		}

	}
}