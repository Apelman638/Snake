#include <iostream>
#include <vector>
#define WIDTH 20
#define HEIGHT 20
using namespace std;

struct Cord {
    int x;
    int y;
};

class Snake {
    public: 
        static vector<Cord> segs;
        static int length;
        Snake() { // constructor
            segs.at(0) = Cord{10,10}; 
        }

        void movex() {
            int *head = &segs.at(0).x;
            *head += 1;
        }
        void movey() {
            int *head = &segs.at(0).y;
            *head += 1;
        }
};

class Screen {
    public:
        static vector<string> pixels;
        Screen() {
            for(int i = 0; i < WIDTH*HEIGHT; i++) {
                pixels.push_back(" ");
            }
        }

        void new_apple() {

        }

        void update_screen(Snake &snake) {
            for(Cord part : snake.segs){
                Screen::pixels[part.y * WIDTH + part.x]= "."; 
            }
        }
};