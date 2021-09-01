using System;
using System.Reflection;
using System.Reflection.Emit;

namespace EmitCode2
{
    class Test2
    {
        static void Main(string[] args)
        {
            // const string name = "Test2.exe";
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "Test2";

            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("Module1");

            TypeBuilder typeBuilder = moduleBuilder.DefineType("Test2Type", TypeAttributes.Public);

            MethodBuilder methodBuilder = typeBuilder.DefineMethod("Test2",
                MethodAttributes.Public | MethodAttributes.Static ,
                CallingConventions.Standard, typeof(void),
                new Type[] { });


            ILGenerator generator = methodBuilder.GetILGenerator();
            MethodInfo methodInfo = typeof(System.Console).GetMethod("Clear", BindingFlags.Public | BindingFlags.Static, null, new Type[] { }, null);

            generator.Emit(OpCodes.Call, methodInfo);
            generator.Emit(OpCodes.Ret);

            Type dt = typeBuilder.CreateType();

            MethodInfo methodInfo2 = dt.GetMethod("Test2");
            
          
            if (methodInfo2 != null)
                methodInfo2.Invoke(null, new Type[] {});
            
        }
    }
}
