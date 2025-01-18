#include <BigInt.hpp>

int main()
{
	BigInt num1("10");
	BigInt num2("-10");
	BigInt sum("0");
	BigInt mul("-100");
	std::cout << num1 << std::endl << num2 << std::endl;
	//std::cout << (num1 < num2) << std::endl;
	//std::cout << (num1 > num2) << std::endl;

	std::cout << (num1 + num2) << std::endl;
	std::cout << "Is the sum correct? " << " ";
	std::cout << (((num1 + num2) == sum) ? "Correct" : "Wrong") << std::endl;


	std::cout << (num1 * num2) << std::endl;
	std::cout << "Is the multiplication correct? " << " ";
	std::cout << (((num1 * num2) == mul) ? "Correct" : "Wrong") << std::endl;

	return 0;
}