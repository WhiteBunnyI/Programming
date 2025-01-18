#pragma once

#include <includes.hpp>
#include <lab11.hpp>

namespace asd
{
	void lab12(std::string filename)
	{
		const size_t read = 4096;

		std::ifstream input1(filename);
		std::ifstream input2;
		std::ofstream out;

		std::vector<int> temp;
		size_t current = 0;

		std::filesystem::create_directory("./tempDir/");

		while (!input1.eof())
		{
			temp.resize(read);
			size_t count = 0;
			for (; count < read && !input1.eof(); count++)
			{
				input1 >> temp[count];
			}

			temp.resize(count);
			lab11(temp);
			out.open("./tempDir/0temp" + std::to_string(current));

			for (size_t i = 0; i < count; i++)
			{
				out << temp[i] << ' ';
			}

			out.close();
			temp.clear();
			++current;
		}
		input1.close();

		size_t cycle = 0;
		while (current != 1)
		{
			size_t i = 0;
			for (; i < current / 2; i++)
			{
				input1.open("./tempDir/" + std::to_string(cycle) + "temp" + std::to_string(i * 2));
				input2.open("./tempDir/" + std::to_string(cycle) + "temp" + std::to_string(i * 2 + 1));
				out.open("./tempDir/" + std::to_string(cycle + 1) + "temp" + std::to_string(i));
				int t1;
				int t2;
				input1 >> t1;
				input2 >> t2;

				bool isOpen1 = !input1.eof();
				bool isOpen2 = !input2.eof();

				while (isOpen1 || isOpen2)
				{
					if (isOpen1 && isOpen2)
					{
						if (t1 < t2)
						{
							out << t1 << ' ';
							input1 >> t1;
							isOpen1 = !input1.eof();
						}
						else
						{
							out << t2 << ' ';
							input2 >> t2;
							isOpen2 = !input2.eof();
						}
					}
					else if (isOpen1)
					{
						out << t1 << ' ';
						input1 >> t1;
						isOpen1 = !input1.eof();
					}
					else
					{
						out << t2 << ' ';
						input2 >> t2;
						isOpen2 = !input2.eof();
					}

				}
				input1.close();
				input2.close();
				out.close();
			}
			if (current % 2)
			{
				std::filesystem::rename("./tempDir/" + std::to_string(cycle) + "temp" + std::to_string(i * 2), "./tempDir/" + std::to_string(cycle + 1) + "temp" + std::to_string(current / 2));
				++current;
			}
			++cycle;
			current /= 2;
		}

		std::filesystem::rename("./tempDir/" + std::to_string(cycle) + "temp" + '0', "./result.txt");
		std::filesystem::remove_all("./tempDir");
	}
}