using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CaseBreaker
{
    [DataContract]
    class SaveEditor
    {
        public void ConvertToWrite(int[,] theMap)
        {
            map = new int[theMap.GetLength(0)][];
            for (int row = 0; row < theMap.GetLength(0); row++)
            {
                map[row] = new int[theMap.GetLength(1)];
                for (int colunm = 0; colunm < theMap.GetLength(1); colunm++)
                {
                    map[row][colunm] = theMap[row, colunm];
                }
            }
        }

        public void ConvertToRead(ref int[,] theMap)
        {
            for (int row = 0; row < theMap.GetLength(0); row++)
            {
                for (int colunm = 0; colunm < theMap.GetLength(1); colunm++)
                {
                    if (map[row][colunm] != null)
                    {
                        theMap[row, colunm] = map[row][colunm];
                    }
                    else
                    {
                        theMap[row, colunm] = 0;
                    }
                }
            }
        }

        [DataMember]
        public int[][] map;
    }
}
