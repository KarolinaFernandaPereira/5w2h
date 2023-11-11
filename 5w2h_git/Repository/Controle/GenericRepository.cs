using Bussines;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Controle
{
    public  class GenericRepository<TEntity> where TEntity : class
    {
        private Context _context;
        private DbSet<TEntity> dbSet;

        public GenericRepository(Context context)
        {
            this._context = context;
            this.dbSet = _context.Set<TEntity>();
        }

       
        public List<TEntity> BuscarTodos()
        {
            return dbSet.ToList();
        }

        
        public virtual void Atualizar(TEntity entity)
        {
            dbSet.Update(entity);

        }

        public virtual void Criar(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Salvar()
        {
                _context.SaveChanges();
        }


        
    }
}
