
using System;
using System.Collections.ObjectModel;
using HistSi.ValueSources;

namespace HistSi
{
    public static class MathOperations
    {
        public enum ArrayMathOperationType
        {
            Sum,
            Min,
            Max
        }
        public enum PairMathOperationType
        {
            Add,
            Subtract,
            Multiply,
            Divide,
            Remainder,
            Pow,
            Root,
            RoundUpTo,
            RoundDownTo
        }
        public static class PairFloat
        {
            public readonly static ReadOnlyCollection<Func<float, float, float>> Operations =
                new ReadOnlyCollection<Func<float, float, float>>
                (new Func<float, float, float>[]
                {
                ( x, y)=>x+y,
                (x, y)=>x-y,
                (x,y)=>x*y,
                (x,y)=>x/y,
                (x,y)=>x%y,
                (x,y)=>(float)Math.Pow(x,y),
                (x,y)=>(float)Math.Pow(x,1/y),
                delegate(float x,float y)
                {
                    if (x % y == 0)
                    {
                        return x;
                    }
                    else
                    {
                        return x+y-x%y;
                    }
                },
                delegate(float x,float y)
                {
                    if (x % y == 0)
                    {
                        return x;
                    }
                    else
                    {
                        return x-x%y;
                    }
                }
                });
        }
        public static class PairInt32
        {
            public readonly static ReadOnlyCollection<Func<int, int, int>> Operations =
                new ReadOnlyCollection<Func<int, int, int>>
                (new Func<int, int, int>[]
                {
            ( x, y)=>x+y,
            (x, y)=>x-y,
            (x,y)=>x*y,
            (x,y)=>x/y,
            (x,y)=>x%y,
            (x,y)=>(int)Math.Pow(x,y),
            (x,y)=>(int)Math.Pow(x,1/y),
            delegate(int x,int y)
            {
                if (x % y == 0)
                {
                    return x;
                }
                else
                {
                    return x+y-x%y;
                }
            },
            delegate(int x,int y)
            {
                if (x % y == 0)
                {
                    return x;
                }
                else
                {
                    return x-x%y;
                }
            }
                });
        }
        public static class ArrayInt32
        {
            public readonly static ReadOnlyCollection<Func<IGetterValue<int>[], int>> Operations =
                new ReadOnlyCollection<Func<IGetterValue<int>[], int>>
                (new Func<IGetterValue<int>[], int>[]
                {
                    Sum,
                    Min,
                    Max
                });
            private static int Sum(IGetterValue<int>[] input)
            {
                int sum = 0;
                foreach (var value in input)
                {
                    sum += value.Value;
                }
                return sum;
            }
            private static int Max(IGetterValue<int>[] input)
            {
                int max = int.MinValue;
                foreach (var item in input)
                {
                    int value = item.Value;
                    if (value > max)
                    {
                        max = value;
                    }
                }
                return max;
            }
            private static int Min(IGetterValue<int>[] input)
            {
                int min = int.MaxValue;
                foreach (var item in input)
                {
                    int value = item.Value;
                    if (min > value)
                    {
                        min = value;
                    }
                }
                return min;
            }
        }
        public static class ArrayFloat
        {
            public readonly static ReadOnlyCollection<Func<IGetterValue<float>[], float>> Operations =
                new ReadOnlyCollection<Func<IGetterValue<float>[], float>>
                (new Func<IGetterValue<float>[], float>[]
                {
                    Sum,
                    Min,
                    Max
                });
            private static float Sum(IGetterValue<float>[] input)
            {
                float sum = 0;
                foreach (var value in input)
                {
                    sum += value.Value;
                }
                return sum;
            }
            private static float Max(IGetterValue<float>[] input)
            {
                float max = float.MinValue;
                foreach (var item in input)
                {
                    float value = item.Value;
                    if (value > max)
                    {
                        max = value;
                    }
                }
                return max;
            }
            private static float Min(IGetterValue<float>[] input)
            {
                float min = float.MaxValue;
                foreach (var item in input)
                {
                    float value = item.Value;
                    if (min > value)
                    {
                        min = value;
                    }
                }
                return min;
            }
        }
    }
}
