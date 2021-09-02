using System;
using System.Reflection;
using System.Reflection.Emit;

namespace EmitCode3
{
    class Test3
    {
        static void Main(string[] args)
        {

            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "Test3";

            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run | AssemblyBuilderAccess.Save);

            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("Module1");

            TypeBuilder typeBuilder = moduleBuilder.DefineType("Sum2Type", TypeAttributes.Public);

            MethodBuilder methodBuilder = typeBuilder.DefineMethod("Sum2",
                MethodAttributes.Public | MethodAttributes.Static,
                CallingConventions.Standard, typeof(int),
                new Type[] {typeof(int), typeof(int) });

            ILGenerator generator = methodBuilder.GetILGenerator();

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Add);
            //generator.Emit(OpCodes.Stloc_0);
            //generator.Emit(OpCodes.Br_S);
            //generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ret);

            Type dt = typeBuilder.CreateType();

            MethodInfo methodInfo2 = dt.GetMethod("Sum2");
            if (methodInfo2 != null)
            {
                int ret;
                ret = (int)(methodInfo2.Invoke(null, new Object[] { 1, 2 }));
                Console.WriteLine("MySum2 returns {0}", ret);
            }

        }
    }
}
