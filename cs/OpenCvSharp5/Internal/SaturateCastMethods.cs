using System.Numerics;

namespace OpenCvSharp5.Internal;

internal static class SaturateCastMethods
{
    public static TOut SaturateCastFromFloat<TIn, TOut>(TIn v)
        where TIn : unmanaged, IBinaryFloatingPointIeee754<TIn>
        where TOut : unmanaged, IBinaryInteger<TOut>
    {
        var d = double.CreateSaturating(v);
        var rounded = double.Round(d, 0, MidpointRounding.AwayFromZero);
        return TOut.CreateSaturating(rounded);
    }

    public static TOut SaturateCastFromInteger<TIn, TOut>(TIn v)
        where TIn : unmanaged, IBinaryInteger<TIn>
        where TOut : unmanaged, IBinaryInteger<TOut> =>
        TOut.CreateSaturating(v);

    public static TOut SaturateCast<TIn, TOut>(TIn v)
        where TIn : unmanaged, IBinaryNumber<TIn>
        where TOut : unmanaged, IBinaryNumber<TOut>
    {
        if (typeof(TIn).IsAssignableTo(typeof(IBinaryFloatingPointIeee754<>)))
        {
            if (typeof(TOut).IsAssignableTo(typeof(IBinaryInteger<>)))
            {
                var d = double.CreateSaturating(v);
                var rounded = double.Round(d, 0, MidpointRounding.AwayFromZero);
                return TOut.CreateSaturating(rounded);
            }

            return TOut.CreateSaturating(v);
        }
        if (typeof(TIn).IsAssignableTo(typeof(IBinaryInteger<>)))
        {
            return TOut.CreateSaturating(v);
        }

        throw new ArgumentException($"The type argument {typeof(TIn).Name} is not supported.");
    }
    
    public static TOut FloatToIntWithRounding<TInFloat, TOut>(TInFloat v)
        where TInFloat : unmanaged, IBinaryFloatingPointIeee754<TInFloat>
        where TOut : unmanaged, IBinaryInteger<TOut>
    {
        var rounded = TInFloat.Round(v,0, MidpointRounding.AwayFromZero); 
        return TOut.CreateSaturating(rounded);
    }
}
