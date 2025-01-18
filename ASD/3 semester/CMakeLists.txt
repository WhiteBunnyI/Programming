cmake_minimum_required(VERSION 3.15) # Проверка версии CMake.
									 # Если версия установленой программы
									 # старее указаной, произайдёт аварийный выход.

set(PROJECT_NAME ASD) # задать значение PROJECT_NAME		 
project("${PROJECT_NAME}")           # установить имя проекта

file(COPY "sorting.txt" DESTINATION "/")
file(COPY "words.txt" DESTINATION "/")

set(CMAKE_CXX_STANDARD 17)			 # устанавливаем 17 стандарт языка
set(CMAKE_CXX_EXTENSIONS OFF)
set(CMAKE_CXX_STANDARD_REQUIRED TRUE)
include_directories("include/")		#Добавляем хедеры

# Сказать программе, что должен быть исполняемый файл
add_executable("${PROJECT_NAME}" main.cpp
								include/includes.hpp
								include/stack.hpp 
								include/labs.hpp
								include/queue.hpp
								include/_lab1.hpp
								include/_lab2.hpp
								include/_lab3.hpp
								include/_lab4.hpp
								include/_lab5.hpp
								include/_lab6.hpp
								include/_lab7.hpp
								include/_lab8.hpp
								include/_lab9.hpp
								include/lab10.hpp
								include/lab11.hpp
								include/lab12.hpp
								include/lab13.hpp
								include/lab14.hpp
								include/lab15.hpp
								include/lab16.hpp
								include/lab17.hpp)

#Устанавливаем в качестве запускаемого проекта наш проект
set_property(DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}" PROPERTY VS_STARTUP_PROJECT "${PROJECT_NAME}")