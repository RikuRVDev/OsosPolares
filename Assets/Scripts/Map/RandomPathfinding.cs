using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class RandomPathfinding
{
    private static int DIRECTION_DOWN = 1;
    private static int DIRECTION_LEFT = 2;
    private static int DIRECTION_RIGHT = 4;
    private static int[] values = new int[3] { DIRECTION_DOWN,DIRECTION_LEFT,DIRECTION_RIGHT };

    public static int GetNewDirection(int allowed) {
        int newDirection;
        do {
            int randomNumber = Random.Range(0,values.Length);
            newDirection = values[randomNumber];
        } while ((newDirection & allowed) == 0);
        return newDirection;
    }

    public static List<int> GenerateRandomPath(int startX, int startY, int endX, int endY, double probability) {
        List<int> newPath = new List<int>();

        int currentX = startX;
        int currentY = startY;
        int currentDirection = DIRECTION_RIGHT;
        int newDirection = currentDirection;

        while(!(currentX == endX && currentY == endY)) {
            if (NextDouble() <= probability)
            {
                // Generate a turn
                do
                {
                    if(currentX == endX)
                    {
                        newDirection = GetNewDirection(DIRECTION_LEFT | DIRECTION_DOWN);
                    }
                    else if(currentY == endY)
                    {
                        newDirection = DIRECTION_RIGHT;
                    }
                    else if(currentX <= 0)
                    {
                        newDirection = GetNewDirection(DIRECTION_RIGHT | DIRECTION_DOWN);
                    }
                    else
                    {
                        newDirection = GetNewDirection(DIRECTION_RIGHT | DIRECTION_DOWN | DIRECTION_LEFT);
                    }
                } while((newDirection | currentDirection) == (DIRECTION_LEFT | DIRECTION_RIGHT));

                newPath.Add(newDirection);
                currentDirection = newDirection;
                switch(newDirection)
                {
                    case 2:
                        currentX--;
                        break;
                    case 4:
                        currentX++;
                        break;
                    case 1:
                        currentY++;
                        break;
                }
            }
        }

        return newPath;
    }

    private static double NextDouble() {
        return Random.Range(1,100) / 100;
    }
}
