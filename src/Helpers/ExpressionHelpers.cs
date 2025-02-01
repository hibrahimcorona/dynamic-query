﻿using System.Linq.Expressions;

namespace DynamicLibrary.Helpers;
internal class ParameterReplacer : ExpressionVisitor
{
	private readonly ParameterExpression _parameter;
	public ParameterReplacer(ParameterExpression parameter)
	{
		_parameter = parameter;
	}

	protected override Expression VisitParameter(ParameterExpression node)
	{
		return _parameter;
	}
}
