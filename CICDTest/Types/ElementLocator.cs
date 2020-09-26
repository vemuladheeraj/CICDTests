using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICDTest.Types
{
    public class ElementLocator
    {

        public ElementLocator(Locator kind, string value)
        {
            this.Kind = kind;
            this.Value = value;
        }


        public Locator Kind { get; set; }


        public string Value { get; set; }


        public ElementLocator Format(params object[] parameters)
        {
            return new ElementLocator(this.Kind, string.Format(CultureInfo.CurrentCulture, this.Value, parameters));
        }
    }
}
