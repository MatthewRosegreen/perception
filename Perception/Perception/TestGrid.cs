using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perception
{
    class TestGrid
    {
        public string[] grid;
        public string[] directions;

        public TestGrid()
        {
            grid = new string[] { "down", "none", "right", "right", "down", "right", "right", "up", "down", "left", "down", "left", "down", "left", "none", "down", "up", "left", "right", "down", "right", "right", "right", "up", "none" };
            directions = new string[] { "up", "down", "left", "right" };
        }

        public void ResetGrid()
        {
            var rng = new Random();

            //create route
            var pos_x = 0;
            var pos_y = 0;
            var tried_left = false;
            var tried_right = false;
            var tried_up = false;
            var tried_down = false;

            var usedBlocks = new List<Vector2>();
            var new_grid = new string[25];

            while (pos_x < 4 || pos_y < 4)
            {
                if(tried_up && tried_right && tried_left && tried_down)
                {
                    usedBlocks.Clear();
                    pos_x = 0;
                    pos_y = 0;
                    new_grid = new string[25];
                }
                var nextRdmNm = rng.Next(4);
                var nextStep = directions[nextRdmNm];
                var routeApproved = true;

                Vector2 potentialPos;
                switch (nextStep)
                {
                    case "up":
                        potentialPos = new Vector2(pos_x, pos_y - 1);
                        break;
                    case "down":
                        potentialPos = new Vector2(pos_x, pos_y + 1);
                        break;
                    case "left":
                        potentialPos = new Vector2(pos_x - 1, pos_y);
                        break;
                    case "right":
                        potentialPos = new Vector2(pos_x + 1, pos_y);
                        break;
                    default:
                        potentialPos = new Vector2(pos_x, pos_y);
                        break;
                }
                if(potentialPos.X >= 5 || potentialPos.X <= -1 || potentialPos.Y >= 5 || potentialPos.Y <= -1)
                {
                    routeApproved = false;
                }
                for(int i = 0; i < usedBlocks.Count; i++)
                {
                    if(usedBlocks[i] == potentialPos)
                    {
                        routeApproved = false;
                    }
                }
                
                if (routeApproved)
                {
                    usedBlocks.Add(new Vector2(pos_x, pos_y));
                    new_grid[pos_x + pos_y * 5] = nextStep;
                    pos_x = (int)potentialPos.X;
                    pos_y = (int)potentialPos.Y;
                    tried_down = false;
                    tried_left = false;
                    tried_right = false;
                    tried_up = false;                    
                }
                else
                {
                    switch (nextStep)
                    {
                        case "up":
                            tried_up = true;
                            break;
                        case "down":
                            tried_down = true;
                            break;
                        case "left":
                            tried_left = true;
                            break;
                        case "right":
                            tried_right = true;
                            break;
                        default:
                            break;
                    }
                }
            }

            pos_x = 0;
            pos_y = 0;
            for (int i = 0; i < new_grid.Length; i++)
            {
                if(new_grid[i] == null)
                {
                    //new_grid[i] = "none";
                    new_grid[i] = directions[rng.Next(4)];
                }
                
            }
            new_grid.CopyTo(grid, 0);
        }
    }
}
