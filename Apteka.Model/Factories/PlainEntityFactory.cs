//using System;
//using System.Linq;
//using System.Linq.Expressions;

//namespace Apteka.Model.Factories
//{
//    public class PlainEntityFactory : IEntityFactory
//    {
//        public T Find<T>(Expression<Func<T, bool>> pred) where T : class, new()
//        {
//            throw new NotImplementedException();
//        }

//        public T Create<T>() where T : new() => new T();

//        public void Attach<T>(T entity) where T : class
//        {
//            throw new NotImplementedException();
//        }

//        public IQueryable<T> Query<T>() where T : class
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
