using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vexe.Runtime.Extensions
{
    using NUnit.Framework;

    public class TestObject
    {
        public int SomeField = 5;
        public readonly int SomeReadonlyField = 55;
        public const string SomeConstField = "This is a const field";

        public int SomeProperty { get; set; }
        public int SomeBackedProperty { get { return SomeField; } set { SomeField = value; } }
        public int SomeReadonlyProperty {  get { return SomeReadonlyField; } }
        public string SomeConstProperty { get {  return SomeConstField; } }

        public TestObject()
        {
            var o = SomeField;
            var oo = SomeConstField;
            var ooo = SomeReadonlyField;
        }
    }

    [TestFixture]
    public class FastReflectionTests
    {
        [Test]
        public void Can_Get_Property_Getter()
        {
            var propertyInfo = typeof (TestObject).GetProperty("SomeProperty");
            MemberGetter<object, object> getter = null;

            Assert.DoesNotThrow(() => { getter = propertyInfo.DelegateForGet(); });
            Assert.IsNotNull(getter);
        }

        [Test]
        public void Can_Get_Property()
        {
            var testObject = new TestObject();
            var propertyInfo = typeof(TestObject).GetProperty("SomeProperty");
            var getter = propertyInfo.DelegateForGet();

            Assert.AreEqual(0, getter(testObject));
        }

        [Test]
        public void Can_Get_Property_Setter()
        {
            var propertyInfo = typeof(TestObject).GetProperty("SomeProperty");
            MemberSetter<object, object> setter = null;

            Assert.DoesNotThrow(() => { setter = propertyInfo.DelegateForSet(); });
            Assert.IsNotNull(setter);
        }

        [Test]
        public void Can_Set_Property()
        {
            var testObject = new TestObject();
            var testObjectAsObj = (object)testObject;
            var propertyInfo = typeof(TestObject).GetProperty("SomeProperty");
            var setter = propertyInfo.DelegateForSet();
            const int valueToSet = 123;

            Assert.DoesNotThrow(() => setter(ref testObjectAsObj, valueToSet));
            Assert.AreEqual(valueToSet, testObject.SomeProperty);
        }

        [Test]
        public void Can_Get_Field_Getter()
        {
            var fieldInfo = typeof(TestObject).GetField("SomeField");
            MemberGetter<object, object> getter = null;

            Assert.DoesNotThrow(() => { getter = fieldInfo.DelegateForGet(); });
            Assert.IsNotNull(getter);
        }

        [Test]
        public void Can_Get_Field()
        {
            var testObject = new TestObject();
            var fieldInfo = typeof(TestObject).GetField("SomeField");
            var getter = fieldInfo.DelegateForGet();

            Assert.AreEqual(5, getter(testObject));
        }

        [Test]
        public void Can_Get_Field_Setter()
        {
            var fieldInfo = typeof(TestObject).GetField("SomeField");
            MemberSetter<object, object> setter = null;

            Assert.DoesNotThrow(() => { setter = fieldInfo.DelegateForSet(); });
            Assert.IsNotNull(setter);
        }

        [Test]
        public void Can_Set_Field()
        {
            var testObject = new TestObject();
            var testObjectAsObj = (object)testObject;
            var fieldInfo = typeof(TestObject).GetField("SomeField");
            var setter = fieldInfo.DelegateForSet();
            const int valueToSet = 123;

            Assert.DoesNotThrow(() => setter(ref testObjectAsObj, valueToSet));
            Assert.AreEqual(valueToSet, testObject.SomeField);
        }

        [Test]
        public void Can_Get_Readonly_Field_Getter()
        {
            var fieldInfo = typeof(TestObject).GetField("SomeReadonlyField");
            MemberGetter<object, object> getter = null;

            Assert.DoesNotThrow(() => { getter = fieldInfo.DelegateForGet(); });
            Assert.IsNotNull(getter);
        }

        [Test]
        public void Can_Get_Readonly_Field()
        {
            var testObject = new TestObject();
            var fieldInfo = typeof(TestObject).GetField("SomeReadonlyField");
            var getter = fieldInfo.DelegateForGet();

            Assert.AreEqual(55, getter(testObject));
        }

        [Test]
        public void Can_Get_Const_Field_Getter()
        {
            var fieldInfo = typeof(TestObject).GetField("SomeConstField");
            MemberGetter<object, object> getter = null;

            Assert.DoesNotThrow(() => { getter = fieldInfo.DelegateForGet(); });
            Assert.IsNotNull(getter);
        }

        [Test]
        public void Can_Get_Const_Field()
        {
            var testObject = new TestObject();
            var fieldInfo = typeof(TestObject).GetField("SomeConstField");
            var getter = fieldInfo.DelegateForGet();

            Assert.AreEqual("This is a const field", getter(testObject));
        }

        [Test]
        public void Cant_Set_Const_Field()
        {
            var fieldInfo = typeof(TestObject).GetField("SomeConstField");

            Assert.Throws<NotSupportedException>(() => fieldInfo.DelegateForSet());
        }

        [Test]
        public void Can_Get_Readonly_Backing_Field_Getter()
        {
            var propertyInfo = typeof(TestObject).GetProperty("SomeReadonlyProperty");
            MemberGetter<object, object> getter = null;

            Assert.DoesNotThrow(() => { getter = propertyInfo.DelegateForGet(); });
            Assert.IsNotNull(getter);
        }

        [Test]
        public void Can_Get_Readonly_Backing_Field()
        {
            var testObject = new TestObject();
            var propertyInfo = typeof(TestObject).GetProperty("SomeReadonlyProperty");
            var getter = propertyInfo.DelegateForGet();

            Assert.AreEqual(55, getter(testObject));
        }

        [Test]
        public void Can_Get_Const_Backing_Field_Getter()
        {
            var propertyInfo = typeof(TestObject).GetProperty("SomeConstProperty");
            MemberGetter<object, object> getter = null;

            Assert.DoesNotThrow(() => { getter = propertyInfo.DelegateForGet(); });
            Assert.IsNotNull(getter);
        }

        [Test]
        public void Can_Get_Const_Backing_Field()
        {
            var testObject = new TestObject();
            var propertyInfo = typeof(TestObject).GetProperty("SomeConstProperty");
            var getter = propertyInfo.DelegateForGet();

            Assert.AreEqual("This is a const field", getter(testObject));
        }
    }
}