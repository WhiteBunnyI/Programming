
////////////////////////////////////////////////////////////
// Headers
////////////////////////////////////////////////////////////
#include <SFML/Graphics.hpp>
#include <SFML/Audio.hpp>

#include <PhysicalObject.hpp>
#include <iostream>
#define DEBUG1

int main()
{
    //Constants
    const float G = 10.f * 3;
    const float GAMEWIDTH = 800;
    const float GAMEHEIGHT = 800;
    const float AIRRESIST = .00001f;
    const float FLOOR = 780;
    const float DAMPING = 0.9f;
    const float SPEED = .1f;
    
    //Objects
    CircleObject ball_f;
    float radius = 30;
    ball_f.shape.setRadius(radius);
    ball_f.shape.setPosition(sf::Vector2f(200, 80 + 50));
    ball_f.shape.setOrigin(radius, radius);


    CircleObject ball_s;
    radius = 50;
    ball_s.shape.setRadius(50);
    ball_s.shape.setPosition(sf::Vector2f(400, 80 + 30));
    ball_s.shape.setOrigin(radius, radius);

    CircleObject ball_t;
    radius = 80;
    ball_t.shape.setRadius(80);
    ball_t.shape.setPosition(sf::Vector2f(600, 80));
    ball_t.shape.setOrigin(radius, radius);

    CircleObject objs[3];
    objs[0] = ball_f;
    objs[1] = ball_s;
    objs[2] = ball_t;

    sf::RectangleShape floor;
    floor.setSize(sf::Vector2f(GAMEWIDTH, 40));
    floor.setPosition(sf::Vector2f(0, FLOOR));
    floor.setFillColor(sf::Color(156, 156, 156));

    //Create the window of the app
    sf::RenderWindow window(sf::VideoMode(GAMEWIDTH, GAMEHEIGHT), "Sfml physics", sf::Style::Titlebar | sf::Style::Close);
    sf::Clock clock;
    bool resizing = false;
    while (window.isOpen())
    {
        sf::Event event;
        while (window.pollEvent(event))
        {
            if (event.type == sf::Event::Closed || event.key.code == sf::Keyboard::Escape)
            {
                window.close();
                break;
            }
            if (event.type == sf::Event::Resized)
            {
                resizing = true;
            }
        }
        if (resizing)
            clock.restart();
        resizing = false;

        float deltaTime = clock.restart().asSeconds();
        //Physics
        for (int i = 0; i < std::size(objs); i++)
        {
            objs[i].vy += (G + objs[i].getAirResistanceY(AIRRESIST)) * deltaTime;
            if (objs[i].shape.getPosition().y >= FLOOR - objs[i].shape.getRadius())
            {
                objs[i].shape.setPosition(objs[i].shape.getPosition() - sf::Vector2f(0, .1f));
                objs[i].vy = -objs[i].vy * DAMPING;
                if (abs(objs[i].vy) <= .61f)
                {
                    objs[i].vy = 0;
                }
            }

            objs[i].shape.move(sf::Vector2f(objs[i].vx, objs[i].vy) * SPEED);
#ifdef DEBUG
            if (i == 0)
            {
                std::cout << objs[i].vy << " pos: " << objs[i].shape.getPosition().y << " accel: " << (G + objs[i].getAirResistanceY(AIRRESIST)) << std::endl;
            }
#endif
        }
        //Render
        window.clear(sf::Color(50, 50, 50));
        for (int i = 0; i < std::size(objs); i++)
        {
            window.draw(objs[i].shape);
        }
        window.draw(floor);
        window.display();
    }
    
}
