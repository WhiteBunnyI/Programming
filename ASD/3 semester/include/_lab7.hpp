#pragma once

#include <includes.hpp>

namespace asd
{
	void lab7(std::vector<int>& data)
	{
		int d = data.size() / 2;

		for (int d = data.size() / 2; d >= 1; d /= 2)
		{
			for (int i = d; i < data.size(); i++)
			{
				int bigger = i;
				for (int o = i - d; o >= 0; o--)
				{
					if (data[bigger] < data[o])
					{
						std::swap(data[bigger], data[o]);
						bigger = o;
						continue;
					}

					break;
				}
			}
		}
	}
}