using DynamicLibrary.Tests.Entity;

namespace DynamicLibrary.Tests;
public class PaginationTests
{
	[Fact]
	public void Should_Return_Paginated_List()
	{
		// Arrage
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new PaginationModel
		{
			Page = 1,
			PageSize = 2
		};

		// Act
		var expected = TestEntity.EntityList.Take(2).ToList();
		query = query.ApplyPagination(model);
		var newList = query.ToList();

		// Assert
		Assert.Equal(2, newList.Count);
	}

	[Fact]
	public void Should_Return_A_List_Skipping_Two_Entities()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new PaginationModel
		{
			Page = 2
		};

		// Act
		var expected = TestEntity.EntityList.Skip(2).ToList();
		query = query.ApplyPagination(model);
		var newList = query.ToList();

		// Assert
		Assert.Equal(8, newList.Count);
	}

	[Fact]
	public void Should_Skip_One_And_Take_Seven()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new PaginationModel
		{
			Page = 1,
			PageSize = 7
		};

		// Act
		var expected = TestEntity.EntityList.Skip(1).Take(7).ToList();
		query = query.ApplyPagination(model);
		var newList = query.ToList();

		// Assert
		Assert.Equal(7, newList.Count);
	}
}