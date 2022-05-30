
using System;
using System.Collections.Generic;

namespace HistSi.ValueSources
{
    public sealed class ArrayMathOperation_Int32 : ArrayMathOperation<int>
    {
        protected sealed override IList<Func<IGetterValue<int>[], int>> Operations =>
            MathOperations.ArrayInt32.Operations;
    }
}
