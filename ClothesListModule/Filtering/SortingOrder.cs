using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesListModule.Filtering
{
    public class SortingOrder
    {
        public string Name { get; set; }
        public bool Direction { get; set; }

        public static List<SortingOrder> GenerateStandardList()
        {
            return new List<SortingOrder>()
            {
                new SortingOrder()
                {
                    Name = "Rosnąco",
                    Direction = true
                },

                new SortingOrder()
                {
                    Name = "Malejąco",
                    Direction = false
                }
            };
        }
    }
}
