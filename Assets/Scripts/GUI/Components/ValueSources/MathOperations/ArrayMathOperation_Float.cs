
using System;
using System.Collections.Generic;

namespace HistSi.ValueSources
{
    public sealed class ArrayMathOperation_Float : ArrayMathOperation<float>
    {
        protected sealed override IList<Func<IGetterValue<float>[], float>> Operations =>
            MathOperations.ArrayFloat.Operations;
    }
}
