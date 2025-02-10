using AltairOps.DynamicLibrary.Enums;
using AltairOps.DynamicLibrary.Models;
using DynamicLibrary.Tests.Entity;

namespace AltairOps.DynamicLibrary.Tests;
public class QueryResultsTest
{
	[Fact]
	public void Should_Return_Query_Response()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();

		// Act
		var response = new QueryResponse<TestEntity>(query);
		var newList = response.Data.ToList();

		// Assert
		Assert.Equal(newList.Count, response.Total);
	}

	[Fact]
	public void Should_Return_Query_Response_With_Empty_Data()
	{
		// Arrange
		IQueryable<TestEntity> query = new List<TestEntity>().AsQueryable();

		// Act
		var response = new QueryResponse<TestEntity>(query);
		var newList = response.Data.ToList();

		// Assert
		Assert.Equal(newList.Count, response.Total);
	}

	[Fact]
	public void Should_Return_Query_Response_With_Multiple_Applies()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();

		// Act
		query = query.ApplyFilter<TestEntity>(a => a.Name == "John");
		query = query.ApplySort<TestEntity>(a => a.Age, SortingDirection.Descending);
		query = query.ApplyPagination<TestEntity>(new PaginationModel
		{
			PageSize = 3
		});

		var newList = query.ToList();

		// Assert
		Assert.Equal(3, newList.Count);
		Assert.True(newList.All(a => a.Name == "John"));
		Assert.Equal(80, newList.First().Age);
		Assert.Equal(25, newList.Last().Age);
	}

	[Fact]
	public void Should_Return_One_When_Using_Filter_Model()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var filter = new FilterModel
		{
			Field = "Name",
			Operator = FilterOperator.Equal,
			Value = "Emily"
		};

		// Act
		query = query.ApplyFilter<TestEntity>(filter);
		var newList = query.ToList();

		// Assert
		Assert.Equal(1, newList.Count);
		Assert.True(newList.All(a => a.Name == "Emily"));
	}
}
