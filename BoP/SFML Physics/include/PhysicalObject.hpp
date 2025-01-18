#pragma once
#define _USE_MATH_DEFINES
#include <SFML/Graphics.hpp>
#include <math.h>

struct CircleObject
{
	sf::CircleShape shape;
	sf::Vector2f center;
	float vx = 0, vy = 0;

	float getAirResistanceX(float airResistanceCoef)
	{
		return -airResistanceCoef * vx * abs(vx) * getCrossSectionalArea() / 2.f;
	}
	float getAirResistanceY(float airResistanceCoef)
	{
		return -airResistanceCoef * vy * abs(vy) * getCrossSectionalArea() / 2.f;
	}
	float getCrossSectionalArea()
	{
		float radius = shape.getRadius();
		return M_PI * radius * radius;
	}
	float getSpeed()
	{
		return std::sqrt(vx * vx + vy * vy);
	}

};