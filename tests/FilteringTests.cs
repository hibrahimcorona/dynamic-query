using AltairOps.DynamicLibrary.Enums;
using AltairOps.DynamicLibrary.Models;
using DynamicLibrary.Tests.Entity;

namespace AltairOps.DynamicLibrary.Tests;
public class FilteringTests
{


	[Fact]
	public void Should_Filter_Using_Expression()
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

	[Fact]
	public void Should_Filter_Using_The_FilterModel_Using_The_Equal_Operator()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new FilterModel
		{
			Field = "Age",
			Operator = FilterOperator.Equal,
			Value = "50"
		};

		// Act
		query = query.ApplyFilter<TestEntity>(model);
		var newList = query.ToList();

		// Assert
		Assert.Single(newList);
	}

	[Fact]
	public void Should_Filter_Using_The_Filter_Model_And_Using_The_Not_Equals_Operator()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new FilterModel
		{
			Field = "Age",
			Operator = FilterOperator.NotEqual,
			Value = "50"
		};

		// Act
		query = query.ApplyFilter<TestEntity>(model);
		var newList = query.ToList();

		// Assert
		Assert.Equal(9, newList.Count);
	}

	[Fact]
	public void Should_Filter_Using_The_Filter_Model_And_Using_The_Greater_Than_Operator()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new FilterModel
		{
			Field = "Age",
			Operator = FilterOperator.GreaterThan,
			Value = "50"
		};

		// Act
		query = query.ApplyFilter<TestEntity>(model);
		var newList = query.ToList();

		// Assert
		Assert.Equal(3, newList.Count);
	}

	[Fact]
	public void Should_Filter_Using_The_Filter_Model_And_Using_The_Greater_Than_Or_Equal_Operator()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new FilterModel
		{
			Field = "Age",
			Operator = FilterOperator.GreaterThanOrEqual,
			Value = "50"
		};

		// Act
		query = query.ApplyFilter<TestEntity>(model);
		var newList = query.ToList();

		// Assert
		Assert.Equal(4, newList.Count);
	}

	[Fact]
	public void Should_Filter_Using_The_Filter_Model_And_Using_The_Less_Than_Operator()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new FilterModel
		{
			Field = "Age",
			Operator = FilterOperator.LessThan,
			Value = "50"
		};

		// Act
		query = query.ApplyFilter<TestEntity>(model);
		var newList = query.ToList();

		// Assert
		Assert.Equal(6, newList.Count);
	}

	[Fact]
	public void Should_Filter_Using_The_Filter_Model_And_Using_The_Less_Than_Or_Equal_Operator()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new FilterModel
		{
			Field = "Age",
			Operator = FilterOperator.LessThanOrEqual,
			Value = "50"
		};

		// Act
		query = query.ApplyFilter<TestEntity>(model);
		var newList = query.ToList();

		// Assert
		Assert.Equal(7, newList.Count);
	}

	[Fact]
	public void Should_Filter_Using_Filter_List()
	{
		// Arrange
		IQueryable<TestEntity> query = TestEntity.EntityList.AsQueryable();
		var model = new FilterModel
		{
			Operator = FilterOperator.And,
			Filters =
			[
				new FilterModel
				{
					Field = "Age",
					Operator = FilterOperator.GreaterThanOrEqual,
					Value = " 50"
				},
				new FilterModel
				{
					Field = "Name",
					Operator = FilterOperator.Equal,
					Value = "John"
				}
			]
		};

		// Act
		query = query.ApplyFilter<TestEntity>(model);
		var newList = query.ToList();

		// Assert
		Assert.Equal(2, newList.Count);
	}
}