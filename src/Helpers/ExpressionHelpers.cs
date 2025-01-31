﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
