﻿using AltairOps.DynamicLibrary.Builders;
using AltairOps.DynamicLibrary.Enums;
using AltairOps.DynamicLibrary.Exceptions;
using AltairOps.DynamicLibrary.Helpers;
using AltairOps.DynamicLibrary.Models;
using System.Linq.Expressions;

namespace AltairOps.DynamicLibrary;


/// <summary>
/// Provides extension methods for applying dynamic queries such as filtering, sorting, and pagination to IQueryable sources.
/// </summary>
public static class DynamicQuery
{
	/// <summary>  
	/// Apply filter to the queryable source if parameter <paramref name="expression"/> is not null.  
	/// </summary>  
	/// <param name="source">The source IQueryable to apply the filter to.</param>  
	/// <param name="expression">The filter expression to apply. If null, no filter is applied.</param>  
	/// <returns>The filtered IQueryable if the expression is not null; otherwise, the original IQueryable.</returns>  
	public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> source,
											Expression<Func<T, bool>>? expression)
	{
		if (expression is null)
		{
			return source;
		}

		source = source.Where(expression);
		return source;
	}

	/// <summary>  
	/// Apply filter to the queryable source if parameter <paramref name="model"/> is not null.  
	/// </summary>  
	/// <param name="source">The source IQueryable to apply the filter to.</param>  
	/// <param name="model">The filter model containing field, value, operator, and nested filters. If null, no filter is applied.</param>  
	/// <returns>The filtered IQueryable if the model is not null; otherwise, the original IQueryable.</returns>  
	public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> source, FilterModel? model)
	{
		if (model is null)
		{
			return source;
		}

		if (!string.IsNullOrWhiteSpace(model.Field) && model.Value is not null)
		{
			try
			{
				Expression<Func<T, bool>> expression = ExpressionBuilder.BuildExpression<T>(model.Field, model.Value, model.Operator);
				return model.Operator switch
				{
					FilterOperator.Equal => source.Where(expression),
					FilterOperator.NotEqual => source.Where(expression),
					FilterOperator.GreaterThan => source.Where(expression),
					FilterOperator.GreaterThanOrEqual => source.Where(expression),
					FilterOperator.LessThan => source.Where(expression),
					FilterOperator.LessThanOrEqual => source.Where(expression),
					_ => source
				};
			}
			catch (ArgumentException)
			{
				throw new QueryException($"The property {model.Field} is not present in the {typeof(T).Name}");
			}
		}

		if (model.Filters is not null && model.Filters.Count() != 0)
		{
			FilterModel firstModel = model.Filters.First();
			Expression<Func<T, bool>> firstExpression = ExpressionBuilder.BuildExpression<T>(firstModel.Field, firstModel.Value, firstModel.Operator);
			Expression? combinedExpression = null;
			// Starting from the second filter, since we need to combine the first filter with the rest of the filters.
			//for (var i = 1; i < model.Filters.Count(); i++)
			foreach (var m in model.Filters)
			{
				Expression<Func<T, bool>> innerExpression = ExpressionBuilder.BuildExpression<T>(m.Field, m.Value, m.Operator);
				Expression fullExpression = new ParameterReplacer(firstExpression.Parameters[0]).Visit(innerExpression.Body);
				if (model.Operator == FilterOperator.And)
				{
					combinedExpression = Expression.AndAlso(firstExpression.Body, fullExpression);
				}
				else if (model.Operator == FilterOperator.Or)
				{
					combinedExpression = Expression.OrElse(firstExpression.Body, fullExpression);
				}
			}

			if (combinedExpression is not null)
			{
				var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, firstExpression.Parameters[0]);
				source = source.Where(lambda);
			}
		}
		return source;
	}

	/// <summary>  
	/// Apply sorting to the queryable source based on the provided <paramref name="expression"/> and sorting direction.  
	/// </summary>  
	/// <param name="source">The source IQueryable to apply the sorting to.</param>  
	/// <param name="expression">The sorting expression to apply. If null, no sorting is applied.</param>  
	/// <param name="sortingDirection">The direction of sorting. If null, no sorting is applied.</param>  
	/// <returns>The sorted IQueryable if the expression and sorting direction are not null; otherwise, the original IQueryable.</returns>  
	public static IQueryable<T> ApplySort<T>(this IQueryable<T> source,
										  Expression<Func<T, object>>? expression,
										  SortingDirection? sortingDirection)
	{
		if (expression is null)
		{
			return source;
		}

		return sortingDirection switch
		{
			null => source,
			SortingDirection.Ascending => source.OrderBy(expression),
			SortingDirection.Descending => source.OrderByDescending(expression),
			_ => source
		};
	}


	/// <summary>  
	/// Apply sorting to the queryable source based on the provided <paramref name="orderByModel"/>.  
	/// </summary>  
	/// <param name="source">The source IQueryable to apply the sorting to.</param>  
	/// <param name="orderByModel">The model containing sorting information. If null, no sorting is applied.</param>  
	/// <returns>The sorted IQueryable if the orderByModel is not null; otherwise, the original IQueryable.</returns>  
	public static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, OrderByModel<T>? orderByModel)
	{
		if (orderByModel is null)
		{
			return source;
		}

		if (!string.IsNullOrWhiteSpace(orderByModel.UseString))
		{
			var field = orderByModel.UseString.Split(' ')[0];
			var direction = orderByModel.UseString.Split(' ')[1];
			try
			{
				Expression<Func<T, object>> expression = ExpressionBuilder.BuildParameter<T, object>(field, "o");
				return direction switch
				{
					"asc" => source.OrderBy(expression),
					"desc" => source.OrderByDescending(expression),
					_ => source.OrderBy(expression)
				};
			}
			catch (ArgumentException)
			{
				throw new QueryException($"The property {field} is not present in the {typeof(T).Name}");
			}
		}

		if (orderByModel.Expression is not null)
		{
			return orderByModel.SortingDirection switch
			{
				SortingDirection.Ascending => source.OrderBy(orderByModel.Expression),
				SortingDirection.Descending => source.OrderByDescending(orderByModel.Expression),
				_ => source.OrderBy(orderByModel.Expression)
			};
		}

		if (orderByModel.Field is not null)
		{
			try
			{
				Expression<Func<T, object>> expression = ExpressionBuilder.BuildParameter<T, object>(orderByModel.Field, "o");

				return orderByModel.SortingDirection switch
				{
					SortingDirection.Ascending => source.OrderBy(expression),
					SortingDirection.Descending => source.OrderByDescending(expression),
					_ => source.OrderBy(expression)
				};
			}
			catch (ArgumentException)
			{
				throw new QueryException($"The property {orderByModel.Field} is not present in the {typeof(T).Name}");
			}
		}
		return source;
	}

	/// <summary>
	/// Apply pagination to the queryable source based on the provided <paramref name="model"/>.
	/// </summary>
	/// <param name="source">The source IQueryable to apply the pagination to.</param>
	/// <param name="model">The pagination model containing page number and page size. If null, no pagination is applied.</param>
	/// <returns>The paginated IQueryable if the model is not null; otherwise, the original IQueryable.</returns>
	public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> source, PaginationModel? model)
	{
		if (model?.Page is not null)
		{
			source = source.Skip(model.Page.Value);
		}

		if (model?.PageSize is not null)
		{
			source = source.Take(model.PageSize.Value);
		}

		return source;
	}
}
