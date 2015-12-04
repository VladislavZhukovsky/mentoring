using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {
            //var mapGenerator = new MappingGenerator();
            //var mapper = mapGenerator.Generate<Foo, Bar>();

            //var result = mapper.Map(new Foo());
            var par = Expression.Parameter(typeof(int));
            var add = Expression.Add(par, Expression.Constant(3));
            var exp1 = Expression.New(typeof(object));
            var l = Expression.Lambda<Func<object>>(exp1);
            var ll = l.Compile();
            var r = ll.Invoke();
        }
        public class Foo { }
        public class Bar { }

        public class Mapper<TSource, TDestination>
        {
            Func<TSource, TDestination> mapFunction;

            internal Mapper(Func<TSource, TDestination> func)
            {
                mapFunction = func;
            }

            public TDestination Map(TSource source)
            {
                return mapFunction(source);
            }
        }

        public class MappingGenerator
        {
            public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
            {
                var sourceParam = Expression.Parameter(typeof(TSource));
                var mapFunction =
                    Expression.Lambda<Func<TSource, TDestination>>(
                        Expression.New(typeof(TDestination)),
                        sourceParam
                 );
                return new Mapper<TSource, TDestination>(mapFunction.Compile());
            }

        }
    }
}
