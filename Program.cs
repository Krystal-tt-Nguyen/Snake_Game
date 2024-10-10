// SNAKE GAME

Random numberGenerator = new Random();

List<Coordinates> foodPositions = new List<Coordinates>();
Coordinates foodCoordinates = new Coordinates();
Coordinates currentFood;

Queue<Coordinates> snakeBody = new Queue<Coordinates>();
Coordinates newSnakePosition = new Coordinates();

int width = 25;
int height = 15;
int maxAmountFood = 100;

int currentSnakePosition_X = width / 2;
int currentSnakePosition_Y = height / 2;

bool playAgain = true;
bool ateFood = false;


DrawBox(width, height);
AddRandomPositionToFood(width, height, maxAmountFood);
InitializeGame();

while (playAgain)
{
    PrintFood(foodPositions);
    MoveSnake(ref currentSnakePosition_X, ref currentSnakePosition_Y, width, height);
    ateFood = HasSnakeEatenFood(newSnakePosition, currentFood);
    UpdateSnakeBody(ateFood, newSnakePosition);
    PrintSnakeBody();
}

if (!playAgain)
{
    Console.Clear();
    Console.WriteLine("The game is finished...");
}

// ____________________________________________________________________________

void DrawBox(int width, int height)
{
    for (int j = 0; j < height; j++)
    {
        for (int i = 0; i < width; i++)
        {
            if (j == 0 || j == height - 1 || i == 0 || i == width - 1)
            {
                Console.Write('#');
            }
            else
            {
                Console.Write(' ');
            }
        }
        Console.WriteLine();
    }

    Console.WriteLine();
    Console.WriteLine("Press Enter when the snake \'@\' has eaten all fruits.");
}

void AddRandomPositionToFood(int width, int height, int maxAmountFood)
{
    for (int i = 0; i < maxAmountFood; i++)
    {
        int randomFoodCoordinate_X = numberGenerator.Next(1, (width - 1));
        int randomFoodCoordinate_Y = numberGenerator.Next(1, (height - 1));

        foodCoordinates = new Coordinates() { coordinate_X = randomFoodCoordinate_X, coordinate_Y = randomFoodCoordinate_Y };
        foodPositions.Add(foodCoordinates);
    }
}

void PrintFood(List<Coordinates> foodPositions)
{
    Console.ForegroundColor = ConsoleColor.Red;

    currentFood = foodPositions[0];

    Console.SetCursorPosition(currentFood.coordinate_X, currentFood.coordinate_Y);
    Console.Write("¤");
    maxAmountFood--;

    Console.ResetColor();
}

void InitializeGame()
{
    // Lägg till ormens initiala position i kön. 
    snakeBody.Enqueue(new Coordinates() { coordinate_X = currentSnakePosition_X, coordinate_Y = currentSnakePosition_Y });

    Console.SetCursorPosition(currentSnakePosition_X, currentSnakePosition_Y);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine('@');
    Console.ResetColor();
}

void MoveSnake(ref int currentSnakePosition_X, ref int currentSnakePosition_Y, int maxWidth, int maxHeight)
{
    Console.CursorVisible = false;
    ConsoleKey keyInfo = Console.ReadKey(true).Key;

    if (keyInfo == ConsoleKey.UpArrow && currentSnakePosition_Y > 1)
    {
        currentSnakePosition_Y--;
    }
    else if (keyInfo == ConsoleKey.DownArrow && currentSnakePosition_Y < maxHeight - 2)
    {
        currentSnakePosition_Y++;
    }
    else if (keyInfo == ConsoleKey.LeftArrow && currentSnakePosition_X > 1)
    {
        currentSnakePosition_X--;
    }
    else if (keyInfo == ConsoleKey.RightArrow && currentSnakePosition_X < maxWidth - 2)
    {
        currentSnakePosition_X++;
    }

    if (keyInfo == ConsoleKey.Enter)
    {
        playAgain = false;
    }

    newSnakePosition = new Coordinates() { coordinate_X = currentSnakePosition_X, coordinate_Y = currentSnakePosition_Y };
}

bool HasSnakeEatenFood(Coordinates newSnakePosition, Coordinates currentFood)
{
    if (newSnakePosition.coordinate_X == currentFood.coordinate_X && newSnakePosition.coordinate_Y == currentFood.coordinate_Y)
    {
        Console.Beep(750,75);
        foodPositions.Remove(currentFood);
        return true;
    }

    return false;
}

void UpdateSnakeBody(bool ateFood, Coordinates newSnakePosition)
{
    snakeBody.Enqueue(newSnakePosition);

    if (!ateFood)
    {
        // Hitta den äldsta positionen i listan, spara koordinaterna och ta bort den
        Coordinates oldSnakePosition = snakeBody.Dequeue();

        Console.SetCursorPosition(oldSnakePosition.coordinate_X, oldSnakePosition.coordinate_Y);
        Console.Write(' ');
    }
}

void PrintSnakeBody()
{
    foreach (var position in snakeBody)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(position.coordinate_X, position.coordinate_Y);
        Console.Write("@");
        Console.ResetColor();
    }
}

struct Coordinates()
{
    public int coordinate_X;
    public int coordinate_Y;
}

