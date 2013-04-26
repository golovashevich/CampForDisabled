using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CustomValidation.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Web.Tests.Attributes
{
    [TestClass]
    public class NumericLessThanTests
    {
        private class TestProperties
        {
            [DisplayName("WithDisplayName")]
            public virtual int WithDisplayName { get; set; }

            [Display(Name="WithName")]
            public virtual int WithName { get; set; }

            [Display(Name = "WithNameAndResourceType", ResourceType=typeof(Resources))]
            public virtual int WithNameAndResourceType { get; set; }

            [Display(Name = "WithNonExistentNameAndResourceType", ResourceType = typeof(Resources))]
            public virtual int WithNonExistentNameAndResourceType { get; set; }

            [Display(Name = "WithNameAndNullResourceType", ResourceType=null)]
            public virtual int WithNameAndNullResourceType { get; set; }
        }


        [TestMethod]
        public void OtherPropertyTitle()
        {
            var methodInfo = typeof(NumericLessThanAttribute).GetMethod("TryToExtractOtherPropertyTitle", 
                    BindingFlags.Instance | BindingFlags.NonPublic);
            var getOtherPropertyTitle = typeof(NumericLessThanAttribute).GetProperty("OtherPropertyTitle", 
                    BindingFlags.Instance | BindingFlags.NonPublic).GetGetMethod(true);

            var checks = new Tuple<string, string>[] { 
                    Tuple.Create("WithNonExistentNameAndResourceType", "WithNonExistentNameAndResourceType"),
                    Tuple.Create("WithDisplayName", "WithDisplayName"), 
                    Tuple.Create("WithName", "WithName"), 
                    Tuple.Create("WithNameAndResourceType", "TestFieldValue"),
                    Tuple.Create("WithNonExistentNameAndResourceType", "WithNonExistentNameAndResourceType"),
                    Tuple.Create("WithNameAndNullResourceType", "WithNameAndNullResourceType")};

            foreach (var check in checks)
            {
                var attribute = new NumericLessThanAttribute(check.Item1);
                var propertyInfo = typeof(TestProperties).GetProperty(check.Item1);
                methodInfo.Invoke(attribute, new[] { propertyInfo });
                Assert.AreEqual(check.Item2, getOtherPropertyTitle.Invoke(attribute, null), 
                        "Invalid title for property " + check.Item1);
            }
        }
    }
}
