namespace DynamicLibrary.Tests.Entity;
/// <summary>
/// For testing purposes.
/// </summary>
internal class TestEntity
{
	public Guid Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public string LastName { get; set; } = string.Empty;

	public int Age { get; set; }

	public DateTime CreatedDateUtc { get; set; }

	public static List<TestEntity> EntityList =
	[
		new TestEntity { Id = Guid.NewGuid(), Name = "John", LastName = "Doe", Age = 25, CreatedDateUtc = DateTime.UtcNow },
		new TestEntity { Id = Guid.NewGuid(), Name = "Michael", LastName = "Johnson", Age = 30, CreatedDateUtc = DateTime.UtcNow },
		new TestEntity { Id = Guid.NewGuid(), Name = "John", LastName = "Wilson", Age = 18, CreatedDateUtc = DateTime.UtcNow },
		new TestEntity { Id = Guid.NewGuid(), Name = "John", LastName = "Thomas", Age = 55, CreatedDateUtc = DateTime.UtcNow },
		new TestEntity { Id = Guid.NewGuid(), Name = "Emily", LastName = "Davis", Age = 35, CreatedDateUtc = DateTime.UtcNow },
		new TestEntity { Id = Guid.NewGuid(), Name = "David", LastName = "Brown", Age = 50, CreatedDateUtc = DateTime.UtcNow },
		new TestEntity { Id = Guid.NewGuid(), Name = "John", LastName = "Moore", Age = 80, CreatedDateUtc = DateTime.UtcNow },
		new TestEntity { Id = Guid.NewGuid(), Name = "Anna", LastName = "Taylor", Age = 43, CreatedDateUtc = DateTime.UtcNow },
		new TestEntity { Id = Guid.NewGuid(), Name = "James", LastName = "Anderson", Age = 65, CreatedDateUtc = DateTime.UtcNow },
		new TestEntity { Id = Guid.NewGuid(), Name = "Jane", LastName = "Smith", Age = 28, CreatedDateUtc = DateTime.UtcNow },
	];
}
