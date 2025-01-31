# Dynamic Library
This library contains a set of functions that can be used to filter, sort enumerables.

# Nagivation
- [Usage](#usage)
- [Filtering](#filtering)
	- [Filtering using the `FilterModel`](#filtering-using-the-filtermodel)
- [Sorting](#sorting)
	- [Sorting using the `OrderByModel`](#sorting-using-the-orderbymodel)
- [Pagination](#pagination)

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
#### Filtering using the `FilterModel`
You can also use the `FilterModel` to filter the list of entities. <br/>
The `FilterModel` accepts the following properties:
- Field
	- The field to filter by
- Value
	- The value to filter by
- Operator
	- The operator to use in the filter

The `FilterModel` accepts the following operators:
- Equal
- NotEqual
- GreaterThan
- GreaterThanOrEqual
- LessThan
- LessThanOrEqual

The following example shows how to filter the list of entities to get only the ones with age greater than or equal to 65.

```csharp
var query = EntityList.AsQueryable();

var model = new FilterModel<TestEntity>
{
	Field = "Age",
	Operator = FilterOperator.Equal,
	Value = "50"
};

query = query.ApplyFilter<TestEntity>(model);

// Apply the .ToList() to get the filtered list
var newList = query.ToList();
```

*Returns:*
```csharp
[
	new TestEntity { Id = Guid.NewGuid(), Name = "David", LastName = "Brown", Age = 50, CreatedDateUtc = DateTime.UtcNow },
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
#### Sorting using the `OrderByModel`
Filtering can also be done using the `OrderByModel`. <br/>
The `OrderByModel` accepts the following properties:
- Field
	- The field to sort by
- Direction
	- The direction of the sorting
- UseString
	- If desire, you an also sent a string with the field and direction to sort by, e.g.: "Age asc"
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


Or also use the `Field` attribute in the `OrderByModel`:
```csharp
var query = EntityList.AsQueryable();
var model = new OrderByModel<TestEntity>
{
	Field = "Age",
	Direction = SortingDirection.Ascending,
};

query = query.ApplySort(model);

// Apply .ToList() to get the sorted list
var sortedList = query.ToList();
```

*Returns*:
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
> **Note:** If the `Field` property is not present in the Entity, the library will throw an exception.

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