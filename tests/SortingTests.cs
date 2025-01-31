using DynamicLibrary.Exceptions;
using DynamicLibrary.Models;
using DynamicLibrary.Tests.Entity;

namespace DynamicLibrary.Tests;
public class SortingTests
{
	[Fact]
	public void Should_Apply_Sort()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();

		// Act
		query = query.ApplySort(a => a.Age, Enums.SortingDirection.Ascending);
		var newlist = query.ToList();
		var expected = TestEntity.EntityList.OrderBy(a => a.Age).ToList();
		// Assert
		Assert.True(expected.SequenceEqual(newlist));
	}

	[Fact]
	public void Should_Apply_Sort_To_A_String_Property()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();

		// Act
		query = query.ApplySort(a => a.Name, Enums.SortingDirection.Ascending);
		var newlist = query.ToList();
		var expected = TestEntity.EntityList.OrderBy(a => a.Name).ToList();

		// Assert
		Assert.True(expected.SequenceEqual(newlist));
	}

	[Fact]
	public void Should_Return_Asc_List_Using_String()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new OrderByModel<TestEntity>
		{
			UseString = "Age asc",
		};

		// Act
		var expexted = TestEntity.EntityList.OrderBy(a => a.Age).ToList();
		query = query.ApplySorting(model);
		var newList = query.ToList();

		// Assert
		Assert.True(expexted.SequenceEqual(newList));
	}

	[Fact]
	public void Should_Return_Desc_List_Using_String()
	{
		// Arrange 
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new OrderByModel<TestEntity>
		{
			UseString = "Age desc",
		};

		// Act
		var expected = TestEntity.EntityList.OrderByDescending(a => a.Age).ToList();
		query = query.ApplySorting(model);
		var newList = query.ToList();

		// Assert
		Assert.True(expected.SequenceEqual(newList));
	}

	[Fact]
	public void Should_Return_Exception_List_When_String_Is_Empty()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new OrderByModel<TestEntity>
		{
			UseString = string.Empty,
		};

		// Assert
		Assert.Throws<QueryException>(() => query.ApplySorting(model));
	}

	[Fact]
	public void Should_Return_Exception_List_When_String_Is_Null()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new OrderByModel<TestEntity>
		{
			UseString = null,
		};

		// Assert
		Assert.Throws<QueryException>(() => query.ApplySorting(model));
	}

	[Fact]
	public void Should_Return_Execption_When_Field_Does_Not_Exists()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new OrderByModel<TestEntity>
		{
			UseString = "NotExisting asc",
		};

		// Assert
		Assert.Throws<QueryException>(() => query.ApplySorting(model));
	}

	[Fact]
	public void Should_Return_List_Ordered_By_Descending_Using_Model_Field()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new OrderByModel<TestEntity>
		{
			Field = "Age",
			SortingDirection = Enums.SortingDirection.Descending
		};

		// Arrange
		var expected = TestEntity.EntityList.OrderByDescending(a => a.Age).ToList();
		query = query.ApplySorting(model);
		var newList = query.ToList();

		// Assert
		Assert.True(expected.SequenceEqual(newList));
	}
}
