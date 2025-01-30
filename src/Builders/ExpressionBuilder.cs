using System.Linq.Expressions;

namespace DynamicLibrary.Builders;

/// <summary>
/// Provides methods to build expressions dynamically.
/// </summary>
public static class ExpressionBuilder
{
	/// <summary>
	/// Builds a lambda expression for accessing a specified property of a given type.
	/// </summary>
	/// <typeparam name="T">The type of the object containing the property.</typeparam>
	/// <typeparam name="TKey">The type of the property.</typeparam>
	/// <param name="property">The name of the property to access.</param>
	/// <param name="name">The name of the parameter in the lambda expression.</param>
	/// <returns>A lambda expression representing the property access.</returns>
	/// <exception cref="Exception">Thrown when the property cannot be accessed or converted.</exception>
	public static Expression<Func<T, TKey>> BuildParameter<T, TKey>(string property, string name)
	{
		try
		{
			ParameterExpression parameter = Expression.Parameter(typeof(T), name);
			MemberExpression propertyAccess = Expression.Property(parameter, property);
			UnaryExpression conversion = Expression.Convert(propertyAccess, typeof(TKey));
			return Expression.Lambda<Func<T, TKey>>(conversion, parameter);
		}
		catch (Exception)
		{
			throw;
		}
	}
}

