using DynamicLibrary.Tests.Entity;

namespace DynamicLibrary.Tests;
public class QueryBuilderTest
{


	[Fact]
	public void Should_Return_Two_Entites()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();

		// Act
		query = query.ApplyFilter(a => a.Age >= 65);
		var newList = query.ToList();

		// Assert
		Assert.Equal(2, newList.Count);
	}

	[Fact]
	public void Should_Apply_Multiple_Filters()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		// Act
		query = query.ApplyFilter(a => a.Age >= 50);
		query = query.ApplyFilter(a => a.Name == "John");
		var newList = query.ToList();
		// Assert
		Assert.Equal(2, newList.Count);
	}
}
