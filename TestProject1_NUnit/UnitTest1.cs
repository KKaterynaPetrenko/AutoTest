using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestProject1_NUnit
{
    [TestFixture]
    public class Tests1
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("The test is running");
        }

        [Test]
        [Category("A")]
        public void IsEqualString()
        {
            string string1 = "The Boy who lived has come to die";
            string string2 = "The Boy who lived has come to die";

            //Assert.AreEqual(sameString1, sameString2);
            Assert.That(string1, Is.EqualTo(string2));
        }

        [Test]
        [Category("A")]
        public void IsEqualList()
        {
            List<string> list1 = new List<string> { "dog", "cat", "rat" };
            List<string> list2 = new List<string> { "dog", "cat", "rat" };

            //CollectionAssert.AreEqual(list1, list2);
            Assert.That(list1, Is.EquivalentTo(list2));
        }

        [Test, MaxTime(15)]
        public void IsContainsWord()
        {
            string animal = "rat";
            List<string> list1 = new List<string> { "dog", "cat", "rat" };

            //Assert.IsTrue(list1.Contains(animal), $"The collection doesn't contains a {animal}");
            //CollectionAssert.Contains(list1, animal, $"The collection doesn't contains a {animal}");
            Assert.That(list1, Has.Member(animal));
        }

        [Test]
        [TestCase(10, 5)]
        public void CompareTwoNumbers(int a, int b)
        {
            //Assert.True(a > b);
            Assert.That(a, Is.GreaterThan(b));
        }


        [TearDown]
        public void End()
        {
            Console.WriteLine("Test finished");
        }

    }
}