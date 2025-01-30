# Dynamic Library
This library contains a set of functions that can be used to filter, sort enumerables.

## Usage
This examples contemplates the usage of the library having the following 'entity'.
```csharp
List<TestEntity> EntityList =
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
```
### Filtering
Filtering can be done using the `ApplyFilter`. <br/>
The following example shows how to filter the list of entities to get only the ones with age greater than or equal to 65.
```csharp
var query = EntityList.AsQueryable();
query = query.ApplyFilter(f => f.Age >= 65);

// Apply .ToList() to get the filtered list
var filteredList = query.ToList();
```

*Returns:*
```csharp
[
	new TestEntity { Id = Guid.NewGuid(), Name = "John", LastName = "Moore", Age = 80, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "James", LastName = "Anderson", Age = 65, CreatedDateUtc = DateTime.UtcNow },
];
```

### Sorting
Sorting can be done using the `ApplySort`. <br/>
The following example shows how to sort the list of entities by the `Age` property in ascending order.
```csharp
var query = EntityList.AsQueryable();
query = query.ApplySort(a => a.Age, Enums.SortingDirection.Ascending);

// Apply .ToList() to get the sorted list
var sortedList = query.ToList();
```
*Returns:*
```csharp
[
	new TestEntity { Id = Guid.NewGuid(), Name = "John", LastName = "Wilson", Age = 18, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "Jane", LastName = "Smith", Age = 28, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "Michael", LastName = "Johnson", Age = 30, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "Emily", LastName = "Davis", Age = 35, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "Anna", LastName = "Taylor", Age = 43, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "David", LastName = "Brown", Age = 50, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "John", LastName = "Thomas", Age = 55, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "James", LastName = "Anderson", Age = 65, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "John", LastName = "Moore", Age = 80, CreatedDateUtc = DateTime.UtcNow },
];
```

You can also sort using the `OrderByModel`:
```csharp
var query = EntityList.AsQueryable();
var model = new OrderByModel<TestEntity>
{
	UseString = "Age asc",
};

query = query.ApplySort(model);

// Apply .ToList() to get the sorted list
var sortedList = query.ToList();
```

*Returns:*
```csharp
[
	new TestEntity { Id = Guid.NewGuid(), Name = "John", LastName = "Wilson", Age = 18, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "Jane", LastName = "Smith", Age = 28, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "Michael", LastName = "Johnson", Age = 30, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "Emily", LastName = "Davis", Age = 35, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "Anna", LastName = "Taylor", Age = 43, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "David", LastName = "Brown", Age = 50, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "John", LastName = "Thomas", Age = 55, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "James", LastName = "Anderson", Age = 65, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "John", LastName = "Moore", Age = 80, CreatedDateUtc = DateTime.UtcNow },
];
```

> **Note:** If the `UseString` property is not present in the Entity, the library will throw an exception.


### Pagination
Pagination can be done using the `ApplyPagination`. It uses the `OrderByModel`.<br/>
The following example shows how to paginate the list of entities to get only the first 3 elements.

```csharp
var query = EntityList.AsQueryable();
var model = new PaginationModel
{
	Page = 1,
	PageSize = 3,
};

query = query.ApplyPagination(model);

// Apply .ToList() to get the paginated list
var paginatedList = query.ToList();
```

*Returns:*
```csharp
[
	new TestEntity { Id = Guid.NewGuid(), Name = "John", LastName = "Doe", Age = 25, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "Michael", LastName = "Johnson", Age = 30, CreatedDateUtc = DateTime.UtcNow },
	new TestEntity { Id = Guid.NewGuid(), Name = "John", LastName = "Wilson", Age = 18, CreatedDateUtc = DateTime.UtcNow },
];
```