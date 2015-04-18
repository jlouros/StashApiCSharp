using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Atlassian.Stash.Api.UnitTests
{
    [TestClass]
    public class CodeAnalysis
    {
        [TestMethod]
        public void EnsureNoAsyncVoid_Methods()
        {
            AssemblyAnalysis.AssertNoAsyncVoidMethods(typeof(StashClient).Assembly);
        }

    }

    // from Phil Haack blog http://haacked.com/archive/2014/11/11/async-void-methods/
    public static class AssemblyAnalysis
    {
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        public static bool HasAttribute<TAttribute>(this MethodInfo method) where TAttribute : Attribute
        {
            return method.GetCustomAttributes(typeof(TAttribute), false).Any();
        }

        public static IEnumerable<MethodInfo> GetAsyncVoidMethods(this Assembly assembly)
        {
            return assembly.GetLoadableTypes()
              .SelectMany(type => type.GetMethods(
                BindingFlags.NonPublic
                | BindingFlags.Public
                | BindingFlags.Instance
                | BindingFlags.Static
                | BindingFlags.DeclaredOnly))
              .Where(method => method.HasAttribute<AsyncStateMachineAttribute>())
              .Where(method => method.ReturnType == typeof(void));
        }

        public static void AssertNoAsyncVoidMethods(Assembly assembly)
        {
            List<string> messages = assembly
                .GetAsyncVoidMethods()
                .Select(method => String.Format("'{0}.{1}' is an async void method.", method.DeclaringType.Name, method.Name))
                .ToList();

            Assert.IsFalse(messages.Any(), "Async void methods found!" + Environment.NewLine + String.Join(Environment.NewLine, messages));
        }
    }
}
