using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace LudyCakeShop.Infrastructure
{
    internal static class PropertyManager<T>
    {
        private static readonly ConcurrentDictionary<string, Delegate> SetterDelegateCache = new();
        private static readonly ConcurrentDictionary<string, Delegate> ConstructorDelegateCache = new();

        internal static Action<T, object> GetOrCreateSetter(PropertyInfo property)
        {
            if (!SetterDelegateCache.ContainsKey(property.Name))
            {
                ParameterExpression instance = Expression.Parameter(property.DeclaringType, "instance");
                ParameterExpression parameter = Expression.Parameter(typeof(object), "parameter");
                Expression convertParameter = Expression.Convert(parameter, property.PropertyType);
                MethodCallExpression setter = Expression.Call(instance, property.SetMethod, convertParameter);
                LambdaExpression lambda = Expression.Lambda(setter, instance, parameter);

                SetterDelegateCache.TryAdd(property.Name, lambda.Compile());
            }

            Delegate setterDelegate = SetterDelegateCache[property.Name];

            return (Action<T, object>)setterDelegate;
        }

        internal static Func<T> GetOrCreateConstructor(Type type)
        {
            if (!ConstructorDelegateCache.ContainsKey(type.Name))
            {
                NewExpression constructor = Expression.New(type);
                LambdaExpression lambda = Expression.Lambda(constructor);

                ConstructorDelegateCache.TryAdd(type.Name, lambda.Compile());
            }

            Delegate constructorDelegate = ConstructorDelegateCache[type.Name];

            return (Func<T>)constructorDelegate;
        }
    }
}
