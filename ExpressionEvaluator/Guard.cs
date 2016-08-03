﻿using System;

namespace ExpressionEvaluator
{
    static class Guard
    {
        public static void NotNull<T>(T value, string paramName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
