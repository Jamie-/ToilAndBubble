using UnityEngine;
using System.Collections;

public class IngredentSpriteMaker : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public const int EMPTY = 0;
    public const int Avoid = 1;
    public const int BACKGROUND = 2;
    public const int AvoidCOLORABLE = 3;
    public const int COLORABLE = 4;
    public const int SOLID = 5;
    public static Texture2D Create(int[,] seed, int scale, bool Flipx, bool flipy, Color c)
    {





        int[,] grid = new int[seed.GetLength(0), seed.GetLength(1)];
        for (int x = 0; x < seed.GetLength(0); x++)
            for (int y = 0; y < seed.GetLength(1); y++)
            {
                int cell = seed[x, y];
                switch (cell)
                {
                    case EMPTY:
                        grid[x, y] = emptyCell();
                        break;
                    case Avoid:
                        grid[x, y] = avoidCell();
                        break;
                    case BACKGROUND:
                        grid[x, y] = backgroundCell();
                        break;
                    case AvoidCOLORABLE:
                        grid[x, y] = AvoidColorableCell();
                        break;
                    case COLORABLE:
                        grid[x, y] = ColorableCell();
                        break;
                    case SOLID:
                        grid[x, y] = SolidCell();
                        break;

                }
            }
        if (Flipx)
        {
            for (int x = 0; x < grid.GetLength(0) / 2; x++)
                for (int y = 0; y < grid.GetLength(1); y++)
                    grid[grid.GetLength(0) - 1 - x, y] = grid[x, y];
        }
        if (Flipy)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
                for (int y = 0; y < grid.GetLength(1) / 2; y++)
                    grid[x, grid.GetLength(1) - 1 - y] = grid[x, y];
        }
        for (int x = 0; x < grid.GetLength(0); x++)
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                int here = grid[x, y];
                if (here != EMPTY) continue;
                bool needsolid = false;
                if ((y > 0) && (grid[x, y - 1] == AVOID)) needsolid = true;
                if ((y < cols - 1) && (grid[x, y + 1] == AVOID)) needsolid = true;
                if ((x > 0) && (grid[x - 1, y] == AVOID)) needsolid = true;
                if ((x < rows - 1) && (grid[x + 1, y] == AVOID)) needsolid = true;
                if (needsolid)
                    grid[x, y] = SOLID;
            }
    }

 static Texture2D Draw(int[,] gridtorender, int scale, Color c) { 
        var texture = new Texture2D(seed.GetLength(0) * scale, seed.GetLength(1) * scale, TextureFormat.ARGB32, false);
        for (int x = 0; x < gridtorender.GetLength(0); x++)
            for (int y = 0; y < gridtorender.GetLength(1); y++)
            {
                switch (gridtorender[x, y])
                {
                    case EMPTY:
                        DrawRect(x, y, scale, scale, Color.clear);
                        break;
                    case BACKGROUND:
                        DrawRect(x, y, scale, scale, Color.white);
                        break;
                    case COLORABLE:
                        DrawRect(x, y, scale, scale, Color.c);
                        break;
                    case SOLID:
                        DrawRect(x, y, scale, scale, Color.black);
                        break;
                }
               
            }
        






                // Apply all SetPixel calls
                texture.Apply();

        // connect texture to material of GameObject this script is attached to
        return texture;
    }
    int emptyCell()
    {
        return EMPTY;
    }
    int avoidCell()
    {
        double val = Random.value;
        if (val < 0.4)
        {
            return EMPTY;
        }
        else if (val < 0.6)
        {
            return BACKGROUND;
        }
        return SOLID;
        
    }
    int backgroundCell()
    {
        return BACKGROUND;
    }
    int AvoidColorableCell()
    {
        double val = Random.value;
        if (val < 0.2)
        {
            return EMPTY;
        }
        else if (val < 0.4)
        {
            return BACKGROUND;
        }else if (val < 0.8)
        {
            return COLORABLE;
        }
        return SOLID;

    }
    int ColorableCell()
    {
        return COLORABLE;
    }
    int SolidCell()
    {
        return 5;
    }
    public static void DrawRect(int x, int y, int h, int w, Color c)
    {
        for (int i = x; i < w; i++)
            for (int j = y; j < h; j++)
            {
                texture.SetPixel(i, j, c);
            }
    }

}
