using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathExtras
{
    /// <summary>
    /// A custom sigmoid function f(x) that smooth steps from roughly -1.0 to 1.0 over a domain of -1.0 to 1.0.
    /// Will only ever tend towards the amplitude but never reach it due to the asymptotic nature of sigmoid curves.
    /// </summary>
    /// <param name="value">The value at which the result of the function will be returned, aka the x in f(x).</param>
    /// <returns>The output of f(x).</returns>
    public static float Sigmoid(float value)
    {
        float d = 1.0F + (float)Math.Pow(Math.E, -6 * value);
        return (2.0F / d) - 1.0F;
    }

    /// <summary>
    /// Custom sigmoid function f(x) that smooth steps from roughly -amplitude to +amplitude over a domain of -spread to +spread.
    /// Will only ever tend towards the amplitude but never reach it due to the asymptotic nature of sigmoid curves.
    /// For a more intuitive understanding of this function go to https://www.desmos.com/calculator/hzmu9woodw and play with the parameters.
    /// </summary>
    /// <param name="value">The value at which the result of the function will be returned, aka the x in f(x).</param>
    /// <param name="amplitude">The value to which the function tends approaching infinity (or negative that value towards negative infinity).</param>
    /// <param name="spread">The rough domain of the function, where the output will be roughly equal for an input value equal to the spread.</param>
    /// <returns>The output of f(x).</returns>
    public static float Sigmoid(float value, float amplitude, float spread)
    {
        float d = 1.0F + (float)Math.Pow(Math.E, -6.0F / spread * value);
        return (2.0F * amplitude / d) - amplitude;
    }
}
