//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Perception
//{
//    class Grid
//    {
//        public List<string> row_1;
//        public List<string> row_2;
//        public List<string> row_3;
//        public List<string> row_4;
//        public List<string> row_5;
//        public string[] directions;

//        public Grid()
//        {
//            directions = new string[] { "left", "right", "up", "down" };
//        }

//        public List<List<string>> createGrid()
//        {
//            var pos_x = 0;
//            var pos_y = 0;
//            var positions = new List<Vector2>();
//            var moves = new List<string>();
            
//            var rng = new Random();
//            bool canMove;
//            var new_x = 0;
//            var new_y = 0;

//            while((pos_x < 4) || (pos_y < 4))
//            {
//                var direction = directions[rng.Next()];
//                canMove = true;
//                switch (direction)
//                {
//                    case "up":
//                        if (pos_x <= 0)
//                        {

//                        }
//                        break;
//                    case "left":
//                        if (pos_y <= 0)
//                        {

//                        }
//                        break;
//                    case "down":
//                        if (pos_x >= 4)
//                        {

//                        }
//                        break;
//                    case "right":
//                        if (pos_y >= 4)
//                        {

//                        }
//                        break;
//                    default:
//                        break;
//                }
//            }

//            var row_1 = new List<string>();
//            var row_2 = new List<string>();
//            var row_3 = new List<string>();
//            var row_4 = new List<string>();
//            var row_5 = new List<string>();

//            var grid = new List<List<string>>();
//            grid.Add(row_1);
//            grid.Add(row_2);
//            grid.Add(row_3);
//            grid.Add(row_4);
//            grid.Add(row_5);
//            return grid;
//        }
//    }
//}
