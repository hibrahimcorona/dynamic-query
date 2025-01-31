using DynamicLibrary.Enums;

namespace DynamicLibrary.Models;

/// <summary>
/// Represents a model for filtering data.
/// </summary>
public class FilterModel<T>
{
	/// <summary>
	/// Gets or sets the field to be filtered.
	/// </summary>
	public string Field { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the value to filter by.
	/// </summary>
	public object Value { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the operator to be used for filtering.
	/// </summary>
	public FilterOperator Operator { get; set; }

	/// <summary>
	/// Gets or sets the list of filters.
	/// </summary>
	public List<FilterModel<T>>? Filter { get; set; }
}
