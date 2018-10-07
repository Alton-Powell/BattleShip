using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Square
    {
        public string ColumnRow { get; set; }
        public bool IsHit { get; set; }

        public override bool Equals(object obj)
        {
            var squareBeingCompared = obj as Square;

            return ColumnRow.Equals(squareBeingCompared.ColumnRow, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return ColumnRow.GetHashCode();
        }
    }
}
