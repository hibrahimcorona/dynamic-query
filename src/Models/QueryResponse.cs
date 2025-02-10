namespace AltairOps.DynamicLibrary.Models;
/// <summary>
/// Represents a response for a query containing data and total count.
/// </summary>
/// <typeparam name="T">The type of the data items.</typeparam>
public class QueryResponse<T>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="QueryResponse{T}"/> model with the specified data.
	/// </summary>
	/// <param name="data">The data items for the query response.</param>
	public QueryResponse(IEnumerable<T> data)
	{
		Data = data;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="QueryResponse{T}"/> model.
	/// </summary>
	public QueryResponse()
	{
	}

	private List<T> _data;
	private int _total;

	/// <summary>
	/// Gets or sets the data items for the query response.
	/// </summary>
	public IEnumerable<T> Data
	{
		get => _data;
		set
		{
			_data = value.ToList();
			_total = value.Count();
		}
	}

	/// <summary>
	/// Gets the total count of data items.
	/// </summary>
	public int Total => _total;
}
