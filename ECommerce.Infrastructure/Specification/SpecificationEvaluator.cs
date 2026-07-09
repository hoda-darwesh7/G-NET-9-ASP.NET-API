using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Specification
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> entryPoint ,
                                                                    ISpecification<TEntity , TKey> spec) where TEntity : BaseEntity<TKey>
        {
            var query = entryPoint;

            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            //if (spec.IncludeExpressions.Any())
            //{
            //    foreach (var expression in spec.IncludeExpressions)
            //    {
            //        query = query.Include(expression);
            //    }
            //}

            query = spec.IncludeExpressions.Aggregate(query , (current , nextExp) => current.Include(nextExp));

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null) 
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPaginated)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            return query;
        }
    }
}
