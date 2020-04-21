﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Apex.Serialization.Tests
{
    public class InheritanceTests : AbstractSerializerTestBase
    {
        public static Type[] SerializableTypes()
        {
            return new[]
            {
                typeof(List<>)
            };
        }

        public class DeepBase
        {
            public int DeepValue = 5;
        }

        public class MiddleBase : DeepBase { }

        public class Base : MiddleBase
        {
            public Base(int v)
            {
                BaseValue = v;
            }
            private int BaseValue;

            public int BaseValueProp => BaseValue;
        }

        public class Derived : Base
        {
            public Derived(int v) : base(v)
            { }
            public int DerivedValue;
        }

        public class DerivedFromList : List<int>
        {
            public int Value;
        }

        [Fact]
        public void InheritedFields()
        {
            var x = new Derived(1) {DerivedValue = 3};

            RoundTrip(x);
        }

        public class A1
        {
            public int A = 1;
        }

        public class B1 : A1
        {
            public int B = 2;
        }

        public class B2 : A1
        {
            public int B = 3;
        }

        [Fact]
        public void InheritanceV()
        {
            var x = new { b1 = new B1(), b2 = new B2() };

            RoundTrip(x);
        }

        [Fact]
        public void InheritFromList()
        {
            var x = new DerivedFromList { Value = 5 };
            x.Add(2);
            x.Add(3);

            RoundTrip(x);

            var y = new List<int>();
            y.Add(2);
            y.Add(3);

            RoundTrip(y);
        }
    }
}
