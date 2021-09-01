using System;
using System.Reflection;
using System.Reflection.Emit;

namespace EmitCode
{
    class Test
    {
        static void Main(string[] args)
        {
            const string name = "Test.exe";
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "Test";

            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);

            ModuleBuilder moduleBuilder =  assemblyBuilder.DefineDynamicModule("Class1.mod", "Test.exe", false);

            TypeBuilder typeBuilder = moduleBuilder.DefineType("Class1"); 

            MethodBuilder methodBuilder =  typeBuilder.DefineMethod("Main", 
                MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.HideBySig,
                CallingConventions.Standard, typeof(void),
                new Type[] { typeof(string[])});

            assemblyBuilder.SetEntryPoint(methodBuilder);

          

            ILGenerator generator = methodBuilder.GetILGenerator();
            MethodInfo methodInfo = typeof(System.Console).GetMethod("Clear", BindingFlags.Public | BindingFlags.Static, null,   new Type[] { }, null);

            generator.Emit(OpCodes.Call, methodInfo);
            generator.Emit(OpCodes.Ret);

            typeBuilder.CreateType();
            assemblyBuilder.Save(name);
        }

    }
}
