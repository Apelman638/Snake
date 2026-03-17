import random 
from inputimeout import inputimeout, TimeoutOccurred

width = 20
height = 20

class Cords:
    def __init__(self, x: int, y: int):
        self.x = x
        self.y = y

class Snake:
    snakeParts: list[Cords] = []
    score = len(snakeParts)
    def __init__(self):
        self.snakeParts.append(Cords(10,10))
    def moveY(self, dir):
        # end game if it touches itself 
        self.snakeParts.insert(0, Cords(self.snakeParts[0].x, self.snakeParts[0].y - dir))
        print(f"{self.snakeParts[0].x} {self.snakeParts[0].y}")
        if (self.snakeParts[0].x >= width or self.snakeParts[0].x < 0) or (self.snakeParts[0].y >= height or self.snakeParts[0].y < 0): 
            # if out of bounds, game should end
            print("GAME OVER\nSnake out of bounds")
            exit()

        if (self.snakeParts[0].y in (yv.y for yv in self.snakeParts[1:])): 
            # checks if snake is touching itself by comaring the head value to the read of the values 
            print("GAME OVER\nSnake cannot touch itself") # seems to glitch and counts being next to as an end
            exit()
        if self.snakeParts[0].x == location.x and self.snakeParts[0].y == location.y:
            screen.newApple() # if its touching the apple is creates a new apple
        else: 
            self.snakeParts.pop() # if it isnt it removes the last part to create new motion
            # should remove the last element to make it appear like its moving forward
    def moveX(self, dir):
        self.snakeParts.insert(0, Cords(self.snakeParts[0].x + dir, self.snakeParts[0].y))
        print(f"{self.snakeParts[0].x} {self.snakeParts[0].y}")
        if (self.snakeParts[0].x >= width or self.snakeParts[0].x < 0) or (self.snakeParts[0].y >= height or self.snakeParts[0].y < 0):
            print("GAME OVER\nSnake out of bounds")
            exit()

        if (self.snakeParts[0].x in (xv.x for xv in self.snakeParts[1:])): 
            # checks if snake is touching itself by comaring the head value to the read of the values 
            print("GAME OVER\nSnake cannot touch itself") # seems to glitch and counts being next to as an end
            exit()
        if self.snakeParts[0].x == location.x and self.snakeParts[0].y == location.y:
            screen.newApple() # if its touching the apple is creates a new apple
        else: 
            self.snakeParts.pop() # if it isnt it removes the last part to create new motion
            # should remove the last element to make it appear like its moving forward
 
class Screen:
    screenElements: list = []
    def __init__(self):
        for i in range(width*height):
            self.screenElements.append(' ')

    def newApple(self): # make into a class itself maybe
        spot = random.randint(0,width*height)
        global location
        location = Cords(spot%width, spot//height)
        # save location 

        if self.screenElements[spot] == ' ': # to ensure it spawns on empty space
            self.screenElements[spot] = "\uf8ff"
        else:
            self.newApple()

    def clearScreen(self):
        for i in range(len(self.screenElements)):
            if self.screenElements[i] == "\uf8ff":
                self.screenElements[i] = "\uf8ff"
            else:
                self.screenElements[i] = ' '

    def updScreen(self, snake: Snake):
        for part in snake.snakeParts: 
            self.screenElements[part.y*width + part.x] = "\u25A0"

    def printScreen(self): 
        i = 1
        for element in self.screenElements:
            print(element, end=' ')
            if i % width == 0:
                print(end="\n")
            i+=1

def init():
    global screen
    global userSnake
    screen = Screen()
    userSnake = Snake()
    screen.newApple()
    screen.updScreen(userSnake)
    screen.printScreen()

def update():
    screen.clearScreen()
    screen.updScreen(userSnake)
    screen.printScreen()

init()

last_dir = 'w'
while(True):
    try: 
        dir = inputimeout(prompt="wasd: ", timeout=1)
    except TimeoutOccurred:
        dir = last_dir
    match dir:
        case 'w': 
            userSnake.moveY(1) 
        case 's':
            userSnake.moveY(-1) 
        case 'a':
            userSnake.moveX(-1)
        case 'd':
            userSnake.moveX(1)
        case _:
            print("input not found")
    last_dir = dir
    update()