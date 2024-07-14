using System;
using plib.Enums;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace plib.Models
{
    public static class ProfiledRecursive
    {
        public static void Run<T>(T instance) where T : class
        {
            AutoProfileRecursive(instance, typeof(T));
        }

        private static void AutoProfileRecursive(object instance, Type type)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            MethodInfo[] methods = type.GetMethods(bindingFlags);
            foreach (MethodInfo method in methods)
            {
                ProfiledAttribute profiledAttribute = method.GetCustomAttributes(
                    typeof(ProfiledAttribute), false)
                    .Cast<ProfiledAttribute>()
                    .FirstOrDefault();

                if (profiledAttribute != null)
                {
                    if (profiledAttribute.OperationType == ProfiledOperationImplement.POI_ASYNC && 
                        method.ReturnType == typeof(Task))
                    {
                        async Task methodDelegate() => await (Task)method.Invoke(instance, null);
                        ProfiledImpl.ProfileAsync(methodDelegate).Wait();
                    }
                    else if (profiledAttribute.OperationType == ProfiledOperationImplement.POI_SYNC)
                    {
                        void methodDelegate() => method.Invoke(instance, null);
                        ProfiledImpl.Profile(methodDelegate);
                    }
                }
            }

            foreach (var nestedType in type.GetNestedTypes(bindingFlags))
            {
                var nestedInstance = Activator.CreateInstance(nestedType);
                AutoProfileRecursive(nestedInstance, nestedType);
            }
        }
    }
}