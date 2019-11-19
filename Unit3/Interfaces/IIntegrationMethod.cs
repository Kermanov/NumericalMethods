using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit3.Interfaces
{
    public interface IIntegrationMethod
    {
        double Calculate(Func<double, double> function, double a, double b, int n);
    }
}
