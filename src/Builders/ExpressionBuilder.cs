using DynamicLibrary.Enums;
using System.Linq.Expressions;

namespace DynamicLibrary.Builders;

/// <summary>
/// Provides methods to build expressions dynamically.
/// </summary>
public static class ExpressionBuilder
{
	/// <summary>
	/// Builds a lambda expression for accessing a specified propertyName of a given type.
	/// </summary>
	/// <typeparam name="T">The type of the object containing the propertyName.</typeparam>
	/// <typeparam name="TKey">The type of the propertyName.</typeparam>
	/// <param name="property">The name of the propertyName to access.</param>
	/// <param name="name">The name of the parameter in the lambda expression.</param>
	/// <returns>A lambda expression representing the propertyName access.</returns>
	/// <exception cref="Exception">Thrown when the propertyName cannot be accessed or converted.</exception>
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

	/// <summary>
	/// Builds a lambda expression for accessing a specified propertyName of a given type without conversion.
	/// </summary>
	/// <typeparam name="T">The type of the object containing the propertyName.</typeparam>
	/// <typeparam name="TKey">The type of the propertyName.</typeparam>
	/// <param name="propertyName">The name of the propertyName to access.</param>
	/// <param name="name">The name of the parameter in the lambda expression.</param>
	/// <returns>A lambda expression representing the propertyName access.</returns>
	/// <exception cref="Exception">Thrown when the propertyName cannot be accessed.</exception>
	public static Expression<Func<T, bool>> BuildExpression<T>(string propertyName, object value, FilterOperator filterOperator)
	{
		try
		{
			ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
			System.Reflection.PropertyInfo? info = typeof(T).GetProperty(propertyName);
			MemberExpression property = Expression.Property(parameter, info);
			var conversion = Convert.ChangeType(value, info.PropertyType);
			ConstantExpression constant = Expression.Constant(conversion, info.PropertyType);

			BinaryExpression binaryExpression = filterOperator switch
			{
				FilterOperator.Equal => Expression.Equal(property, constant),
				FilterOperator.NotEqual => Expression.NotEqual(property, constant),
				FilterOperator.GreaterThan => Expression.GreaterThan(property, constant),
				FilterOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(property, constant),
				FilterOperator.LessThan => Expression.LessThan(property, constant),
				FilterOperator.LessThanOrEqual => Expression.LessThanOrEqual(property, constant),
				_ => throw new ArgumentException("Invalid filter operator.")
			};

			return Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
		}
		catch (Exception)
		{
			throw;
		}
	}
}

