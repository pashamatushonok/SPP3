using System;
using AsmBrowserTest.TestingClasses;
using AssemblyBrowser;
using AssemblyBrowser.Enums;
using NUnit.Framework;

namespace AsmBrowserTest
{
    [TestFixture]
    public class Tests
    {
        private const string Path = "D:\\JetBrains Rider 2019.2.3\\projects\\WpfAppAssemblyBrowser\\WpfAppAssemblyBrowser\\bin\\Debug\\AssemblyBrowser.dll";
        private const int ExpectedNamespaceCount = 3;
        private const int ExpectedMethodCount = 8;
        private const AccessModifier ExpectedAccessModifier = AccessModifier.Private;
        private static readonly Type ExpectedType = typeof(void);
        private readonly BrowserOfAsm _browserOfAssembly = new BrowserOfAsm();

        [Test]
        public void BrowseOfBrowserOfAsmShouldReturnEightMethods()
        {
            BrowsingResult browsingResult = _browserOfAssembly.Browse(typeof(BrowserOfAsm));
            Assert.AreEqual(ExpectedMethodCount, browsingResult.Namespaces[0].AssemblyTypes[0].Methods.Count);
        }

        [Test]
        public void BrowseOfClassWithOneStaticMethodWithVoidReturnTypeShouldReturnOneStaticMethodWithVoidReturnType()
        {
            BrowsingResult browsingResult = _browserOfAssembly.Browse(typeof(ClassWithOnePublicStaticMethodWithVoidReturnType));
            Assert.IsTrue(browsingResult.Namespaces[0].AssemblyTypes[0].Methods[0].IsStatic);
            Assert.AreEqual(ExpectedType, browsingResult.Namespaces[0].AssemblyTypes[0].Methods[0].ValueType);
        }

        [Test]
        public void BrowseOfClassWithPrivateConstructorAndOnePrivateFieldShouldReturnOnePrivateFieldAndPrivateConstructor()
        {
            BrowsingResult browsingResult = _browserOfAssembly.Browse(typeof(ClassWithPrivateConstructorAndOnePrivateField));
            Assert.AreEqual(ExpectedAccessModifier, browsingResult.Namespaces[0].AssemblyTypes[0].Fields[0].Modifier);
            Assert.AreEqual(ExpectedAccessModifier, browsingResult.Namespaces[0].AssemblyTypes[0].Constructors[0].Modifier);
        }

        [Test]
        public void BrowseOfAsmShouldReturnThreeNamespaces()
        {
            BrowsingResult browsingResult = _browserOfAssembly.Browse(Path);
            Assert.AreEqual(ExpectedNamespaceCount, browsingResult.Namespaces.Count);
        }
    }
}
