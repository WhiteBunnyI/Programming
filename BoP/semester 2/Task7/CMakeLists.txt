cmake_minimum_required(VERSION 3.15) # Проверка версии CMake.
									 # Если версия установленой программы
									 # старее указаной, произайдёт аварийный выход.

set(PROJECT_NAME inheritance) # задать значение PROJECT_NAME		 
project("${PROJECT_NAME}")           # установить имя проекта


set(CMAKE_CXX_STANDARD 17)			 # устанавливаем 17 стандарт языка
set(CMAKE_CXX_EXTENSIONS OFF)
set(CMAKE_CXX_STANDARD_REQUIRED TRUE)


# Сказать программе, что должен быть исполняемый файл
add_executable("${PROJECT_NAME}" main.cpp)

file(COPY "input.txt" DESTINATION "/")
file(COPY "input.bin" DESTINATION "/")
file(COPY "input.binf" DESTINATION "/")
