#pragma once

#include <includes.hpp>

namespace asd
{
	void lab10(std::vector<int>& data)
	{
		size_t count = data.size();
		for (size_t i = 1; i < count; i *= 2) //���-�� ��������� � ����������� �������
		{
			std::vector<int> temp;
			size_t size = 2 * i;
			for (size_t o = 0; o < count; o += size) //������� ����� ������
			{
				size_t left = o;					//����� ������� ������ �������
				size_t right = left + i;			//����� ������� ������� �������
				size_t leftEnd = right;
				size_t rightEnd = right + i;
				bool leftValid = left != leftEnd && left < count;		//�������� ������ ������ �������
				bool rightValid = right != rightEnd && right < count;	//�������� ������ ������� �������
				while (rightValid || leftValid)
				{
					rightValid = right != rightEnd && right < count;
					leftValid = left != leftEnd && left < count;

					if (rightValid && leftValid)
					{
						if (data[left] < data[right])
						{
							temp.push_back(data[left]);
							++left;
						}
						else
						{
							temp.push_back(data[right]);
							++right;
						}
					}
					else if (rightValid)
					{
						temp.push_back(data[right]);
						++right;
					}
					else if (leftValid)
					{
						temp.push_back(data[left]);
						++left;
					}
				}
			}
			data = temp;
		}
	}
}