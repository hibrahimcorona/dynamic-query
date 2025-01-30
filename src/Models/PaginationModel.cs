namespace DynamicLibrary.Tests;

/// <summary>
/// Represents a model for pagination with page number and page size.
/// </summary>
public class PaginationModel
{
	/// <summary>
	/// Gets or sets the page number.
	/// </summary>
	public int? Page { get; set; }

	/// <summary>
	/// Gets or sets the size of the page.
	/// </summary>
	public int? PageSize { get; set; }
}
