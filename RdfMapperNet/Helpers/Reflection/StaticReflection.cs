using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RdfMapperNet.Helpers.Reflection
{
    /// <summary>
    /// http://joelabrahamsson.com/getting-property-and-method-names-using-static-reflection-in-c/
    /// </summary>
    public static class StaticReflection
    {
        public static string GetFullMemberName<T>(Expression<Func<T, object>> expression)
        {
            return string.Format("{0}.{1}", typeof(T).FullName, GetMemberName(expression));
        }

        public static string GetMemberName<T>(this T instance, Expression<Func<T, object>> expression)
        {
            return GetMemberName(expression);
        }

        public static string GetMemberName<T>(Expression<Func<T, object>> expression)
        {
            if (expression == null)
                throw new ArgumentException("The expression cannot be null.");

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(this T instance, Expression<Action<T>> expression)
        {
            return GetMemberName(expression);
        }

        public static string GetMemberName<T>(Expression<Action<T>> expression)
        {
            if (expression == null)
                throw new ArgumentException("The expression cannot be null.");

            return GetMemberName(expression.Body);
        }

        private static string GetMemberName(Expression expression)
        {
            if (expression == null)
                throw new ArgumentException("The expression cannot be null.");

            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression = (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression = (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }

            throw new ArgumentException("Invalid expression");
        }

        private static string GetMemberName(UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression = (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ((MemberExpression)unaryExpression.Operand).Member.Name;
        }

        public static bool IsCollection(this PropertyInfo property)
        {
            return !typeof(string).Equals(property.PropertyType) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType);
        }

        public static bool IsString(this PropertyInfo property)
        {
            return typeof(string).Equals(property.PropertyType);
        }

        public static bool IsValueType(this PropertyInfo property)
        {
            return property.PropertyType.BaseType != null && typeof(ValueType).Equals(property.PropertyType.BaseType);
        }

        public static bool ContainsAttribute<T>(this T entity, Type attributeType)
        {
            return entity.GetType().IsDefined(attributeType);
        }

        public static bool ContainsAttribute(this PropertyInfo property, Type attributeType)
        {
            return Attribute.IsDefined(property, attributeType);
        }

        public static bool TryGetAttribute<T1, T2>(T1 entity, out T2 attribute)
        {
            Type type = entity.GetType();
            object[] attributes = type.GetCustomAttributes(true);

            attribute = attributes.OfType<T2>().FirstOrDefault();

            return attribute != null;
        }

        public static bool TryGetAttribute<T>(this PropertyInfo property, out T attribute) where T : class
        {
            attribute = property.GetCustomAttribute(typeof(T)) as T;

            return attribute != null;
        }
    }
}
