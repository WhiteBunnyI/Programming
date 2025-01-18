#pragma once

#include <includes.hpp>

namespace asd
{
	void lab4(std::vector<int>& data)
	{
		float factor = 0.85f;

		int step = data.size() - 1;

		while (step >= 1)
		{
			for (int i = 0; i + step < data.size(); i++)
			{
				if (data[i] > data[i + step])
				{
					std::swap(data[i], data[i + step]);
				}
			}
			step *= factor;
		}
	}
}