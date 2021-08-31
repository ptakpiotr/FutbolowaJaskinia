using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FutbolowaJaskinia.Utilities
{
    public class ValidUrlListAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as List<string>;
            return list.All(elem => elem.Contains("unsplash"));
        }
    }
}
