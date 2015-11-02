﻿
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWSSDK.Tests.Framework
{
    public class Utils
    {
        public static void AssertExceptionExpected(Action action)
        {
            AssertExceptionExpected(action, typeof(Exception));
        }

        public static T AssertExceptionExpected<T>(Action action) where T : Exception
        {
            return AssertExceptionExpected(action, typeof(T)) as T;
        }

        public static Exception AssertExceptionExpected(Action action, Type expectedExceptionType, string expectedExceptionMessage = null)
        {
            try
            {
                action();
                if (expectedExceptionType != null)
                {
                    Assert.Fail("Exception of type " + expectedExceptionType.FullName + " expected but not thrown!");
                }
                Console.WriteLine("Success, no exception expected or thrown");
                return null;
            }
            catch (Exception e)
            {
                if (expectedExceptionType == null)
                {
                    Assert.Fail("No exception expected, but exception thrown: " + e.ToString());
                }

                Type eType = e.GetType();
                if (!expectedExceptionType.IsAssignableFrom(eType))
                {
                    Assert.Fail("Expected exception of type " + expectedExceptionType.FullName + ", but thrown exception is of type " + eType.FullName + " : " + e.Message);
                }
                else if (
                    !string.IsNullOrEmpty(expectedExceptionMessage) &&
                    !string.Equals(expectedExceptionMessage, e.Message, StringComparison.OrdinalIgnoreCase))
                {
                    Assert.Fail("Expected exception message of [" + expectedExceptionType.FullName + "], but thrown exception has message of [" + e.Message + "]");
                }

                Console.WriteLine("Success, expected " + expectedExceptionType.FullName + ", thrown " + eType.FullName + ": " + e.Message);
                return e;
            }
        }
    }
}
