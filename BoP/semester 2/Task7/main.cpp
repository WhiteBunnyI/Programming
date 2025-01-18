#include <iostream>
#include <fstream>
#include <string>

// N < 256 и a_i < 256, для всех i=1..N
// .txt - массив данных в формате N и a_i, где i=1..N (ASCII)
// .bin - массив данных в формате N и a_i, где i=1..N (bin)

class DataReader
{
protected:
	std::ifstream m_in;
	std::string m_filename;
	uint8_t* m_data;
	uint8_t m_n;

public:
	DataReader(const std::string& filename) : 
		m_filename(filename), m_n(0), m_data(nullptr) {}

	virtual ~DataReader() {}

	virtual bool Open() = 0;
	void Close()
	{
		m_in.close();
	}

	virtual void Read() = 0;
	virtual void Write() = 0;

	virtual void GetData(uint8_t* buf, uint32_t& n)
	{
		n = m_n;
		std::copy(m_data, m_data + m_n, buf);
	}
};

class TxtReader : public DataReader
{
public:
	TxtReader(const std::string& filename) : DataReader(filename) {}
	virtual ~TxtReader() 
	{
		if(m_data != nullptr)
			delete[] m_data;
	}

	bool Open() override
	{
		m_in.open(m_filename);
		if (!m_in.is_open())
			return false;
		return true;
	}

	void Read() override
	{
		int tmp;
		m_in >> tmp;
		m_n = tmp;
		m_data = new uint8_t[m_n];
		for (int i = 0; i < m_n; i++)
		{
			int tmp;
			m_in >> tmp;
			m_data[i] = tmp;
		}
	}

	void Write() override
	{
		std::ofstream out(m_filename);
		unsigned int count;
		std::cin >> count;
		char* chars = new char[count*2];
		std::string str = std::to_string(count) + '\n';
		for (int i = 0; i < count; i++)
		{
			char chr;
			std::cin >> chr;
			str += std::to_string((int)chr) + ' ';

		}
		out.write(str.c_str(), str.length());

	}
};

class BinReader : public DataReader
{
public:
	BinReader(const std::string& filename) : DataReader(filename) {}
	virtual ~BinReader()
	{
		if (m_data != nullptr)
			delete[] m_data;
	}

	bool Open() override
	{
		m_in.open(m_filename, std::ios::binary);
		if (!m_in.is_open())
			return false;
		return true;
	}

	void Read() override
	{
		m_in.read((char*)&m_n, 1);
		m_data = new uint8_t[m_n];
		m_in.read((char*)m_data, m_n);
	}

	void Write() override
	{
		std::ofstream out(m_filename, std::ios::binary);
		unsigned int count = 0;
		std::cin >> count;
		uint8_t c = (uint8_t)count;
		uint8_t* buf = new uint8_t[c+1];
		buf[0] = c;
		for (int i = 1; i <= c; i++)
		{
			uint8_t t;
			std::cin >> t;
			buf[i] = t;
		}

		out.write((char*)buf, c+1);
		delete[] buf;
	}
};

class BinfReader : public DataReader
{
private:
	float GetFloat(int pos)
	{
		float res;
		uint8_t* p = (uint8_t*)&res;
		for (int i = 0; i < 4; i++)
		{
			uint8_t t = m_data[pos + i];
			*(p + i) = t;
		}
			
		return res;
	}
	void GetBits(float value, char* buf, int start)
	{
		char* bit = (char*)&value;
		for (int i = 0; i < 4; i++)
			buf[i + start] = *(bit + i);
	}
	void GetBits(unsigned int value, char* buf, int start)
	{
		char* bit = (char*)&value;
		for (int i = 0; i < 4; i++)
			buf[i + start] = *(bit + i);
	}
public:
	BinfReader(const std::string& filename) : DataReader(filename) {}
	~BinfReader()
	{
		if (m_data != nullptr)
			delete[] m_data;
	}
	bool Open() override
	{
		m_in.open(m_filename, std::ios::binary);
		if (!m_in.is_open())
			return false;
		return true;
	}

	void Read() override
	{
		char count[4];
		m_in.read(count, 4);
		m_n = ((int)count[3] << 24 | (int)count[2] << 16 | (int)count[1] << 8 | (int)count[0]);
		m_data = new uint8_t[m_n*4];
		m_in.read((char*)m_data, m_n*4);
	}

	void GetDataF(float* buf, uint32_t& n)
	{
		n = m_n;
		float* tmp = (float*)(m_data);

		for (int i = 0; i < m_n; i++)
		{
			buf[i] = tmp[i];
		}
	}

	void GetData(uint8_t* buf, uint32_t& n) override
	{
		n = m_n;
		std::copy(m_data, m_data + m_n * 4, buf);
	}

	void Write() override
	{
		std::ofstream out(m_filename, std::ios::binary);
		unsigned int count = 0;
		std::cin >> count;
		char* buf = new char[(count + 1) * 4];
		GetBits(count, buf, 0);

		for (int i = 1; i <= count; i++)
		{
			float in = 0.0f;
			std::cin >> in;
			GetBits(in, buf, i * 4);
		}

		out.write(buf, (count + 1) * 4);
		delete[] buf;
	}
};

DataReader* Factory(const std::string& filename)
{
	std::string extension = filename.substr(filename.find_last_of('.') + 1);

	if (extension == "txt")
		return new TxtReader(filename);
	else if (extension == "bin")
		return new BinReader(filename);
	else if (extension == "binf")
		return new BinfReader(filename);
	return nullptr;
}


int main()
{
	float buf[100];
	uint32_t n;
	DataReader* Reader = Factory("input.binf");
	if (Reader == nullptr)
		return -1;
	//Reader->Write();
	Reader->Open();
	Reader->Read();
	Reader->GetData((uint8_t*)buf, n);
	for (int i = 0; i < n; i++)
		std::cout << buf[i] << " ";
	std::cout << std::endl;
	delete Reader;
	return 0;



	/*uint8_t n;
	uint8_t buf[100];

	DataReader* Reader = Factory("input1.txt");
	if (Reader == nullptr)
		return -1;
	Reader->Open();
	Reader->Read();
	Reader->GetData(buf, n);

	std::cout << (int)n << std::endl;
	for (int i = 0; i < n; i++)
		std::cout << (int)buf[i] << std::endl;

	delete Reader;*/


	
	/*std::ifstream in("input2.bin", std::ios::binary);
	uint8_t n;
	in.read((char*)&n, 1);

	uint8_t* buf = new uint8_t[n];
	in.read((char*)buf, n);

	std::cout << (int)n << std::endl;

	for (int i = 0; i < n; i++)
		std::cout << (int)buf[i] << std::endl;

	delete[] buf;*/

	/*//Создание бинарного файла
	std::ofstream out("input2.bin", std::ios::binary);
	uint8_t buf[6];
	buf[0] = 5;
	for (int i = 0; i < 5; i++)
	{
		buf[i+1] = i+127;
	}

	out.write((char*)buf, 6);*/
}

