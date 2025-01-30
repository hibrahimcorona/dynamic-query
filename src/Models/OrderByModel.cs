using DynamicLibrary.Enums;
using System.Linq.Expressions;

namespace DynamicLibrary.Models;
/// <summary>
/// Represents a model for ordering by a field or using a string
/// </summary>
public class OrderByModel<TEntity>
{
	/// <summary>
	/// The field to order by.
	/// </summary>
	public string? Field { get; set; } = string.Empty;

	/// <summary>
	/// The direction of the sorting as string.<br/>
	/// e.g: "Id asc" or "Id desc"
	/// </summary>
	public string? UseString { get; set; } = string.Empty;

	/// <summary>
	/// The direction of the sorting as enum, by default is Ascending.
	/// </summary>
	public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;

	/// <summary>
	/// The expression to order by.
	/// </summary>
	public Expression<Func<TEntity, bool>>? Expression { get; set; }
}
