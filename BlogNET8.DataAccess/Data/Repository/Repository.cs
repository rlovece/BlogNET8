using BlogNET8.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogNET8.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = Context.Set<T>();
        }
        public void Add(T entity)
        {
           dbSet.Add(entity);
        }

    

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            //Toda la lista
            IQueryable<T> query = dbSet;

            //Aplicamos filtro si existe
            if(filter != null)
            {
                query = query.Where(filter);
            }

            //Aplicamos relación si existe.
            /*includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries):

            includeProperties.Split: Esta función divide la cadena includeProperties en un array de subcadenas. Utiliza una coma (,) como delimitador para dividir la cadena.
            new char[] { ',' }: Este es el delimitador que se utiliza para dividir la cadena. En este caso, es una coma.

            StringSplitOptions.RemoveEmptyEntries: Este parámetro asegura que cualquier entrada vacía resultante de la división se elimine del array. Por ejemplo, si includeProperties contiene ",,,", el resultado no incluirá elementos vacíos.*/

            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            //Aplicamos orden si existe
            if (orderBy != null)
            {
                // orderBy va a ser una función de este tipo ➡️ var orderBy = q => q.OrderBy(e => e.Name);

                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public T GerFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            //Toda la lista
            IQueryable<T> query = dbSet;


            //Aplicamos filtro si existe
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Aplicamos relación si existe.
            /*includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries):

            includeProperties.Split: Esta función divide la cadena includeProperties en un array de subcadenas. Utiliza una coma (,) como delimitador para dividir la cadena.
            new char[] { ',' }: Este es el delimitador que se utiliza para dividir la cadena. En este caso, es una coma.

            StringSplitOptions.RemoveEmptyEntries: Este parámetro asegura que cualquier entrada vacía resultante de la división se elimine del array. Por ejemplo, si includeProperties contiene ",,,", el resultado no incluirá elementos vacíos.*/

            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            // Retorno sólo el primer resultado de la query.
            return query.FirstOrDefault();

        }

        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);

            if (entityToRemove != null)
            {
                dbSet.Remove(entityToRemove);
            }
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
