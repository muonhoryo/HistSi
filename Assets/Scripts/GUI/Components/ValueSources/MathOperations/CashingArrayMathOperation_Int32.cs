
using System;
using System.Collections.Generic;

namespace HistSi.ValueSources
{
    public sealed class CashingArrayMathOperation_Int32 : CashingArrayMathOperation<int>
    {
        protected sealed override IList<Func<IGetterValue<int>[], int>> Operations =>
            MathOperations.ArrayInt32.Operations;
    }
}
